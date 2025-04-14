using FluentValidation;
using ToDoListManagement.Bll.DTOs;

namespace ToDoListManagement.Bll.Validators;

public class ToDoListGetDtoValidator : AbstractValidator<ToDoListGetDto>
{
    public ToDoListGetDtoValidator()
    {
        RuleFor(x => x.Title)
                .NotEmpty().WithMessage("ToDoList Title  is required.")
                .Length(1, 200).WithMessage("ToDoList Title must be between 1 and 200 characters.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(10, 1000).WithMessage("Description must be between 10 and 200 characters.");
    }
}
