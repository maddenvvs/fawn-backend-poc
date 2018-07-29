using FluentValidation;

namespace Fawn.WebAPI.Models.Orders
{
    public class NewOrderValidator : AbstractValidator<NewOrderApiDTO>
    {
        public NewOrderValidator()
        {
            RuleFor(o => o.Goods)
                .NotEmpty()
                .WithMessage("You must provide at least one goods item to create an order.");
        }
    }
}