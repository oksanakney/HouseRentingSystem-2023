using HouseRentingSystem.Services.Data.Interfaces;
using HouseRentingSystem.Services.Data.Models.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.WebAPI.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IHouseService houseService;

        public StatisticsApiController(IHouseService houseService)
        {
            this.houseService = houseService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(StatisticsServiceModel))]
        [ProducesResponseType(400)]        
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                StatisticsServiceModel serviceModel = 
                    await this.houseService.GetStatisticsAsync();
                return this.Ok(serviceModel);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }

    }
}
