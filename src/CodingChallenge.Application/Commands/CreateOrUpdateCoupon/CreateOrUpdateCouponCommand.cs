namespace CodingChallenge.Application.Commands.CreateOrUpdateCoupon;

public record CreateOrUpdateCouponCommand(
    Guid Id,
    string Name,
    string Description,
    string Code,
    decimal Price,
    int MaxUsages,
    int Usages,
    IEnumerable<string> ProductCodes) : IRequest<CreateOrUpdateCouponCommandResponse>;
