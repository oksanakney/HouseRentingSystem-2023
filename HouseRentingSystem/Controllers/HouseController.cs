using HouseRentingSystem.Services.Data.Interfaces;
using HouseRentingSystem.Web.Infrastructure.Extensions;
using HouseRentingSystem.Web.ViewModels.House;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Web.Controllers
{
    using static Common.NotificationMessagesConstants;

    [Authorize] // Ne iskame niakoj koito ne se e lognal da dostypva kawtite
    public class HouseController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IAgentService agentService; 
        private readonly IHouseService houseService;
        public HouseController(ICategoryService categoryService, IAgentService agentService,
            IHouseService houseService)
        {
            this.categoryService = categoryService;
            this.agentService = agentService;
            this.houseService = houseService;
        }

        [AllowAnonymous]//Iskliu4enia za nelognati potrebiteli
        public async Task<IActionResult> All()
        {
            // TODO: Implement it
            return this.Ok();
        }

        [HttpGet]
        //zarezhdaneto we e async zawoto imme catrorii
        public async Task<IActionResult> Add()
        {
            //triabva da proveria dali potrebitelia e agent i ako ne da go naso4i da stane
            bool isAgent =
                await this.agentService.AgentExistsByUserIdAsync(this.User.GetId()!);
            if (!isAgent) 
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to add new houses!";

                return this.RedirectToAction("Become", "Agent");
            }

            HouseFormModel formModel = new HouseFormModel()
            // sas object initializer we go popalvame
            {
                //zarezdam categories pres categoryService
                Categories = await this.categoryService.AllCategoriesAsync()
            };

            return View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseFormModel model)
        {
            bool isAgent =
                await this.agentService.AgentExistsByUserIdAsync(this.User.GetId()!);
            
            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to add new houses!";

                return this.RedirectToAction("Become", "Agent");
            }

            //ot inspect niakoj mozhe da promeni category id
            bool categoryExists = 
                await this.categoryService.ExistsByIdAsync(model.CategoryId);
            if (!categoryExists) 
            {
                // Adding model error to ModelState automatically makes ModelState Invalid
                this.ModelState.AddModelError(nameof(model.CategoryId), "Selected category does not exist!");
            }

            if (!this.ModelState.IsValid)
            {
                model.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(model);
            }

            try
            {
                //deklariram promenlivata na naj-blizkoto mjasto do koeto ja izpolzvam
                string? agentId = 
                    await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

                await this.houseService.CreateAsync(model, agentId!);
            }
            catch (Exception _)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to add your new house! Please try again later or contact administrator.");
                model.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(model);
            }

            return this.RedirectToAction("All", "House");
        }
    }
}
