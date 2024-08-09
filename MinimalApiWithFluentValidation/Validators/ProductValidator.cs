using FluentValidation;
using MinimalApiWithFluentValidation.Dtos;

namespace MinimalApiWithFluentValidation.Validators;

public class ProductValidator : AbstractValidator<ProductRequest>
{
    public ProductValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3)
            .MaximumLength(10)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");
    }
}
