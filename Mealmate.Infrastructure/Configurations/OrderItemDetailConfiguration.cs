using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class OrderItemDetailConfiguration : IEntityTypeConfiguration<OrderItemDetail>
    {
        public void Configure(EntityTypeBuilder<OrderItemDetail> builder)
        {
            builder.ToTable("OrderItemDetail", "Sale");
            builder.HasKey(p => p.Id)
                   .HasName("PK_OrderItemDetail");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.OrderItemId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.MenuItemOptionId)
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

            builder.HasOne(p => p.MenuItemOption)
                .WithMany(p => p.OrderItemDetails)
                .HasForeignKey(p => p.MenuItemOptionId)
                .HasConstraintName("FK_OrderItemDetail_MenuItemOption")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.OrderItem)
                .WithMany(p => p.OrderItemDetails)
                .HasForeignKey(p => p.OrderItemId)
                .HasConstraintName("FK_OrderItemDetail_OrderItem")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
