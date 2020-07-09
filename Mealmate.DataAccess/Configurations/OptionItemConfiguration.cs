using Mealmate.DataAccess.Entities.Lookup;
using Mealmate.DataAccess.Entities.Mealmate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.DataAccess.Configurations
{
    public class OptionItemConfiguration : IEntityTypeConfiguration<OptionItem>
    {
        public void Configure(EntityTypeBuilder<OptionItem> builder)
        {
            builder.ToTable("OptionItem", "Lookup");
            builder.HasKey(p => p.OptionItemId)
                   .HasName("PK_OptionItem");

            builder.Property(p => p.OptionItemId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasColumnType("NVARCHAR(250)")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
