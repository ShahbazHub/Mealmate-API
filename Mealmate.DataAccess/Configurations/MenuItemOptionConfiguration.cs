using Mealmate.DataAccess.Entities.Mealmate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.DataAccess.Configurations
{
    public class MenuItemOptionConfiguration : IEntityTypeConfiguration<MenuItemOption>
    {
        public void Configure(EntityTypeBuilder<MenuItemOption> builder)
        {
            builder.ToTable("MenuItemOption", "Mealmate");
            builder.HasKey(p => p.MenuItemOptionId)
                   .HasName("PK_MenuItemOption");

            builder.Property(p => p.MenuItemOptionId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.MenuItemId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.OptionItemId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.Quantity)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.Price)
                .HasColumnType("DECIMAL(10, 2)")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");


            builder.HasOne(p => p.MenuItem)
                .WithMany(p => p.MenuItemOptions)
                .HasForeignKey(p => p.MenuItemId)
                .HasConstraintName("FK_MenuItemOption_MenuItem")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.OptionItem)
                .WithMany(p => p.MenuItemOptions)
                .HasForeignKey(p => p.OptionItemId)
                .HasConstraintName("FK_MenuItemOption_OptionItem")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
