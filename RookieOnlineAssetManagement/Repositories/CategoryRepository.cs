using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CategoryModel> CreateCategoryrAsync(CategoryModel category)
        {
            category.CategoryId = Guid.NewGuid().ToString();
            category.ShortName = category.ShortName.ToUpper();
            var cate = new Category
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                ShortName = category.ShortName,
                NumIncrease = 0,
            };
            _dbContext.Add(cate);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<ICollection<CategoryModel>> GetListCategoryAsync()
        { 
            return await _dbContext.Categories
                   .Select(cate => new CategoryModel { CategoryName = cate.CategoryName ,ShortName = cate.ShortName , CategoryId = cate.CategoryId})
                   .ToListAsync();
        }
    }
}
