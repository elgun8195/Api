

using FluentValidation;

namespace WebApplication1.Dto
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserName).MinimumLength(4).MaximumLength(10);
            RuleFor(x => x.Password).MinimumLength(8).NotEmpty();
        }
    }
}
