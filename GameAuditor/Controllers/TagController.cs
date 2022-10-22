using GameAuditor.Models.ViewModels;
using GameAuditor.Models;
using GameAuditor.Repositories.Implimentations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using GameAuditor.Repositories.Interfaces;
using GameAuditor.Services.UserService;
using Microsoft.AspNetCore.Identity;

namespace GameAuditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        public IEntityRepository<PostTag> _entityRepository;

        private readonly IMapper _mapper;
        private readonly IUserService _userService;


        public TagController(IUserService userService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IEntityRepository<PostTag> repository)
        {
            _entityRepository = repository;
            _roleManager = roleManager;
            _userManager = userManager;
            _userService = userService;
        }


        [HttpPost]
        public IActionResult Create(PostTag entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _entityRepository.Create(new PostTag() {Tag = entity.Tag});
                _entityRepository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
