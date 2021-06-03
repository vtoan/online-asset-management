using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("ADMIN")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetListAsync()
        {
            return Ok(await _categoryService.GetListCategoryAsync());
        }

        [HttpPost]
        public async Task<ActionResult<CategoryModel>> CreateAsync(CategoryModel Model)
        {
            if (!ModelState.IsValid) return BadRequest(Model);
            return Ok(await _categoryService.CreateCategoryrAsync(Model));
        }
    }
}
