using FluentValidation;
using StudentWebPortal.Model.Dto;
using StudentWebPortal.Validators.Common;

namespace StudentWebPortal.Validators.Student
{
    public class CreateStudentDtoValidator : AbstractValidator<StudentCreateDto>
    {
        public CreateStudentDtoValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .ValidPhoneNumber();
        }
    }
}
