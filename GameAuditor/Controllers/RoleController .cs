﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using GameAuditor.Database;
using GameAuditor.Models;
using GameAuditor.Models.ViewModels;
using GameAuditor.Services.UserService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace GameAuditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        private readonly IUserService _userService;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IUserService userService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userService = userService;
        }

        [Authorize]
        [HttpGet("getroles")]
        public async Task<IEnumerable<string>> Index()
        {
            var userName = _userService.GetMyName();
            var user = await _userManager.FindByNameAsync(userName);
            // получем список ролей пользователя
            return await _userManager.GetRolesAsync(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            return BadRequest("null name");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                return Ok(result);
            }
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public IActionResult UserList() => Ok(_userManager.Users.ToList());

        [Authorize(Roles = "Admin")]
        [HttpPost("update")]
        public async Task<IActionResult> Edit(Guid userId)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id.ToString(),
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return Ok(model);
            }
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addroletouser")]
        public async Task<IActionResult> AddRoleToUser(Guid userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                if (await _userManager.IsInRoleAsync(user, roleName))
                {   
                    await _userManager.AddToRoleAsync(user, roleName);
                    return Ok();
                }
                else return BadRequest("No Role");
            } else 
                return BadRequest("No user");
        }
    }
}