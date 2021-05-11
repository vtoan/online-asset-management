using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public  Task<CategoryModel> CreateCategoryrAsync(CategoryModel category)
        {
            return _categoryRepository.CreateCategoryrAsync(category);
        }

        public Task<ICollection<CategoryModel>> GetListCategoryAsync()
        {
            return  _categoryRepository.GetListCategoryAsync();
        }
    }
}
