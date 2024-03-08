using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Common.EntityValidationConstants.Agent;

namespace HouseRentingSystem.Data.Models
{
    public class Agent
    {
        public Agent()
        {
            this.OwnedHouses = new HashSet<House>();
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;

        public ICollection<House> OwnedHouses { get; set; }
    }
}

    //• Id – a unique integer, Primary Key
    //• PhoneNumber – a string with min length 7 and max length 15 (required)
    //• UserId – a string (required)
    //• User – an IdentityUser object