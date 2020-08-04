using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class ContactRequestConfiguration : IEntityTypeConfiguration<ContactRequest>
    {
        public void Configure(EntityTypeBuilder<ContactRequest> builder)
        {
            builder.ToTable("ContactRequest", "Request");
            builder.HasKey(p => p.Id)
                   .HasName("PK_ContactRequest");

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

            builder.HasOne(p => p.ContactRequestState)
                .WithMany(p => p.ContactRequests)
                .HasForeignKey(p => p.ContactRequestStateId)
                .HasConstraintName("FK_ContactRequest_ContactRequestState")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Customer)
                .WithMany(p => p.ContactRequests)
                .HasForeignKey(p => p.CustomerId)
                .HasConstraintName("FK_ContactRequest_Customer")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Table)
                .WithMany(p => p.ContactRequests)
                .HasForeignKey(p => p.TableId)
                .HasConstraintName("FK_ContactRequest_Table")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
