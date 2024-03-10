using HouseRentingSystem.Web.ViewModels.Category;
using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem.Web.ViewModels.House
{
    using static Common.EntityValidationConstants.House;
    public class HouseFormModel
    {
        //pri redirect categories mozhe da izgarmiat
        public HouseFormModel()
        {
            this.Categories = new HashSet<HouseSelectCategoryFormModel>();
        }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        // mozem da slozim custom error message, oba4e vgradenia e dostato4no dobar,
        //mozem da go promenim samo ako go iskame na bg
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(ImageUrlMaxLength)]
        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Range(typeof(decimal), PricePerMonthMinValue, PricePerMonthMaxValue)]
        [Display(Name = "Monthly Price")]
        public decimal PricePerMonth { get; set; }

        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        public IEnumerable<HouseSelectCategoryFormModel> Categories { get; set; }
    }
}
