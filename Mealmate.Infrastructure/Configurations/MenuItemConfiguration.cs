using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.ToTable("MenuItem", "Mealmate");
            builder.HasKey(p => p.Id)
                   .HasName("PK_MenuItem");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.MenuId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnType("NVARCHAR(250)")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");


            builder.HasOne(p => p.Menu)
                .WithMany(p => p.MenuItems)
                .HasForeignKey(p => p.MenuId)
                .HasConstraintName("FK_MenuItem_Menu")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
