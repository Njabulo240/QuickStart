using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData
            (
                new Customer
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    FirstName = "Njabulo",
                    LastName = "Mamba",
                    DateOfBirth = new DateTime(1995, 03, 31),
                    Address = "583 Wall Dr. Gwynn Oak, MD 21207"
                },
                new Customer
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    FirstName = "Mfuneko",
                    LastName = "Maziya",
                    DateOfBirth = new DateTime(1997, 11, 15),
                    Address = "312 Forest Avenue, BF 923"
                }
            );
        }
    }
}
