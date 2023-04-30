using FluentValidation;

namespace Application.Features.Auth.Dtos;
public record LoginRequest(string userName, string password);

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(l => l.userName).NotNull().NotEmpty();
        RuleFor(l => l.password).NotNull().MinimumLength(6).Matches(@"[0-9]+");
    }

}
