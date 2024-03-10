using HouseRentingSystem.Web.ViewModels.House;

namespace HouseRentingSystem.Services.Data.Models.House
{
    //Service Models have to see View Models
    public class AllHousesFilteredAndPagedServiceModel
    {
        public AllHousesFilteredAndPagedServiceModel()
        {
            this.Houses = new HashSet<HouseAllViewModel>();
        }
        public int TotalHousesCount { get; set; }
        public IEnumerable<HouseAllViewModel> Houses { get; set; }
    }
}
