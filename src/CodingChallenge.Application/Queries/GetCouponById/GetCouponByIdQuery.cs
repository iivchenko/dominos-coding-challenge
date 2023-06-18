namespace CodingChallenge.Application.Queries.GetCouponById;

public record GetCouponByIdQuery (Guid Id) : IRequest<GetCouponByIdQueryResponse>;
