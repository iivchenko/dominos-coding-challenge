namespace CodingChallenge.Application.Queries.GetCouponByCode;

public record GetCouponByCodeQueryResponse(
    Guid Id,
    string Name,
    string Description,
    string Code,
    decimal Price,
    int MaxUsages,
    int Usages,
    IEnumerable<string> ProductCodes);