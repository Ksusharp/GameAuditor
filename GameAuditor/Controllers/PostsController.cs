using GameAuditor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GameAuditor.Models;
using GameAuditor.Models.ViewModels;
using AutoMapper;
using GameAuditor.Services.UserService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace GameAuditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        public IEntityRepository<Post> entityRepository;
        private readonly IMapper _mapper;


        public PostsController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IEntityRepository<Post> repository)
        {
            entityRepository = repository;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public PostsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Post> GetAll()
        {
            return entityRepository.GetAll();
        }

        [HttpGet("{id}")]
        public Post Get(Guid id)
        {
            return entityRepository.Get(id);
        }

        [HttpPost]
        public IActionResult Create(CreatePostViewModel entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                entityRepository.Create(_mapper.Map<Post>(entity));
                entityRepository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update(UpdatePostViewModel entity)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                entityRepository.Update(_mapper.Map<Post>(entity));
                entityRepository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                entityRepository.Delete(id);
                entityRepository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{tag}")]
        public ActionResult<Post> GetTag(Post tag)
        {
            return entityRepository.GetTag(tag);
        }
        /*
        [HttpPut("{ownpost}")]
        public IActionResult UpdateOwnPost(Post Id, UpdatePostViewModel entity)
        {
            if () 
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                try
                {
                    entityRepository.Update(_mapper.Map<Post>(entity));
                    entityRepository.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else BadRequest();
        }*/
    }
}
