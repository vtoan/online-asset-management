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
            var CateNameRepo = await _dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryName == category.CategoryName);
            if (CateNameRepo != null)
            {
                throw new Exception("Repository | Category Name is used");
            }
            var CatePrefixRepo = await _dbContext.Categories.FirstOrDefaultAsync(x => x.ShortName == category.ShortName);
            if(CatePrefixRepo!=null)
            {
                throw new Exception("Repository | Prefix is used");
            }
            category.CategoryId = Guid.NewGuid().ToString();
            category.ShortName = category.ShortName.ToUpper();
            var cate = new Category
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                ShortName = category.ShortName,
                NumIncrease = 0,
            };
            var create = _dbContext.Add(cate);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return category;
            }
            throw new Exception("Repository | Create category fail");
        }

        public async Task<ICollection<CategoryModel>> GetListCategoryAsync()
        { 
            return await _dbContext.Categories
                   .Select(cate => new CategoryModel { CategoryName = cate.CategoryName ,ShortName = cate.ShortName , CategoryId = cate.CategoryId})
                   .ToListAsync();
        }
    }
}
