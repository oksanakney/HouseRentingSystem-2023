using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace HouseRentingSystem.Web.ViewModels.Agent
{
    public class AgentInfoOnHouseViewModel
    {
        public string Email { get; set; } = null!;

        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;
    }
}
