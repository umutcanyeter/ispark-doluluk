using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IsparkDoluluk.Entities.Concrete;

namespace IsparkDoluluk.DataAccess.Concrete.Mapping
{
    public class AppUserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseMySqlIdentityColumn();

            builder.Property(I => I.Username).HasMaxLength(100).IsRequired();
            builder.HasIndex(I => I.Username).IsUnique();
            
            builder.Property(I => I.Password).HasMaxLength(100).IsRequired();
            
            builder.HasMany(I => I.AppUserRoles).WithOne(I => I.AppUser).HasForeignKey(I => I.AppUserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}