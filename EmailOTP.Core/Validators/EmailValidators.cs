using FluentValidation;


namespace EmailOTP.Domain.Validators
{
    public class EmailValidators: AbstractValidator<string>
    {
        public EmailValidators() { 

            RuleFor(email=> email).NotEmpty().EmailAddress().Must(email => email.EndsWith("@dso.org.sg"))
                            .WithMessage("Only emails from the .dso.org.sg domain are allowed.");
        }
    }
}
