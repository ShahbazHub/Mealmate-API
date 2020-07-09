using Mealmate.DataAccess.Entities.Mealmate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.DataAccess.Configurations
{
    public class QRCodeConfiguration : IEntityTypeConfiguration<QRCode>
    {
        public void Configure(EntityTypeBuilder<QRCode> builder)
        {
            builder.ToTable("QRCode", "Mealmate");
            builder.HasKey(p => p.QRCodeId)
                   .HasName("PK_QRCode");

            builder.Property(p => p.QRCodeId)
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
