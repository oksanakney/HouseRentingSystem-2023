using HouseRentingSystem.Web.ViewModels.Agent;

namespace HouseRentingSystem.Web.ViewModels.House
{
    // Which expands HouseAllViewModel
    public class HouseDetailsViewModel : HouseAllViewModel
    {
        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public AgentInfoOnHouseViewModel Agent { get; set; } = null!;


    }
}
