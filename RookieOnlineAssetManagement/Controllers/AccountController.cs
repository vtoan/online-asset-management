using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RookieOnlineAssetManagement.Entities;
using System.Threading.Tasks;
using RookieOnlineAssetManagement.Models;
using Microsoft.AspNetCore.Authorization;
using RookieOnlineAssetManagement.Utils;

namespace RookieOnlineAssetManagement.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManger;
        private readonly UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManger, UserManager<User> userManager)
        {
            _signInManger = signInManger;
            _userManager = userManager;
        }

        public class LoginModel
        {
            [Required]
            public string UserName { set; get; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        [HttpPost("/check-login")]
        public async Task<ActionResult<UserModel>> CheckLoginAsync()
        {
            if (_signInManger.IsSignedIn(User))
            {
                var userCurr = await _userManager.GetUserAsync(User);
                if (userCurr == null) return NotFound();
                RequestHelper.SetLocationSession(HttpContext, userCurr.LocationId);
                var roles = await _userManager.GetRolesAsync(userCurr);
                return Ok(new UserModel
                {
                    UserId = userCurr.Id,
                    UserName = userCurr.UserName,
                    FullName = $"{userCurr.FirstName} {userCurr.LastName}",
                    RoleName = roles.Count > 0 ? roles[0] : "unknown",
                    LocationId = userCurr.LocationId
                });
            }
            return NotFound();
        }


        [HttpPost("/login")]
        public async Task<ActionResult<UserModel>> LoginAsync(LoginModel loginModel)
        {
            if (!ModelState.IsValid) return BadRequest();
            var re = await _signInManger.PasswordSignInAsync(loginModel.UserName, loginModel.Password, false, true);
            if (re.IsLockedOut == true) return Forbid();
            if (re.Succeeded)
            {
                var userCurr = await _userManager.FindByNameAsync(loginModel.UserName);
                if (userCurr == null) return NotFound();
                RequestHelper.SetLocationSession(HttpContext, userCurr.LocationId);
                var roles = await _userManager.GetRolesAsync(userCurr);
                return Ok(new UserModel
                {
                    UserId = userCurr.Id,
                    UserName = userCurr.UserName,
                    FullName = $"{userCurr.FirstName} {userCurr.LastName}",
                    RoleName = roles.Count > 0 ? roles[0] : "unknown",
                    LocationId = userCurr.LocationId
                });
            }
            return NotFound();
        }

        [HttpPost("/logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            if (!_signInManger.IsSignedIn(User)) return Forbid();
            await _signInManger.SignOutAsync();
            return NoContent();
        }


        public class ChangePassWordModel
        {
            [Required]
            public string OldPassword { get; set; }

            [Required]
            public string NewPassword { set; get; }
        }

        [Authorize]
        [HttpPost("/change-password")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePassWordModel userModel)
        {
            if (!ModelState.IsValid) return BadRequest();
            var user = await _userManager.GetUserAsync(User);
            var isPassValid = await _signInManger.CheckPasswordSignInAsync(user, userModel.OldPassword, false);
            if (!isPassValid.Succeeded) return BadRequest("Password incorrect");
            var changePassword = await _userManager.ChangePasswordAsync(user, userModel.OldPassword, userModel.NewPassword);
            if (changePassword.Succeeded) return Ok();
            return Problem("Can't change password");
        }

        [Authorize]
        [HttpPost("/change-password-first/{id}")]
        public async Task<IActionResult> ChangePasswordFirstAsync([FromBody] string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword)) return BadRequest();
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var defaultPass = AccountHelper.GenerateAccountPass(user.UserName, user.DateOfBirth.Value);
            var changePassword = await _userManager.ChangePasswordAsync(user, defaultPass, newPassword);
            if (changePassword.Succeeded) return Ok();
            return Problem("Can't change password");
        }

    }
}