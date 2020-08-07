using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class UserBranchConfiguration : IEntityTypeConfiguration<UserBranch>
    {
        public void Configure(EntityTypeBuilder<UserBranch> builder)
        {
            builder.ToTable("UserBranch", "Identity");
            builder.HasKey(p => p.Id)
                   .HasName("PK_UserBranch");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.UserId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.BranchId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.IsActive)
                .HasColumnType("BIT")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");


            builder.HasOne(p => p.User)
                .WithMany(p => p.UserBranches)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_UserBranch_User")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Branch)
                .WithMany(p => p.UserBranches)
                .HasForeignKey(p => p.BranchId)
                .HasConstraintName("FK_UserBranch_Branch")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
