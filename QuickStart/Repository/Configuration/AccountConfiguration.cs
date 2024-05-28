using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasData
            (
                new Account
                {
                    Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                    DateCreated = new DateTime(2015, 10, 13),
                    AccountType = "Domestic",
                    CustomerId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                },
                new Account
                {
                    Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                    DateCreated = new DateTime(2015, 09, 11),
                    AccountType = "Savings",
                    CustomerId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                },
                 new Account
                 {
                     Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                     DateCreated = new DateTime(2022, 07, 22),
                     AccountType = "Foreign",
                     CustomerId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                 }
            );
        }
    }
}
