using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem", "Sale");
            builder.HasKey(p => p.Id)
                   .HasName("PK_OrderItem");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.OrderId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.MenuItemId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.Price)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(p => p.Quantity)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(p => p.MenuItem)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(p => p.MenuItemId)
                .HasConstraintName("FK_OrderItem_MenuItem")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Order)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(p => p.OrderId)
                .HasConstraintName("FK_OrderItem_Order")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
