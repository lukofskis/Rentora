using System.Security.Claims;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using O9d.AspNet.FluentValidation;
using Rentora.Auth;
using Rentora.Auth.Model;
using Rentora.Data;
using Rentora.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{

    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod().
            AllowCredentials());
});
//PostgreSQL
//Npgsql.EntityFrameworkCore.PostgreSQL
//Microsoft.EntityFrameworkCore.Tools

//FluentValidation
//FluentValidation.DependencyInjectionExtensions
//09d.AspNet.FluentValidation

//dotnet tool install --global dotnet-ef done
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ForumDbContext>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddTransient<JwtTokenService>();
builder.Services.AddTransient<SessionService>();
builder.Services.AddScoped<AuthSeeder>();
//2
builder.Services.AddIdentity<ForumUser, IdentityRole>()
    .AddEntityFrameworkStores<ForumDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(configureOptions: options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.MapInboundClaims = false;
    options.TokenValidationParameters.ValidAudience = builder.Configuration["Jwt:ValidAudience"];
    options.TokenValidationParameters.ValidIssuer = builder.Configuration["Jwt:ValidIssuer"];
    options.TokenValidationParameters.IssuerSigningKey =
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]));
});

builder.Services.AddAuthorization();


var app = builder.Build();
//app.UseCors();
using var scope = app.Services.CreateScope();
//var dbContext = scope.ServiceProvider.GetRequiredService<ForumDbContext>();

var dbSeeder = scope.ServiceProvider.GetRequiredService<AuthSeeder>();
await dbSeeder.SeedAsync();

/*
    /api/v1/houses GET List 200
    /api/v1/houses POST Create 201
    /api/v1/houses/{id} GET One 200
    /api/v1/houses/{id} PUT/PATCH Modify 200
    /api/v1/houses/{id} DELETE Remove 200/204
 */
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddAuthApi();
var housesGroup = app.MapGroup("/api").WithValidationFilter();
//automapper, mapperly bibliotekos, darysime rankiniu
housesGroup.MapGet("houses", async (ForumDbContext dbContext, CancellationToken CancellationToken) =>
{
  return (await dbContext.Houses.ToListAsync(CancellationToken))
      .Select(houses => new HousesDto(houses.Id,houses.CreatedAt, houses.Name,houses.Region,houses.District));
});

housesGroup.MapGet("houses/{houseId}", async (int houseId,ForumDbContext dbContext) =>
{
    var  house = await dbContext.Houses.FirstOrDefaultAsync((h) => h.Id == houseId);
    if (house == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(new HousesDto(house.Id,house.CreatedAt, house.Name,house.Region,house.District));
});

// [Authorize(Roles = ForumRoles.ForumUser)]  ir UserId = ""
housesGroup.MapPost("houses/", [Authorize(Roles = ForumRoles.Admin)]  async ([Validate]CreateHousesDto createHousesDto,HttpContext httpContext,ForumDbContext dbContext ) =>
{
    var houses = new Houses()
    {
        CreatedAt = DateTime.UtcNow,
        Name = createHousesDto.Name,
        Region = createHousesDto.Region,
        District = createHousesDto.District,
        UserId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub)

    };
    
    dbContext.Houses.Add(houses);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/api/houses/{houses.Id}",
        new HousesDto(houses.Id, houses.CreatedAt, houses.Name, houses.Region, houses.District));
});
//neleisti kitiems paeditinti tik admin ir user
housesGroup.MapPut("houses/{houseId}",[Authorize] async(int houseId,[Validate]UpdateHousesDto dto ,HttpContext httpContext,ForumDbContext dbContext  ) =>
{
    var  house = await dbContext.Houses.FirstOrDefaultAsync((h) => h.Id == houseId);
    if (house == null)
    {
        return Results.NotFound();
    }
    
    if (!httpContext.User.IsInRole(ForumRoles.Admin) &&
        httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub) != house.UserId)
    {
        // NotFound()
        return Results.Forbid();
    }

    house.Name = dto.Name;
    house.Region = dto.Region;
    house.District = dto.District;
    
    dbContext.Update(house);
    
    await dbContext.SaveChangesAsync();
    
    return Results.Ok(new HousesDto(house.Id,house.CreatedAt, house.Name,house.Region,house.District));
});

housesGroup.MapDelete("houses/{houseId}",[Authorize] async (int houseId,ForumDbContext dbContext,HttpContext httpContext) =>
{
    
    var  house = await dbContext.Houses.FirstOrDefaultAsync((h) => h.Id == houseId);
    if (house == null)
    {
        return Results.NotFound();
    }
    
    var userId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub); 
    var isAdmin = httpContext.User.IsInRole(ForumRoles.Admin); 
    var isRenterWhoCreated = house.UserId == userId && httpContext.User.IsInRole(ForumRoles.Renter); 

    if (!isAdmin && !isRenterWhoCreated)
    {
        return Results.Forbid(); 
    }
    dbContext.Remove(house);
    await dbContext.SaveChangesAsync();
    return Results.NoContent();

});


//////////////////////////////////////
/// /api/v1/houses/{houseId}/rooms GET List 200
/*
/api/v1/houses/{houseId}/rooms/{roomId} GET One 200
    /api/v1/houses/{houseId}/rooms POST Create 201
    /api/v1/houses/{houseId}/rooms/{roomId} PUT/PATCH Modify 200
    /api/v1/houses/{houseId}/rooms/{roomId} DELETE Remove 200/204
    */
//jei kuri groups validacija uztenka vienos

var roomsGroup = app.MapGroup("/api/houses/{houseId}").WithValidationFilter();

roomsGroup.MapGet("rooms", async (int houseId, ForumDbContext dbContext, CancellationToken cancellationToken) =>
{
    // Patikriname, ar nurodytas namas egzistuoja
    var houseExists = await dbContext.Houses.AnyAsync(h => h.Id == houseId, cancellationToken);

    if (!houseExists)
    {
        return Results.NotFound();
    }

    
    var rooms = await dbContext.Rooms
        .Where(r => r.Houses.Id == houseId)
        .Select(room => new RoomsDto(room.Id, room.Number, room.Description, room.Price))
        .ToListAsync(cancellationToken);

    return Results.Ok(rooms);
});

roomsGroup.MapGet("rooms/{roomId}", async (int houseId,int roomId, ForumDbContext dbContext) =>
{
    var  house = await dbContext.Houses.FirstOrDefaultAsync((h) => h.Id == houseId);
    if (house == null)
    {
        return Results.NotFound();
    }
    
    var  room = await dbContext.Rooms.FirstOrDefaultAsync((r) => r.Id == roomId && r.Houses.Id==houseId);
    if (room == null)
    {
        return Results.NotFound();
    }

    //return Results.Ok(room);jei norim rodyti rysius
     return Results.Ok(new RoomsDto(room.Id, room.Number, room.Description, room.Price));

});

roomsGroup.MapPost("rooms/",[Authorize(Roles = ForumRoles.Admin)] async (int houseId,[Validate]CreateRoomsDto createRoomsDto,HttpContext httpContext,ForumDbContext dbContext ) =>
{
    var house = await dbContext.Houses.FirstOrDefaultAsync(h => h.Id == houseId);

    if (house == null)
    {
        return Results.NotFound();
    }
    var rooms = new Rooms()
    {
        Number = createRoomsDto.Number,
        Description = createRoomsDto.Description,
        Price = createRoomsDto.Price,
        Houses = house,
        UserId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub)
    };
    
    dbContext.Rooms.Add(rooms);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/api/houses/{houseId}/rooms/{rooms.Id}",
        new RoomsDto(rooms.Id, rooms.Number, rooms.Description, rooms.Price));
});

roomsGroup.MapPut("rooms/{roomId}", [Authorize] async (int houseId, int roomId, [Validate] UpdateRoomsDto dto, ForumDbContext dbContext,HttpContext httpContext) =>
{
    var houseExists = await dbContext.Houses.AnyAsync(h => h.Id == houseId);
    if (!houseExists)
    {
        return Results.NotFound();
    }
    var room = await dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == roomId && r.Houses.Id == houseId);

   
    if (room == null)
    {
        return Results.NotFound();
    }
    
    var userId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub); 
    if (!httpContext.User.IsInRole(ForumRoles.Admin) && room.UserId != userId)
    {
        return Results.Forbid(); 
    }

    room.Number = dto.Number;
    room.Description = dto.Description;
    room.Price = dto.Price;

    dbContext.Rooms.Update(room);
    await dbContext.SaveChangesAsync();

    return Results.Ok(new RoomsDto(room.Id, room.Number, room.Description, room.Price));
});
//gal galima bet CancellationToken cancellationToken
roomsGroup.MapDelete("rooms/{roomId}",[Authorize] async (int houseId, int roomId,HttpContext httpContext, ForumDbContext dbContext) =>
{
    
    var houseExists = await dbContext.Houses.AnyAsync(h => h.Id == houseId);
    if (!houseExists)
    {
        return Results.NotFound();
    }

   
    var room = await dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == roomId && r.Houses.Id == houseId);
    
    if (room == null)
    {
        return Results.NotFound();
    }

    // var userId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub); 
    // if (!httpContext.User.IsInRole(ForumRoles.Admin) && room.UserId != userId)
    // {
    //     return Results.Forbid(); 
    // }
    
    var userId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub); 
    var isAdmin = httpContext.User.IsInRole(ForumRoles.Admin); 
    var isRenterWhoCreated = room.UserId == userId && httpContext.User.IsInRole(ForumRoles.Renter); 

    if (!isAdmin && !isRenterWhoCreated)
    {
        return Results.Forbid(); 
    }
    
    dbContext.Rooms.Remove(room);
    await dbContext.SaveChangesAsync();

    return Results.NoContent();
});


///////////// note ////////
/// /// /api/v1/houses/{houseId}/rooms GET List 200
/*
/api/v1/houses/{houseId}/rooms/{roomId}/notes GET List 200
/api/v1/houses/{houseId}/rooms/{roomId}/{noteId} GET One 200
    /api/v1/houses/{houseId}/rooms/notes POST Create 201
    /api/v1/houses/{houseId}/rooms/{roomId}/notes/{notesId} PUT/PATCH Modify 200
    /api/v1/houses/{houseId}/rooms/{roomId}/notes/{notesId} DELETE Remove 200/204
    */
var notesGroup = app.MapGroup("/api/houses/{houseId}/rooms/{roomId}").WithValidationFilter();

notesGroup.MapGet("notes", async (int houseId,int roomId, ForumDbContext dbContext, CancellationToken cancellationToken) =>
{
    // Patikriname, ar nurodytas namas egzistuoja
    var houseExists = await dbContext.Houses.AnyAsync(h => h.Id == houseId, cancellationToken);

    if (!houseExists)
    {
        return Results.NotFound();
    }
    
    var roomExists = await dbContext.Rooms.AnyAsync(r => r.Id == roomId && r.Houses.Id == houseId);
    if (!roomExists)
    {
        return Results.NotFound();
    }

    
    var notes = await dbContext.Notes
        .Where(n => n.Rooms.Id == roomId)
        .Select(note => new NotesDto(note.Id, note.Note))
        .ToListAsync();

    return Results.Ok(notes);
});


notesGroup.MapGet("notes/{noteId}", async (int houseId,int roomId,int noteId, ForumDbContext dbContext) =>
{
    var houseExists = await dbContext.Houses.AnyAsync(h => h.Id == houseId);
    if (!houseExists)
    {
        return Results.NotFound();
    }

    var roomExists = await dbContext.Rooms.AnyAsync(r => r.Id == roomId && r.Houses.Id == houseId);
    if (!roomExists)
    {
        return Results.NotFound();
    }

    var note = await dbContext.Notes
        .FirstOrDefaultAsync(n => n.Id == noteId && n.Rooms.Id == roomId);

    if (note == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(new NotesDto(note.Id, note.Note));
});



notesGroup.MapPost("notes/",[Authorize(Roles = ForumRoles.ForumUser)] async (int houseId, int roomId,[Validate]CreateNotesDto createNotesDto,HttpContext httpContext,ForumDbContext dbContext ) =>
{
    var userIsAuthorized = httpContext.User.IsInRole(ForumRoles.ForumUser) || httpContext.User.IsInRole(ForumRoles.Admin);
    if (!userIsAuthorized)
    {
        return Results.Forbid();
    }
    
    var house = await dbContext.Houses.FirstOrDefaultAsync(h => h.Id == houseId);

    if (house == null)
    {
        return Results.NotFound();
    }
    
    var room = await dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == roomId && r.Houses.Id == houseId);

    if (room == null)
    {
        return Results.NotFound();
    }
    
    var userId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
    var notes = new Notes
    {
        Note = createNotesDto.Note,  
        Rooms = room,
        UserId = userId
    };

    
    dbContext.Notes.Add(notes);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/api/houses/{houseId}/rooms/{roomId}/notes/{notes.Id}",
        new NotesDto(notes.Id, notes.Note));
});

notesGroup.MapPut("notes/{noteId}",[Authorize] async (int houseId, int roomId, int noteId, [Validate] UpdateNotesDto dto,HttpContext httpContext, ForumDbContext dbContext) =>
{
    
    var houseExists = await dbContext.Houses.AnyAsync(h => h.Id == houseId);
    if (!houseExists)
    {
        return Results.NotFound();
    }
    
    var room = await dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == roomId && r.Houses.Id == houseId);

   
    if (room == null)
    {
        return Results.NotFound();
    }

    
    var note = await dbContext.Notes.FirstOrDefaultAsync(n => n.Id == noteId && n.Rooms.Id == roomId);
    if (note == null)
    {
        return Results.NotFound();
    }
    
    var userId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
    var isAdmin = httpContext.User.IsInRole(ForumRoles.Admin);
    var isOwner = note.UserId == userId && (httpContext.User.IsInRole(ForumRoles.ForumUser) || httpContext.User.IsInRole(ForumRoles.Admin));

   
    // if (!isAdmin && !isOwner)
    // {
    //     return Results.Forbid();
    // }
    if(!httpContext.User.IsInRole(ForumRoles.Admin) && httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub) != note.UserId)
    {
        return Results.Forbid();
    }
    
    note.Note = dto.Note;

    dbContext.Notes.Update(note);
    await dbContext.SaveChangesAsync();

    return Results.Ok(new NotesDto(note.Id, note.Note));
});
//gal galima bet CancellationToken cancellationToken
notesGroup.MapDelete("notes/{noteId}",[Authorize] async (int houseId, int roomId, int noteId,HttpContext httpContext, ForumDbContext dbContext) =>
{
    
    var houseExists = await dbContext.Houses.AnyAsync(h => h.Id == houseId);
    if (!houseExists)
    {
        return Results.NotFound();
    }

   
    var room = await dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == roomId && r.Houses.Id == houseId);
    
    if (room == null)
    {
        return Results.NotFound();
    }
    
    var note = await dbContext.Notes.FirstOrDefaultAsync(n => n.Id == noteId && n.Rooms.Id == roomId);
    if (note == null)
    {
        return Results.NotFound();
    }

    var userId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
    var isAdmin = httpContext.User.IsInRole(ForumRoles.Admin);
    var isOwner = note.UserId == userId && (httpContext.User.IsInRole(ForumRoles.ForumUser) || httpContext.User.IsInRole(ForumRoles.Admin));
    
    // if (!isAdmin && !isOwner)
    // {
    //     return Results.Forbid();
    // }
    if (!httpContext.User.IsInRole(ForumRoles.Admin) && httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub) != note.UserId)
    {
        return Results.Forbid();
    }
    
    dbContext.Notes.Remove(note);
    await dbContext.SaveChangesAsync();

    return Results.NoContent();
});


//lab2 bib
// Microsoft.AspNetCore.Identity nebereik
// Microsoft.AspNetCore.Identity.EntityFrameworkCore
// Microsoft.AspNetCore.Authentication.JwtBearer

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();
app.Run();

