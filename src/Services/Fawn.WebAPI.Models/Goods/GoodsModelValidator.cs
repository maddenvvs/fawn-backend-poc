using FluentValidation;

namespace Fawn.WebAPI.Models.Goods
{
    public class GoodsModelValidator : AbstractValidator<GoodsModel>
    {
        public GoodsModelValidator()
        {
            RuleFor(g => g.Name)
                .NotEmpty()
                .WithMessage("Goods name cannot be empty.");

            RuleFor(g => g.Name)
                .MaximumLength(200)
                .WithMessage("Goods name is cannot exceed 200 characters.");

            RuleFor(g => g.Description)
                .MaximumLength(4000)
                .WithMessage("Goods name is cannot exceed 4000 characters.");
        }
    }
}