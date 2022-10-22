using GameAuditor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GameAuditor.Models;
using GameAuditor.Models.ViewModels;
using AutoMapper;
using GameAuditor.Repositories.Implimentations;
using GameAuditor.Services.UserService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Linq;

namespace GameAuditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        private Post _resource;
        private readonly IEntityRepository<Post> _entityRepository;
        private readonly IEntityRepository<PostTag> _postTagRepository;
        private readonly IEntityRepository<TagNavigation> _tagNavigationRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;


        public PostsController(IEntityRepository<Post> repository, IEntityRepository<PostTag> postTag,
            IEntityRepository<TagNavigation> tagNavigationRepository, IMapper mapper)
        {
            _entityRepository = repository;
            //_roleManager = roleManager;
            //_userManager = userManager;
            //_userService = userService;
            _postTagRepository = postTag;
            //_resource = resource;
            _tagNavigationRepository = tagNavigationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Post> GetAll()
        {
            return _entityRepository.GetAll();
        }

        [HttpGet("{id}")]
        public Post Get(Guid id)
        {
            return _entityRepository.Get(id);
        }

        [HttpPost]
        public IActionResult Create(CreatePostViewModel entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var newPost = _mapper.Map<Post>(entity);
                newPost.OwnerId = "04f10f0e-5eaf-444d-b6f1-91ef3e88c7aa";
                _entityRepository.Create(newPost);
                if (entity.Tags.Any())
                {
                    var existTags = entity.Tags.Select(x => x.Tag).ToList();
                    var tags = _postTagRepository.GetAll().Where(x => existTags.Contains(x.Tag)).ToList();
                    if (tags.Any())
                    {
                        var newTagsNav = tags.Select(x => new TagNavigation() { TagId = x.Id, PostId = newPost.Id});
                        _tagNavigationRepository.CreateRange(newTagsNav);
                    }
                }
                _entityRepository.Save();
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
            if (_resource.OwnerId == _userService.GetMyId())
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                try
                {
                    _entityRepository.Update(_mapper.Map<Post>(entity));
                    _entityRepository.Save();
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
            if (_resource.OwnerId == _userService.GetMyId())
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                try
                {
                    _entityRepository.Delete(id);
                    _entityRepository.Save();
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

        [HttpGet("{tag}")]
        public ActionResult<Post> GetTag(Post tag)
        {
            return _entityRepository.GetTag(tag);
        }
    }
}
