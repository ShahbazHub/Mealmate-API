using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class BillRequestConfiguration : IEntityTypeConfiguration<BillRequest>
    {
        public void Configure(EntityTypeBuilder<BillRequest> builder)
        {
            builder.ToTable("BillRequest", "Request");
            builder.HasKey(p => p.Id)
                   .HasName("PK_BillRequest");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.CustomerId)
               .HasColumnType("INT")
               .IsRequired();

            builder.Property(p => p.TableId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.RequestTime)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.Remarks)
                    .HasColumnType("NVARCHAR(500)");

            builder.Property(p => p.ResponseTime)
                .HasColumnType("DATETIMEOFFSET");

            builder.HasOne(p => p.BillRequestState)
                .WithMany(p => p.BillRequests)
                .HasForeignKey(p => p.BillRequestStateId)
                .HasConstraintName("FK_BillRequest_BillRequestState")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Customer)
                .WithMany(p => p.BillRequests)
                .HasForeignKey(p => p.CustomerId)
                .HasConstraintName("FK_BillRequest_Customer")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Table)
                .WithMany(p => p.BillRequests)
                .HasForeignKey(p => p.TableId)
                .HasConstraintName("FK_BillRequest_Table")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
