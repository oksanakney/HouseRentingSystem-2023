using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static HouseRentingSystem.Common.EntityValidationConstants;

namespace HouseRentingSystem.Data.Configurations
{
    using Models;
    public class HouseEntityConfiguration : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            //Here we can write fluent api
            builder
                .HasOne(h => h.Category)
                .WithMany(c => c.Houses)
                .HasForeignKey(h => h.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(h => h.Agent)
                .WithMany(a => a.OwnedHouses)
                .HasForeignKey(h => h.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(h => h.Renter)
                .WithMany(r => r.RentedHouses)
                .HasForeignKey(h => h.RenterId) 
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
