using FluentValidation;
using StudentWebPortal.Model.Dto;
using StudentWebPortal.Validators.Common;

namespace StudentWebPortal.Validators.Student
{
    public class UpdateStudentDtoValidator : AbstractValidator<UpdateStudentDto>
    {
        public UpdateStudentDtoValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .ValidPhoneNumber()
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        }
    }
}
