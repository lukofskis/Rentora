using Rentora.Data;
using Rentora.Data.Entities;
using Rentora.Helpers;
namespace Rentora.Auth;

/*

    Task CreateSessionAsync(Guid sessionId, string userId, string refreshToken, DateTime expiresAt);
    Task ExtendSessionAsync(Guid sessionId, string refreshToken, DateTime expiresAt);
    Task InvalidateSessionAsync(Guid sessionId);
    Task<bool> IsSessionValidAsync(Guid sessionId, string refreshToken);

 */

public class SessionService(ForumDbContext dbContext)
{
    public async Task CreateSessionAsync(Guid sessionId, string userId, string refreshToken, DateTime expiresAt)
    {
        dbContext.Sessions.Add(new Session
        {
            Id = sessionId,
            UserId = userId,
            InitiatedAt = DateTimeOffset.UtcNow,
            ExpiresAt = expiresAt,
            LastRefreshToken = refreshToken.ToSHA256()
        });

        await dbContext.SaveChangesAsync();
    }
    
    public async Task ExtendSessionAsync(Guid sessionId, string refreshToken, DateTime expiresAt)
    {
        var session = await dbContext.Sessions.FindAsync(sessionId);
        session.ExpiresAt = expiresAt;
        session.LastRefreshToken = refreshToken.ToSHA256();
        
        await dbContext.SaveChangesAsync();
    }
    
    public async Task InvalidateSessionAsync(Guid sessionId)
    {
        var session = await dbContext.Sessions.FindAsync(sessionId);
        if (session is null)
        {
            return;
        }
        session.IsRevoked = true;
        
        await dbContext.SaveChangesAsync();
    }
    
    public async Task<bool> IsSessionValidAsync(Guid sessionId, string refreshToken)
    {
        var session = await dbContext.Sessions.FindAsync(sessionId);
        return session is not null && session.ExpiresAt > DateTimeOffset.UtcNow && !session.IsRevoked &&
               session.LastRefreshToken == refreshToken.ToSHA256();
    }
}