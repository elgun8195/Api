using FluentValidation;

namespace WebApplication1.Dto
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CheckPassword { get; set; }
    }

    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.UserName).MinimumLength(4).MaximumLength(10).NotEmpty();
            RuleFor(x => x.FullName).MinimumLength(4).MaximumLength(10).NotEmpty();
            RuleFor(x => x.Password).MinimumLength(8).NotEmpty();
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password!=x.CheckPassword)
                {
                    context.AddFailure("CheckPassword", "Password does not match");
                }
            });
        }
    }
}
