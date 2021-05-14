using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userSer;

        public UsersController(IUserService userSer)
        {
            _userSer = userSer;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetListAsync(string locationId, [FromQuery] TypeUser[] type, string query, SortBy? sortCode, SortBy? sortFullName, SortBy? sortDate, SortBy? sortType, int page, int pageSize)
        {
            var result = await _userSer.GetListUserAsync(locationId, type, query, sortCode, sortFullName, sortDate, sortType, page, pageSize);
            HttpContext.Response.Headers.Add("total-pages", result.TotalPage.ToString());
            return Ok(result.Datas);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailModel>> GetAsync(string id)
        {
            return Ok(await _userSer.GetUserByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserRequestModel>> UpdateAsync(string id, UserRequestModel userRequest)
        {
            return Ok(await _userSer.UpdateUserAsync(id, userRequest));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserRequestModel>> DisableAsync(string id)
        {
            return Ok(await _userSer.DisableUserAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<UserRequestModel>> CreateAsync(UserRequestModel userRequestModel)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _userSer.CreateUserAsync(userRequestModel);
            if (result == null) return BadRequest();
            return Ok(result);
        }

    }
}
