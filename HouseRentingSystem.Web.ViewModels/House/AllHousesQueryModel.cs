using HouseRentingSystem.Web.ViewModels.House.Enums;
using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem.Web.ViewModels.House
{
    using static Common.GeneralApplicationConstants;
    public class AllHousesQueryModel
    {
        public AllHousesQueryModel()
        {
            //Iznasiam si defaltnite stojnosti, moga da si gi iznesa v GeneralAppConsts
            this.CurrentPage = DefaultPage;
            this.HousesPerPage = EntitiesPerPage;

            //to be sure it is not nullable
            this.Categories = new HashSet<string>();
            this.Houses = new HashSet<HouseAllViewModel>();
        }
        // Is nullable because the user can put it or not
        public string? Category { get; set; }

        [Display(Name ="Search by word")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort Houses By")]
        public HouseSorting HouseSorting { get; set; }

        //Current page, stranica na kojato se namirame
        public int CurrentPage { get; set; }

        //Ot Kris -> po princip v pove4eto sajtove sami mozheme da reguliarame kolko to4no
        // sa entitita koito visualizirame na edna stranica
        [Display(Name ="Show Houses On Page")]
        public int HousesPerPage { get; set; }
        public int TotalHouses { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<HouseAllViewModel> Houses { get; set; }
    }
}
