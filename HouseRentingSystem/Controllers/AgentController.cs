using HouseRentingSystem.Services.Data.Interfaces;
using HouseRentingSystem.Web.Infrastructure.Extensions;
using HouseRentingSystem.Web.ViewModels.Agent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace HouseRentingSystem.Web.Controllers
{
    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class AgentController : Controller
    {
        private readonly IAgentService agentService;
        public AgentController(IAgentService agentService)
        {
            this.agentService = agentService;
        }
        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();
            bool isAgent = await this.agentService.AgentExistsByUserIdAsync(userId);
            if (isAgent)
            {
                this.TempData[ErrorMessage] = "You are already an agent!";

                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Become (BecomeAgentFormModel model)
        {
            string? userId = this.User.GetId();
            bool isAgent = await this.agentService.AgentExistsByUserIdAsync(userId);
            if (isAgent)
            {
                this.TempData[ErrorMessage] = "You are already an agent!";

                return this.RedirectToAction("Index", "Home");
            }

            bool isPhoneNumberTaken = await this.agentService.AgentExistsByPhoneNumberAsync(model.PhoneNumber);
            if (isPhoneNumberTaken) 
            {
                this.ModelState.AddModelError(nameof(model.PhoneNumber), "Agent with the provided number already exists!");
            }

            if (!ModelState.IsValid) 
            { 
                return this.View(model);
            }

            bool userHasActiveRents = await this.agentService
                .HasRentByUserIdAsync(userId);

            if (userHasActiveRents)
            {
                this.TempData[ErrorMessage] = "You must not have any active rents in order to become an agent!";

                return this.RedirectToAction("Mine", "House");
            }

            try
            {
                await this.agentService.Create(userId, model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured while registering you as an agent! Please try again late or contact administrator.";

                return this.RedirectToAction("Index", "Home");
            }

            return this.RedirectToAction("All", "House");
        }

        
    }
}
