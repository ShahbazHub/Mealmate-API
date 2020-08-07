using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bill", "Sale");
            builder.HasKey(p => p.Id)
                   .HasName("PK_Bill");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            
            builder.Property(p => p.InvoiceNumber)
               .HasColumnType("NVARCHAR(50)")
               .IsRequired();

            builder.Property(p => p.BillRequestId)
               .HasColumnType("INT")
               .IsRequired();

            builder.Property(p => p.BillStateId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(p => p.BillState)
                .WithMany(p => p.Bills)
                .HasForeignKey(p => p.BillStateId)
                .HasConstraintName("FK_Bill_BillState")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.BillRequest)
                .WithOne(p => p.Bill)
                .HasForeignKey<Bill>(p => p.BillRequestId)
                .HasConstraintName("FK_Bill_BillRequest")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
