using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Web.Controllers
{
    [Authorize] // Ne iskame niakoj koito ne se e lognal da dostypva kawtite
    public class HouseController : Controller
    {
        [AllowAnonymous]//Iskliu4enia za nelognati potrebiteli
        public async Task<IActionResult> All()
        {
            return View();
        }

        [HttpGet]
        //zarezhdaneto we e async zawoto imme catrorii
        public async Task<IActionResult> Add()
        {

        }
    }
}
