using GameAuditor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GameAuditor.Models;
using GameAuditor.Models.ViewModels;
using AutoMapper;
using NuGet.Protocol.Core.Types;
using GameAuditor.Services.UserService;
using Microsoft.AspNetCore.Identity;

namespace GameAuditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        public IEntityRepository<Post> entityRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public PostsController(IUserService userService, UserManager<User> userManager, IEntityRepository<Post> repository)
        {
            entityRepository = repository;
            _userService = userService;
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
            if (Post.OwnerID = _userService.GetMyId())
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
            else
                return BadRequest();
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
        [HttpPut("updatetag")]
        public IActionResult Update(Guid tagId, Guid postId)
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
        
        }*/
    }
}
