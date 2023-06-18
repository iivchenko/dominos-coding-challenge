namespace CodingChallenge.Application.Queries.GetCouponById;

public record GetCouponByIdQueryResponse(
    Guid Id,
    string Name,
    string Description,
    string Code,
    decimal Price,
    int MaxUsages,
    int Usages,
    IEnumerable<string> ProductCodes);