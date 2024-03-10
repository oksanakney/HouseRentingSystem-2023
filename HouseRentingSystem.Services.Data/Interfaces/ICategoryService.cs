using HouseRentingSystem.Web.ViewModels.Category;

namespace HouseRentingSystem.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        //Kogato we imame admin area triabva da mozem da popalvame categoriite
        Task<IEnumerable<HouseSelectCategoryFormModel>> AllCategoriesAsync();
    }
}
