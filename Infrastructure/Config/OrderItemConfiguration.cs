using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(p => p.Quantity).IsRequired();
        //builder.Property(p => p.Price).IsRequired();
        //builder.HasOne(p => p.Track).WithMany().HasForeignKey(p => p.TrackId);
        //builder.HasOne(p => p.TrackMedium).WithMany().HasForeignKey(p => p.Id);
    }
}