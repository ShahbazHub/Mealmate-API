using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order", "Sale");
            builder.HasKey(p => p.Id)
                   .HasName("PK_Order");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.CustomerId)
                .HasColumnType("INT")
                .IsRequired();
            builder.Property(p => p.TableId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.OrderNumber)
                .HasColumnType("NVARCHAR(150)")
                .IsRequired();

            builder.Property(p => p.OrderDate)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(p => p.Table)
                .WithMany(p => p.Orders)
                .HasForeignKey(p => p.TableId)
                .HasConstraintName("FK_Order_Table")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Customer)
                .WithMany(p => p.Orders)
                .HasForeignKey(p => p.CustomerId)
                .HasConstraintName("FK_Order_Customer")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
