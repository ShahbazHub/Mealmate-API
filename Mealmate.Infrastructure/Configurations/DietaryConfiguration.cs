using Mealmate.Core.Entities;
using Mealmate.Core.Entities.Lookup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class DietaryConfiguration : IEntityTypeConfiguration<Dietary>
    {
        public void Configure(EntityTypeBuilder<Dietary> builder)
        {
            builder.ToTable("Dietary", "Lookup");
            builder.HasKey(p => p.Id)
                   .HasName("PK_Dietary");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasColumnType("NVARCHAR(250)")
                .IsRequired();

            builder.Property(p => p.Photo)
                .HasColumnType("VARBINARY(MAX)")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.IsActive)
                    .HasColumnType("BIT")
                    .IsRequired();

        }
    }
}
