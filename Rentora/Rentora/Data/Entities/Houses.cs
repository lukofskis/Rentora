using System.ComponentModel.DataAnnotations;
using Rentora.Auth.Model;

namespace Rentora.Data.Entities;
using FluentValidation;
public class Houses
{
    public int Id { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
    public required string Name { get; set; }
    public required string Region { get; set; }
    public required string District { get; set; }
    
    //public required string ImageUrl { get; set; }
    //public required string Location_Description { get; set; }
    //public int? WifiSpeed { get; set; }
    //public required int FkRenter { get; set; }
 
    [Required]
    public required string UserId { get; set; }
    
    public ForumUser User { get; set; }
    
}
public record HousesDto(int Id,DateTimeOffset CreatedAt,string Name,string Region,string District);
public record CreateHousesDto(string Name, string Region,string District);
public record UpdateHousesDto(string Name, string Region,string District);

public class CreateHousesDtoValidator : AbstractValidator<CreateHousesDto>
{
    public  CreateHousesDtoValidator ()
    {
        RuleFor(dto => dto.Name ).NotEmpty ().NotNull().Length (min: 2,max: 100);
        RuleFor(dto => dto.Region ).NotEmpty ().NotNull().Length (min: 5,max: 100);
        RuleFor(dto => dto.District ).NotEmpty ().NotNull().Length (min: 5,max: 100);
    }
}
public class UpdateHousesDtoValidator : AbstractValidator<UpdateHousesDto>
{
    public  UpdateHousesDtoValidator ()
    {
        RuleFor(dto => dto.Name ).NotEmpty ().NotNull().Length (min: 2,max: 100);
        RuleFor(dto => dto.Region ).NotEmpty ().NotNull().Length (min: 5,max: 100);
        RuleFor(dto => dto.District ).NotEmpty ().NotNull().Length (min: 5,max: 100);
    }
}
