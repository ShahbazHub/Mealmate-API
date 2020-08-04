using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class RestroomRequestConfiguration : IEntityTypeConfiguration<RestroomRequest>
    {
        public void Configure(EntityTypeBuilder<RestroomRequest> builder)
        {
            builder.ToTable("RestroomRequest", "Request");
            builder.HasKey(p => p.Id)
                   .HasName("PK_RestroomRequest");

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

            builder.HasOne(p => p.RestRoomRequestState)
                .WithMany(p => p.RestroomRequests)
                .HasForeignKey(p => p.RestroomRequestStateId)
                .HasConstraintName("FK_RestroomRequest_RestroomRequestState")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Customer)
                .WithMany(p => p.RestroomRequests)
                .HasForeignKey(p => p.CustomerId)
                .HasConstraintName("FK_RestroomRequest_Customer")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Table)
                .WithMany(p => p.RestroomRequests)
                .HasForeignKey(p => p.TableId)
                .HasConstraintName("FK_RestroomRequest_Table")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
