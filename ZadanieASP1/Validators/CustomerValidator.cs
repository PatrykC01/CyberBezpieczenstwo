using FluentValidation;
using ZadanieASP1.Models;

namespace ZadanieASP1.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("The First Name cannot be blank.")
                                    .Length(3, 100).WithMessage("The First Name cannot be less than 3 characters.");
            
            RuleFor(x => x.LastName).NotEmpty().WithMessage("The Last Name cannot be blank.")
                                    .Length(3, 100).WithMessage("The Last Name cannot be less than 3 characters.");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("The Phone Number cannot be blank.")
                                   .Matches(@"^\d{3}-\d{3}-\d{3}$").WithMessage("The Phone Number should be in the format xxx-xxx-xxx.");

        }
    }
}