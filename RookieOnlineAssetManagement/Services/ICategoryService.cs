using RookieOnlineAssetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryModel>> GetListCategoryAsync();

        Task<CategoryModel> CreateCategoryrAsync(CategoryModel category);
    }
}
