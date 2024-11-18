using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
//using Microsoft.IdentityModel.JsonWebTokens;
using Rentora.Auth.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Transactions;
using Microsoft.AspNetCore.Http.HttpResults;
using Rentora.Data;

namespace Rentora.Auth;

public static class AuthEndPoints
{
    public static void  AddAuthApi(this WebApplication app)
    {
        //register
    //papildomai , IServiceScopeFactory scopeFactory
        app.MapPost("api/register", async (UserManager<ForumUser> userManager, RegisterUserDto dto,IServiceScopeFactory scopeFactory) =>
        {
            // check user exists
            var user = await userManager.FindByNameAsync(dto.UserName);
            if (user != null)
            {
                return Results.UnprocessableEntity("Username already exists");
            }

            var newUser = new ForumUser
            {
                Email = dto.Email,
                UserName = dto.UserName,
            };
            
            //TODO : wrap in transaction
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var createUserResult = await userManager.CreateAsync(newUser, dto.Password);
                if (!createUserResult.Succeeded)
                {
                    return Results.UnprocessableEntity("Failed to create user");
                }
                var addToRoleResult = await userManager.AddToRoleAsync(newUser, ForumRoles.ForumUser);
                if (!addToRoleResult.Succeeded)
                {
                    return Results.UnprocessableEntity("Failed to assign role to user");
                }
                transaction.Complete();
            }
            
            
            
            // var createUserResult  = await userManager.CreateAsync(newUser, dto.Password);
            // if (!createUserResult.Succeeded)
            // {
            //     return Results.UnprocessableEntity();
            // }
            // await userManager.AddToRoleAsync(newUser, ForumRoles.ForumUser);
            
            return Results.Created();
           // return Results.Created("api/login", new UserDto(newUser.Id, newUser.UserName, newUser.Email));

        });
    
        //login
        app.MapPost("api/login", async (UserManager<ForumUser> userManager,JwtTokenService jwtTokenService,SessionService sessionService, HttpContext httpContext, LoginDto dto) =>
        {
            // check user exists
            var user = await userManager.FindByNameAsync(dto.UserName);
            if (user == null)
                return Results.UnprocessableEntity("Username does not exit.");

            var isPasswordValid = await userManager.CheckPasswordAsync(user, dto.Password);
            if (!isPasswordValid)
                return Results.UnprocessableEntity("Username or password was incorrect.");
    
            var roles = await userManager.GetRolesAsync(user);
            
            var sessionId = Guid.NewGuid();
            var expiresAt = DateTime.UtcNow.AddDays(3);
            var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
            var refreshToken = jwtTokenService.CreateRefreshToken(sessionId, user.Id, expiresAt);
            
            await sessionService.CreateSessionAsync(sessionId, user.Id, refreshToken, expiresAt);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                Expires = expiresAt,
                //Secure = false => should be true possibly
            };
            
            httpContext.Response.Cookies.Append("RefreshToken", refreshToken , cookieOptions);

            return Results.Ok(new SuccessfulLoginDto(accessToken));
            // user.ForceRelogin = false;
            // await userManager.UpdateAsync(user);
            //
            //
            // var refreshToken = jwtTokenService.CreateRefreshToken(user.Id);

            //return Results.Ok(new SuccessfulLoginDto(accessToken, refreshToken));
        });

        app.MapPost("api/accessToken",
            async (UserManager<ForumUser> userManager, JwtTokenService jwtTokenService,SessionService sessionService, HttpContext httpContext) =>
            {
                if (!httpContext.Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
                {
                    return Results.UnprocessableEntity();
                }
                if (!jwtTokenService.TryParseRefreshToken(refreshToken, out var claims))
                {
                    return Results.UnprocessableEntity();
                }
                
                var sessionId = claims.FindFirstValue("SessionId");
                if (string.IsNullOrWhiteSpace(sessionId))
                {
                    return Results.UnprocessableEntity();
                }
                
                var sessionIdAsGuid = Guid.Parse(sessionId);
                if (!await sessionService.IsSessionValidAsync(sessionIdAsGuid, refreshToken))
                {
                    return Results.UnprocessableEntity();
                }
                
                var userId = claims.FindFirstValue(JwtRegisteredClaimNames.Sub);
                var user = await userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Results.UnprocessableEntity();
                }
                
                
                var roles = await userManager.GetRolesAsync(user);

                var expiresAt = DateTime.UtcNow.AddDays(3);
                var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
                var newRefreshToken = jwtTokenService.CreateRefreshToken(sessionIdAsGuid, user.Id, expiresAt);
                
                var cookieOptions = new CookieOptions
                {   
                    HttpOnly = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = expiresAt,
                    //Secure = false => should be true possibly
                };
            //new pakeisti
                httpContext.Response.Cookies.Append("RefreshToken", newRefreshToken, cookieOptions);
                
                await sessionService.ExtendSessionAsync(sessionIdAsGuid, newRefreshToken, expiresAt);
                
                return Results.Ok(new SuccessfulLoginDto(accessToken));
            });

        app.MapPost("api/logout", async (UserManager<ForumUser> userManager, JwtTokenService jwtTokenService, SessionService sessionService, HttpContext httpContext) =>
        {
            if (!httpContext.Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
            {
                return Results.UnprocessableEntity();
            }

            if (!jwtTokenService.TryParseRefreshToken(refreshToken, out var claims))
            {
                return Results.UnprocessableEntity();
            }

            var sessionId = claims.FindFirstValue("SessionId");
            if (string.IsNullOrWhiteSpace(sessionId))
            {
                return Results.UnprocessableEntity();
            }
            
            await sessionService.InvalidateSessionAsync(Guid.Parse(sessionId));
            httpContext.Response.Cookies.Delete("RefreshToken");
            
            return Results.Ok();
        });

    }
    public record RegisterUserDto(string UserName, string Email, string Password);
    public record LoginDto(string UserName, string Password);
    public record SuccessfulLoginDto(string AccessToken);
    
}