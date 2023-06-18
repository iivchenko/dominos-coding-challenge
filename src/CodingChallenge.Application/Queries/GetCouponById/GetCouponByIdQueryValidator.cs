namespace CodingChallenge.Application.Queries.GetCouponById;

public sealed class GetCouponByIdQueryValidator : AbstractValidator<GetCouponByIdQuery>
{
    public GetCouponByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

