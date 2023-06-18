using CodingChallenge.Domain.CouponAggregate;

namespace CodingChallenge.Application.Commands.CreateOrUpdateCoupon;

public sealed class CreateOrUpdateCouponCommandValidator : AbstractValidator<CreateOrUpdateCouponCommand>
{
    public CreateOrUpdateCouponCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(CouponName.MaxLength);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(CouponDescription.MaxLength);
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.MaxUsages).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Usages).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Usages).LessThanOrEqualTo(x => x.MaxUsages).When(x => x.MaxUsages > 0);
        RuleFor(x => x.ProductCodes).NotEmpty();
    }
}
