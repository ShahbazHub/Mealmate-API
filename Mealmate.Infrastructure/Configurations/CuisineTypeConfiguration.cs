using Mealmate.Core.Entities;
using Mealmate.Core.Entities.Lookup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class CuisineTypeConfiguration : IEntityTypeConfiguration<CuisineType>
    {
        public void Configure(EntityTypeBuilder<CuisineType> builder)
        {
            builder.ToTable("CuisineType", "Lookup");
            builder.HasKey(p => p.Id)
                   .HasName("PK_CuisineType");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasColumnType("NVARCHAR(250)")
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
