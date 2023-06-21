using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CodingChallenge.Domain.CouponAggregate;

namespace CodingChallenge.Infrastructure.CouponAggregate;

public sealed class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .OwnsOne(x => x.Name)
            .Property(x => x.Value)
            .HasColumnName(nameof(Coupon.Name))
            .IsRequired()
            .HasMaxLength(CouponName.MaxLength);

        builder
            .OwnsOne(x => x.Description)
            .Property(x => x.Value)
            .HasColumnName(nameof(Coupon.Description))
            .IsRequired()
            .HasMaxLength(CouponDescription.MaxLength);

        builder
            .OwnsOne(x => x.Code)
            .Property(x => x.Value)
            .HasColumnName(nameof(Coupon.Code))
            .IsRequired();

        builder
            .OwnsOne(x => x.Code)
            .HasIndex(x => x.Value)
            .IsUnique();

        builder
            .OwnsOne(x => x.Price)
            .Property(x => x.Value)
            .HasColumnName(nameof(Coupon.Price))
            .IsRequired();

        builder
            .OwnsOne(x => x.Usage)
            .Property(x => x.MaxUsages)
            .HasColumnName(nameof(Coupon.Usage.MaxUsages))
            .IsRequired();

        builder
            .OwnsOne(x => x.Usage)
            .Property(x => x.Usages)
            .HasColumnName(nameof(Coupon.Usage.Usages))
            .IsRequired();

        builder
            .OwnsMany(
                x => x.ProductCodes,
                y =>
                {
                    y.ToTable(nameof(Coupon.ProductCodes));
                    y.WithOwner().HasForeignKey("CouponId");
                });

        builder
            .Property<byte[]>("Version")
            .IsRowVersion()
            .HasColumnName("Version");
    }
}