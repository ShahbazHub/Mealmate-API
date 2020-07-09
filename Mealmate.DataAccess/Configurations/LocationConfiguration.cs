using Mealmate.DataAccess.Entities.Mealmate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.DataAccess.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Location", "Mealmate");
            builder.HasKey(p => p.LocationId)
                   .HasName("PK_Location");

            builder.Property(p => p.LocationId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.BranchId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnType("NVARCHAR(250)")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(p => p.Branch)
                .WithMany(p => p.Locations)
                .HasForeignKey(p => p.BranchId)
                .HasConstraintName("FK_Location_Branch")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
