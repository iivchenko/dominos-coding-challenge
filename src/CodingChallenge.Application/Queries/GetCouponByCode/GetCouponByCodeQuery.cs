namespace CodingChallenge.Application.Queries.GetCouponByCode;

public record GetCouponByCodeQuery(string Code) : IRequest<GetCouponByCodeQueryResponse>;