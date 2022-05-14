using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(p => p.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(p => p.OrderDate).IsRequired();
        builder.Property(p => p.Subtotal).IsRequired();
        builder.Property(p => p.Comment).IsRequired();
        builder.Property(p => p.PaymentMethod).IsRequired();
        builder.Property(p => p.Payment).IsRequired();
        //builder.HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId);
        //builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
    }
}