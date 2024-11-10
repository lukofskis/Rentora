using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using O9d.AspNet.FluentValidation;
using Rentora.Data;
using Rentora.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

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
var app = builder.Build();
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

housesGroup.MapPost("houses/", async ([Validate]CreateHousesDto createHousesDto,ForumDbContext dbContext ) =>
{
    var houses = new Houses()
    {
        CreatedAt = DateTime.UtcNow,
        Name = createHousesDto.Name,
        Region = createHousesDto.Region,
        District = createHousesDto.District,

    };
    dbContext.Houses.Add(houses);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/api/houses/{houses.Id}",
        new HousesDto(houses.Id, houses.CreatedAt, houses.Name, houses.Region, houses.District));
});
housesGroup.MapPut("houses/{houseId}", async(int houseId,[Validate]UpdateHousesDto dto ,ForumDbContext dbContext  ) =>
{
    var  house = await dbContext.Houses.FirstOrDefaultAsync((h) => h.Id == houseId);
    if (house == null)
    {
        return Results.NotFound();
    }
    house.Name = dto.Name;
    house.Region = dto.Region;
    house.District = dto.District;
    
    dbContext.Update(house);
    
    await dbContext.SaveChangesAsync();
    
    return Results.Ok(new HousesDto(house.Id,house.CreatedAt, house.Name,house.Region,house.District));
});

housesGroup.MapDelete("houses/{houseId}", async (int houseId,ForumDbContext dbContext) =>
{
    
    var  house = await dbContext.Houses.FirstOrDefaultAsync((h) => h.Id == houseId);
    if (house == null)
    {
        return Results.NotFound();
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

roomsGroup.MapPost("rooms/", async (int houseId,[Validate]CreateRoomsDto createRoomsDto,ForumDbContext dbContext ) =>
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
        Houses = house
    };
    
    dbContext.Rooms.Add(rooms);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/api/houses/{houseId}/rooms/{rooms.Id}",
        new RoomsDto(rooms.Id, rooms.Number, rooms.Description, rooms.Price));
});

roomsGroup.MapPut("rooms/{roomId}", async (int houseId, int roomId, [Validate] UpdateRoomsDto dto, ForumDbContext dbContext) =>
{
    // Patikriname, ar nurodytas `House` egzistuoja
    var houseExists = await dbContext.Houses.AnyAsync(h => h.Id == houseId);
    if (!houseExists)
    {
        return Results.NotFound();
    }

    // Ieškome kambario pagal `roomId` ir `houseId`
    var room = await dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == roomId && r.Houses.Id == houseId);

   
    if (room == null)
    {
        return Results.NotFound();
    }

    room.Number = dto.Number;
    room.Description = dto.Description;
    room.Price = dto.Price;

    dbContext.Rooms.Update(room);
    await dbContext.SaveChangesAsync();

    return Results.Ok(new RoomsDto(room.Id, room.Number, room.Description, room.Price));
});
//gal galima bet CancellationToken cancellationToken
roomsGroup.MapDelete("rooms/{roomId}", async (int houseId, int roomId, ForumDbContext dbContext) =>
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



notesGroup.MapPost("notes/", async (int houseId, int roomId,[Validate]CreateNotesDto createNotesDto,ForumDbContext dbContext ) =>
{
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
    
    
    var notes = new Notes
    {
        Note = createNotesDto.Note,  
        Rooms = room
    };

    
    dbContext.Notes.Add(notes);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/api/houses/{houseId}/rooms/{roomId}/notes/{notes.Id}",
        new NotesDto(notes.Id, notes.Note));
});

notesGroup.MapPut("notes/{noteId}", async (int houseId, int roomId, int noteId, [Validate] UpdateNotesDto dto, ForumDbContext dbContext) =>
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
    note.Note = dto.Note;

    dbContext.Notes.Update(note);
    await dbContext.SaveChangesAsync();

    return Results.Ok(new NotesDto(note.Id, note.Note));
});
//gal galima bet CancellationToken cancellationToken
notesGroup.MapDelete("notes/{noteId}", async (int houseId, int roomId, int noteId, ForumDbContext dbContext) =>
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

    
    dbContext.Notes.Remove(note);
    await dbContext.SaveChangesAsync();

    return Results.NoContent();
});



app.Run();

