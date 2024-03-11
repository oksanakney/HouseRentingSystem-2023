using HouseRentingSystem.Services.Data.Interfaces;
using HouseRentingSystem.Services.Data.Models.House;
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

        [HttpGet]
        [AllowAnonymous]//Iskliu4enia za nelognati potrebiteli
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel queryModel)
        {
            AllHousesFilteredAndPagedServiceModel serviceModel = 
                await this.houseService.AllAsync(queryModel);

            queryModel.Houses = serviceModel.Houses;
            queryModel.TotalHouses = serviceModel.TotalHousesCount;
            queryModel.Categories = await this.categoryService.AllCategoryNamesAsync();

            return this.View(queryModel);
        }

        [HttpGet]
        //zarezhdaneto we e async zawoto imame categorii
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

            try
            {
                HouseFormModel formModel = new HouseFormModel()
                // sas object initializer we go popalvame
                {
                    //zarezdam categories pres categoryService
                    Categories = await this.categoryService.AllCategoriesAsync()
                };

                return View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }            
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

                string houseId = await this.houseService.CreateAndReturnIdAsync(model, agentId!);

                this.TempData[SuccessMessage] = "House was added successfully!";
                return this.RedirectToAction("Details", "House", new { id = houseId });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, 
                    "Unexpected error occured while trying to add your new house! Please try again later or contact administrator.");
                model.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(model);
            }            
        }

        [HttpGet]
        [AllowAnonymous]
        // In _HousePartial asp-route-id -> string id
        public async Task<IActionResult> Details(string id)
        {
            bool houseExist = await this.houseService
                .ExistsByIdAsync(id);

            if (!houseExist) 
            {
                this.TempData[ErrorMessage] = "House with the provided id does not exist!";

                return this.RedirectToAction("All", "House");
            }

            try
            {
                HouseDetailsViewModel viewModel = await this.houseService
                .GetDetailsByIdAsync(id);

                return View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }            
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool houseExist = await this.houseService
               .ExistsByIdAsync(id);

            if (!houseExist)
            {
                this.TempData[ErrorMessage] = "House with the provided id does not exist!";

                return this.RedirectToAction("All", "House");
            }            

            //Triabva da proverime i drugi newa: is user agent
            bool isUseAgent = await this.agentService
                .AgentExistsByUserIdAsync(this.User.GetId()!);

            if (!isUseAgent) 
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit house info!";

                return this.RedirectToAction("Become", "Agent");
            }

            //sajt s objavi ne moga da pipam objavi na drugite
            string? agentId = await this.agentService
                .GetAgentIdByUserIdAsync(this.User.GetId()!);
            bool isAgentOwner = await this.houseService
                .IsAgentWithIdOwnerOfTheHouseWithIdAsync(id, agentId!);

            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must become the agent owner of the house you want to edit!";

                return this.RedirectToAction("Mine", "House");
            }

            try
            {
                HouseFormModel formModel = await this.houseService
                .GetHouseForEditByIdAsync(id);
                formModel.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }         

        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, HouseFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = await this.categoryService.AllCategoriesAsync();
                return this.View(model);
            }

            bool houseExist = await this.houseService
              .ExistsByIdAsync(id);

            if (!houseExist)
            {
                this.TempData[ErrorMessage] = "House with the provided id does not exist!";

                return this.RedirectToAction("All", "House");
            }

            //Triabva da proverime i drugi newa: is user agent
            bool isUseAgent = await this.agentService
                .AgentExistsByUserIdAsync(this.User.GetId()!);

            if (!isUseAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit house info!";

                return this.RedirectToAction("Become", "Agent");
            }

            //sajt s objavi ne moga da pipam objavi na drugite
            string? agentId = await this.agentService
                .GetAgentIdByUserIdAsync(this.User.GetId()!);
            bool isAgentOwner = await this.houseService
                .IsAgentWithIdOwnerOfTheHouseWithIdAsync(id, agentId!);

            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must become the agent owner of the house you want to edit!";

                return this.RedirectToAction("Mine", "House");
            }

            HouseFormModel formModel = await this.houseService
                .GetHouseForEditByIdAsync(id);
            formModel.Categories = await this.categoryService.AllCategoriesAsync();

            try
            {
                await this.houseService.EditHouseByIdAndFormModelAsync(id, model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, 
                    "Unexpected error occured while trying to Update the house. Plese try again later or contact administrator.");
                model.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(model);
            }

            this.TempData[SuccessMessage] = "House was edited successfully!";
            return this.RedirectToAction("Details", "House", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool houseExist = await this.houseService
               .ExistsByIdAsync(id);

            if (!houseExist)
            {
                this.TempData[ErrorMessage] = "House with the provided id does not exist!";

                return this.RedirectToAction("All", "House");
            }

            //Triabva da proverime i drugi newa: is user agent
            bool isUseAgent = await this.agentService
                .AgentExistsByUserIdAsync(this.User.GetId()!);

            if (!isUseAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit house info!";

                return this.RedirectToAction("Become", "Agent");
            }

            //sajt s objavi ne moga da pipam objavi na drugite
            string? agentId = await this.agentService
                .GetAgentIdByUserIdAsync(this.User.GetId()!);
            bool isAgentOwner = await this.houseService
                .IsAgentWithIdOwnerOfTheHouseWithIdAsync(id, agentId!);

            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must become the agent owner of the house you want to edit!";

                return this.RedirectToAction("Mine", "House");
            }

            try
            {
                HousePreDeleteDetailsViewModel viewModel = 
                    await this.houseService.GetHouseForDeleteByIdAsync(id);

                return this.View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, HousePreDeleteDetailsViewModel model)
        {
            bool houseExist = await this.houseService
               .ExistsByIdAsync(id);

            if (!houseExist)
            {
                this.TempData[ErrorMessage] = "House with the provided id does not exist!";

                return this.RedirectToAction("All", "House");
            }

            //Triabva da proverime i drugi newa: is user agent
            bool isUseAgent = await this.agentService
                .AgentExistsByUserIdAsync(this.User.GetId()!);

            if (!isUseAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit house info!";

                return this.RedirectToAction("Become", "Agent");
            }

            //sajt s objavi ne moga da pipam objavi na drugite
            string? agentId = await this.agentService
                .GetAgentIdByUserIdAsync(this.User.GetId()!);
            bool isAgentOwner = await this.houseService
                .IsAgentWithIdOwnerOfTheHouseWithIdAsync(id, agentId!);

            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must become the agent owner of the house you want to edit!";

                return this.RedirectToAction("Mine", "House");                 
            }

            try
            {
                await this.houseService.DeleteHouseByIdAsync(id);

                //  TODO: Warning message appears, but it does not show the string! It is empty
                this.TempData[WarningMessage] = "The house was successfully deleted!";

                return this.RedirectToAction("Mine", "House");
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<HouseAllViewModel> myHouses =
                new List<HouseAllViewModel>();

            string userId = this.User.GetId()!;
            bool isUserAgent = await this.agentService
                .AgentExistsByUserIdAsync(userId);

            try
            {
                if (isUserAgent) 
            {
                string? agentId = 
                    await this.agentService.GetAgentIdByUserIdAsync(userId);

                myHouses.AddRange(await this.houseService.AllByAgentIdAsync(agentId!));
            }

            else
            {
                myHouses.AddRange(await this.houseService.AllByUserIdAsync(userId));
            }

            return this.View(myHouses);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }            
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                    "Unexpected error occurred! Please try again later or contact administrator.";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
