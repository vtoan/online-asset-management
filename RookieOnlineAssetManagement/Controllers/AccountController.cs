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

        [HttpPost("/login")]
        public async Task<ActionResult<UserModel>> LoginAsync(LoginModel loginModel)
        {
            if (_signInManger.IsSignedIn(User)) return Ok();
            if (!ModelState.IsValid) return BadRequest();
            var re = await _signInManger.PasswordSignInAsync(loginModel.UserName, loginModel.Password, false, true);
            if (re.IsLockedOut == true) return Forbid();
            if (re.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginModel.UserName);
                var roles = await _userManager.GetRolesAsync(user);
                if (user == null) return NotFound();
                return Ok(new UserModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FullName = $"{user.FirstName} {user.LastName}",
                    RoleName = roles.Count > 0 ? roles[0] : "unknown",
                    LocationId = user.LocationId
                });
            }
            return NotFound();
        }

        [Authorize]
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
            var changePassword = await _userManager.ChangePasswordAsync(user, userModel.OldPassword, userModel.NewPassword);
            if (changePassword.Succeeded) return Ok();
            return NotFound();
        }

        [Authorize]
        [HttpPost("/change-password-first/{id}")]
        public async Task<IActionResult> ChangePasswordFirstAsync(string id, [FromBody] string newPassword)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(newPassword)) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var defaultPass = AccountHelper.GenerateAccountPass(user.UserName, user.DateOfBirth.Value);
            var changePassword = await _userManager.ChangePasswordAsync(user, defaultPass, newPassword);
            if (changePassword.Succeeded) return Ok();
            return NotFound();
        }

    }
}