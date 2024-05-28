using Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<User>();
            builder.HasData(
                new User
                {
                    Id = "dbeb0889-5348-489e-a611-ec710841d0fa",
                    FirstName = "AdminFirstName",
                    LastName = "AdminLastName",
                    UserName = "admin001",
                    Email = "admin001@matech.com",
                    PasswordHash = hasher.HashPassword(null, "AdminPassword123")
                },
                new User
                {
                    Id = "92db8399-e34b-4bda-9e95-21cdc39c11fa",
                    FirstName = "UserFirstName",
                    LastName = "UserLastName",
                    UserName = "user002",
                    Email = "user002@matech.com",
                    PasswordHash = hasher.HashPassword(null, "UserPassword123")
                }
                );
        }
    }

}
