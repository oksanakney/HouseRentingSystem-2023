using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Common.EntityValidationConstants.House;

namespace HouseRentingSystem.Data.Models
{
    public class House
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        public decimal PricePerMonth { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public Guid AgentId { get; set; }
        public Agent Agent { get; set; } = null!;
        public Guid? RenterId { get; set; }

        //Mnogo polezno kato piweme servisi: ot kawa da si izvadia danite direktno za usera i obratnoto
        public virtual ApplicationUser? Renter { get; set; }
    }
}

    //• Id – a unique integer, Primary Key
    //• Title – a string with min length 10 and max length 50 (required)
    //• Address – a string with min length 30 and max length 150 (required)
    //• Description – a string with min length 50 and max length 500 (required)
    //• ImageUrl – a string (required)
    //• PricePerMonth – a decimal with min value 0 and max value 2000 (required)
    //• CategoryId – an integer (required)
    //• Category – a Category object
    //• AgentId – an integer (required)
    //• Agent – an Agent object
    //• RenterId – a string
