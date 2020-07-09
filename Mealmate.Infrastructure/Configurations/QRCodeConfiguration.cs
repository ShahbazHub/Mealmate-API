using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class QRCodeConfiguration : IEntityTypeConfiguration<QRCode>
    {
        public void Configure(EntityTypeBuilder<QRCode> builder)
        {
            builder.ToTable("QRCode", "Mealmate");
            builder.HasKey(p => p.Id)
                   .HasName("PK_QRCode");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.TableId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.Code)
                .HasColumnType("VARBINARY(MAX)")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(p => p.Table)
                .WithMany(p => p.QRCodes)
                .HasForeignKey(p => p.TableId)
                .HasConstraintName("FK_QRCode_Table")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
