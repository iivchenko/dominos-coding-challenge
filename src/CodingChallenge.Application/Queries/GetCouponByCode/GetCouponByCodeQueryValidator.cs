namespace CodingChallenge.Application.Queries.GetCouponByCode;

public sealed class GetCouponByCodeQueryValidator : AbstractValidator<GetCouponByCodeQuery>
{
    public GetCouponByCodeQueryValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
    }
}
