using FluentValidation;
using ZadanieASP1.Models;

namespace ZadanieASP1.Validators
{
    public class TripValidator : AbstractValidator<Trip>
    {
        public TripValidator()
        {
            RuleFor(x => x.TripID).NotEmpty().WithMessage("The TripID cannot be blank.")
                                .GreaterThanOrEqualTo(1).WithMessage("The TripID must be positive number");

            RuleFor(x => x.Title).NotEmpty().WithMessage("The Title cannot be blank.")
                                    .Length(10, int.MaxValue).WithMessage("The Title cannot be less than 10 characters.");

            RuleFor(x => x.Description).NotEmpty().WithMessage("The Description cannot be blank.")
                                    .Length(50, int.MaxValue).WithMessage("The Description cannot be less than 50 characters.");

            RuleFor(x => x.Price).NotEmpty().WithMessage("The Price cannot be blank.")
                                   .GreaterThanOrEqualTo(1).WithMessage("The Price must be grater than 0.");

            RuleFor(x => x.Duration).NotEmpty().WithMessage("The Duration cannot be blank.")
                                   .Length(2, int.MaxValue).WithMessage("The Duration cannot be less than 2 characters.");
        }
    }
}
