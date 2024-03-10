﻿using HouseRentingSystem.Data;
using HouseRentingSystem.Services.Data.Interfaces;
using HouseRentingSystem.Web.ViewModels.Category;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Services.Data
{
    public class CategoryService : ICategoryService
    {
        //pri po tezki proekti da rabotime sas servisi prez REPOSITORIES
        private readonly HouseRentingDbContext dbContext;
        public CategoryService(HouseRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<HouseSelectCategoryFormModel>> AllCategoriesAsync()
        {
            IEnumerable<HouseSelectCategoryFormModel> allCategories = await this.dbContext
                .Categories
                .AsNoTracking()
                .Select(c => new HouseSelectCategoryFormModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();
                
            return allCategories;
        }
    }
}