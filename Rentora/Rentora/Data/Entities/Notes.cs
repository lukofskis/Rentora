using System.ComponentModel.DataAnnotations;
using Rentora.Auth.Model;

namespace Rentora.Data.Entities;
using FluentValidation;
public class Notes
{
    public int Id { get; set; }
    public required string Note { get; set; }
    
    //public required int FkRoom { get; set; }
    
    public required Rooms Rooms { get; set; }
    
    [Required]
    public required string UserId { get; set; }
    
    public ForumUser User { get; set; }
}

public record NotesDto(int Id, string Note);
public record CreateNotesDto(string Note);
public record UpdateNotesDto(string Note);

public class CreateNotesDtoValidator : AbstractValidator<CreateNotesDto>
{
    public CreateNotesDtoValidator()
    {
        RuleFor(dto => dto.Note).NotEmpty().NotNull().Length(2, 200);
        
    }
}

public class UpdateNotesDtoValidator : AbstractValidator<UpdateNotesDto>
{
    public UpdateNotesDtoValidator()
    {
        RuleFor(dto => dto.Note).NotEmpty().NotNull().Length(2, 200);
       
    }
}



