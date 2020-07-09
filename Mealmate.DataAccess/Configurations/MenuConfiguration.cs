using Mealmate.DataAccess.Entities.Mealmate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.DataAccess.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menu", "Mealmate");
            builder.HasKey(p => p.MenuId)
                   .HasName("PK_Menu");

            builder.Property(p => p.MenuId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.BranchId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnType("NVARCHAR(250)")
                .IsRequired();

            builder.Property(p => p.ServiceTime)
                .HasColumnType("TIME(7)")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");


            builder.HasOne(p => p.Branch)
                .WithMany(p => p.Menus)
                .HasForeignKey(p => p.BranchId)
                .HasConstraintName("FK_Menu_Branch")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
