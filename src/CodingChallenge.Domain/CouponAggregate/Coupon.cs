using System.Collections.ObjectModel;

namespace CodingChallenge.Domain.CouponAggregate;

public sealed class Coupon : IAggregateRoot<Guid>
{
    private Coupon()
    {
    }

    private Coupon(
        Guid id, 
        CouponName name, 
        CouponDescription description, 
        CouponCode code, 
        CouponPrice price, 
        CouponUsage usage,
        IReadOnlyCollection<CouponProductCode> productCodes)
    {
        if (id == Guid.Empty)
        {
            throw new DomainException("A coupon id can't be empty!");
        }

        ValidateProductCodes(productCodes);

        Id = id;
        Name = name;
        Description = description;
        Code = code;
        Price = price;
        Usage = usage;
        ProductCodes = productCodes;
    }

    public Guid Id { get; private set; }
    public CouponName Name { get; private set; }
    public CouponDescription Description { get; private set; }
    public CouponCode Code { get; private set; }
    public CouponPrice Price { get; private set; }
    public CouponUsage Usage { get; private set; }
    public IReadOnlyCollection<CouponProductCode> ProductCodes { get; private set; }

    public void UpdateName(CouponName name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public void UpdateDescription(CouponDescription description)
    {
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }

    public void UpdatePrice(CouponPrice price)
    {
        Price = price ?? throw new ArgumentNullException(nameof(price));
    }

    public void UpdateMaxUsages(int maxUsages)
    {
        if (Usage.MaxUsages != maxUsages)
        {
            if (maxUsages >= Usage.Usages || maxUsages == 0)
            {
                Usage = new CouponUsage(maxUsages, Usage.Usages);
            }
            else
            {
                throw new DomainException($"Can't update max usages for Coupon with id '{Id}' as expected max usages '{maxUsages}' less than actual usages '{Usage.Usages}'!");
            }
        }
    }

    public void IncrementUsage()
    {
        if (Usage.MaxUsages == 0 || Usage.Usages < Usage.MaxUsages)
        {
            Usage = new CouponUsage(Usage.MaxUsages, Usage.Usages + 1);
        }
        else
        {
            throw new DomainException($"Can't increment usages for Coupon with id '{Id}' as Coupon already depleted all available usages: max usages '{Usage.MaxUsages}', usages '{Usage.Usages}'!");
        }
    }

    public void DecrementUsage()
    {
        Usage = new CouponUsage(Usage.MaxUsages, Usage.Usages - 1);        
    }

    public void UpdateProductCodes(IEnumerable<CouponProductCode> productCodes)
    {
        ValidateProductCodes(productCodes);

        ProductCodes = new ReadOnlyCollection<CouponProductCode>(productCodes.ToArray());
    }

    private static void ValidateProductCodes(IEnumerable<CouponProductCode> productCodes)
    {
        if (productCodes == null || !productCodes.Any())
        {
            throw new DomainException("A coupon product codes can't be null or empty!");
        }

        var duplicates = productCodes.GroupBy(x => x).Where(x => x.Count() > 1);

        if (duplicates.Any())
        {
            throw new DomainException($"""A coupon product codes have duplicates: {string.Join(", ", duplicates.Select(x => x.Key.Value))}!""");
        }
    }

    public static Coupon Create(
        Guid id, 
        string name, 
        string description, 
        string code, 
        decimal price,
        int maxUsages,
        IEnumerable<string> productCodes)
    {
        return new Coupon(
            id,
            name,
            description,
            code,
            price,
            new CouponUsage(maxUsages, 0),
            productCodes.ToProductCodes());
    }
}
