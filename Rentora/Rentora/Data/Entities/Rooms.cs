using System.ComponentModel.DataAnnotations;
using Rentora.Auth.Model;

namespace Rentora.Data.Entities;
using FluentValidation;
using Rentora.Data;
public class Rooms
{
    
    public int Id { get; set; }
    public required int Number { get; set; }
    public required string Description { get; set; }
    public required int Price { get; set; }
    
    //public required int FkHouse { get; set; }
    
    public required Houses Houses { get; set; }
    
    [Required]
    public required string UserId { get; set; }
    
    public ForumUser User { get; set; }
 
}
public record RoomsDto(int Id,int Number,string Description,int Price);
public record CreateRoomsDto(int Number,string Description,int Price);
public record UpdateRoomsDto(int Number,string Description,int Price);

public class CreateRoomsDtoValidator : AbstractValidator<CreateRoomsDto>
{
    public  CreateRoomsDtoValidator ()
    {
        RuleFor(dto => dto.Number ).NotEmpty ().NotNull().GreaterThan (0);
        RuleFor(dto => dto.Description ).NotEmpty ().NotNull().Length (min: 2,max: 100);
        RuleFor(dto => dto.Price ).NotEmpty ().NotNull().GreaterThan (0);
    }
}
public class UpdateRoomsDtoValidator : AbstractValidator<UpdateRoomsDto>
{
    public  UpdateRoomsDtoValidator ()
    {
        RuleFor(dto => dto.Number ).NotNull().GreaterThan (0);
        RuleFor(dto => dto.Description ).NotEmpty ().NotNull().Length (min: 2,max: 100);
        RuleFor(dto => dto.Price ).GreaterThan (0);//
    }
}






