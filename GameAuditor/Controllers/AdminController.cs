using GameAuditor.Models;
using GameAuditor.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameAuditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles = "Admin")]
    public class AdminController : Controller
    {
        UserManager<User> _userManager;

        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpDelete("deleteuser")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                return Ok();
            }
            else return BadRequest("User not found");
        }
    }
}
