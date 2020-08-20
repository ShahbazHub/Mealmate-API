using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branch", "Mealmate");
            builder.HasKey(p => p.Id)
                   .HasName("PK_Branch");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.RestaurantId)
                    .HasColumnType("INT")
                    .IsRequired();

            builder.Property(p => p.IsActive)
                    .HasColumnType("BIT")
                    .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnType("NVARCHAR(250)")
                .IsRequired();

            builder.Property(p => p.ContactNumber)
                .HasColumnType("NVARCHAR(50)");

            builder.Property(p => p.Address)
                .HasColumnType("NVARCHAR(1000)");

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(p => p.Restaurant)
                .WithMany(p => p.Branches)
                .HasForeignKey(p => p.RestaurantId)
                .HasConstraintName("FK_Branch_Restaurant")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
