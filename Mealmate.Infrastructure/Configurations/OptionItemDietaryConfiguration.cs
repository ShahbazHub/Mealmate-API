using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class OptionItemDietaryConfiguration : IEntityTypeConfiguration<OptionItemDietary>
    {
        public void Configure(EntityTypeBuilder<OptionItemDietary> builder)
        {
            builder.ToTable("OptionItemDietary", "Lookup");
            builder.HasKey(p => p.Id)
                   .HasName("PK_OptionItemDietary");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.OptionItemId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.IsActive)
                    .HasColumnType("BIT")
                    .IsRequired();


            builder.HasOne(p => p.OptionItem)
                .WithMany(p => p.OptionItemDietaries)
                .HasForeignKey(p => p.OptionItemId)
                .HasConstraintName("FK_OptionItemDietary_OptionItem")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
