using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class OptionItemConfiguration : IEntityTypeConfiguration<OptionItem>
    {
        public void Configure(EntityTypeBuilder<OptionItem> builder)
        {
            builder.ToTable("OptionItem", "Lookup");
            builder.HasKey(p => p.Id)
                   .HasName("PK_OptionItem");

            builder.Property(p => p.Id)
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
