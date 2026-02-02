using BaseTemplate.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseTemplate.Web.Controllers.Api {

    [Route("api/account")]
    public class AccountApiController : ControllerBase {
        private readonly SignInManager<UserModel> _signInManager;

        public AccountApiController(SignInManager<UserModel> signInManager) {
            _signInManager = signInManager;
        }
        
        [HttpPost]
        [AllowAnonymous] [Route("login")]
        public async Task<IActionResult> Login([FromBody] Dictionary<string, string> data ) {
            string user = data["user"];
            string pass = data["password"];
            var result  = await _signInManager.PasswordSignInAsync(user, pass, false, lockoutOnFailure:false);

            if (result.Succeeded) {
                return Ok();
            }

            return Unauthorized();
        }

        [HttpGet]
        public async Task<IActionResult> Logout() { 
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
