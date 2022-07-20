using Application.MainModule.DTO.Request;
using FluentValidation;

namespace Application.MainModule.DTO.Fluent
{
    public class SignInRequestValidator : AbstractValidator<SignInRequestDto>
    {
        public SignInRequestValidator()
        {
            RuleFor(p => p.Username)
                .NotNull()
                .NotEmpty()
                .Length(5, 100);
            RuleFor(p => p.Password)
                .NotNull()
                .NotEmpty()
                .Length(5, 100);
        }
    }
}