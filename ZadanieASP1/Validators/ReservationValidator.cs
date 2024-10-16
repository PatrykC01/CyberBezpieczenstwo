using FluentValidation;
using ZadanieASP1.Models;

namespace ZadanieASP1.Validators
{
    public class ReservationValidator : AbstractValidator<Reservation>
    {
        public ReservationValidator()
        {
            RuleFor(x => x.DateOfDeparture).NotEmpty().WithMessage("Date of departure is required.")
                                     .GreaterThan(x => x.ReservationDate).WithMessage("Date of departure must be later than reservation date.")
                                     .LessThan(x => x.DateOfReturn).WithMessage("Date of departure must be earlier than return date.");

            RuleFor(x => x.ReservationDate).NotEmpty().WithMessage("Reservation date is required.")
                                     .LessThan(x => x.DateOfDeparture).WithMessage("Date of reservation must be earlier than date of departure.")
                                     .LessThan(x => x.DateOfReturn).WithMessage("Date of reservation must be earlier than date of return.");

            RuleFor(x => x.DateOfReturn).NotEmpty().WithMessage("Date of return is required.")
                                         .GreaterThan(x => x.ReservationDate).WithMessage("Date of return must be later than reservation date.")
                                         .GreaterThan(x => x.DateOfDeparture).WithMessage("Date of return must be later than departure date.");
        }
    }
}
