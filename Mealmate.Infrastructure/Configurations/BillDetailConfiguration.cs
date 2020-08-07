using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class BillDetailConfiguration : IEntityTypeConfiguration<BillDetail>
    {
        public void Configure(EntityTypeBuilder<BillDetail> builder)
        {
            builder.ToTable("BillDetail", "Sale");
            builder.HasKey(p => p.Id)
                   .HasName("PK_BillDetail");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(p => p.BillId)
               .HasColumnType("INT")
               .IsRequired();

            builder.Property(p => p.OrderId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(p => p.Bill)
                .WithMany(p => p.BillDetails)
                .HasForeignKey(p => p.BillId)
                .HasConstraintName("FK_BillDetail_Bill")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
