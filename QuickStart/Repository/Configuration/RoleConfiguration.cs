using Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData(
              new UserRole
              {
                  Name = "Admin",
                  NormalizedName = "ADMIN",
                  DateCreated = new DateTime(2015, 10, 13),
              },
              new UserRole
              {
                  Name = "User",
                  NormalizedName = "USER",
                  DateCreated = new DateTime(2015, 10, 13),
              }
            );
        }
    }
}
