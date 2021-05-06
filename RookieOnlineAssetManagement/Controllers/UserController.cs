using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userSer;

        public UserController(IUserService userSer)
        {
            _userSer = userSer;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> Get()
        {
            return Ok(_userSer.GetLists());
        }
    }
}
