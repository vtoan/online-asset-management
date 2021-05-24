using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Services;
using RookieOnlineAssetManagement.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<IEnumerable<UserModel>>> GetListAsync([FromQuery] UserRequestParmas userRequestParmas)
        {
            userRequestParmas.LocationId = RequestHelper.GetLocationSession(HttpContext);
            var result = await _userSer.GetListUserAsync(userRequestParmas);
            HttpContext.Response.Headers.Add("total-pages", result.TotalPage.ToString());
            return Ok(result.Datas);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailModel>> GetAsync(string id)
        {
            return Ok(await _userSer.GetUserByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> UpdateAsync(string id, UserRequestModel userRequest)
        {
            if (!ModelState.IsValid) return BadRequest(userRequest);
            return Ok(await _userSer.UpdateUserAsync(id, userRequest));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserRequestModel>> DisableAsync(string id)
        {
            return Ok(await _userSer.DisableUserAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> CreateAsync(UserRequestModel userRequestModel)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _userSer.CreateUserAsync(userRequestModel);
            if (result == null) return BadRequest();
            return Ok(result);
        }
    }
}
