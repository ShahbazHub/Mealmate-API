using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class OptionItemAllergenConfiguration : IEntityTypeConfiguration<OptionItemAllergen>
    {
        public void Configure(EntityTypeBuilder<OptionItemAllergen> builder)
        {
            builder.ToTable("OptionItemAllergen", "Lookup");
            builder.HasKey(p => p.Id)
                   .HasName("PK_OptionItemAllergen");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.OptionItemId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");


            builder.HasOne(p => p.OptionItem)
                .WithMany(p => p.OptionItemAllergens)
                .HasForeignKey(p => p.OptionItemId)
                .HasConstraintName("FK_OptionItemAllergen_OptionItem")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
