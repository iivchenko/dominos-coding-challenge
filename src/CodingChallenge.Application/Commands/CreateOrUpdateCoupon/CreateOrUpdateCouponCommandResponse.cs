namespace CodingChallenge.Application.Commands.CreateOrUpdateCoupon;

public record CreateOrUpdateCouponCommandResponse(
    CreateOrUpdateCouponCommandResponseStatus Status,
    Guid Id,
    string Name,
    string Description,
    string Code,
    decimal Price,
    int MaxUsages,
    int Usages,
    IEnumerable<string> ProductCodes);

public enum CreateOrUpdateCouponCommandResponseStatus
{
    Created,
    Updated
}