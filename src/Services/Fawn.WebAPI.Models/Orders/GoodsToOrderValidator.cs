using FluentValidation;

namespace Fawn.WebAPI.Models.Orders
{
    public class GoodsToOrderValidator : AbstractValidator<GoodsToOrderApiDTO>
    {
        public GoodsToOrderValidator()
        {
            RuleFor(g => g.Amount)
                .GreaterThan(0);

            RuleFor(g => g.ItemPrice)
                .GreaterThanOrEqualTo(0m);
        }
    }
}