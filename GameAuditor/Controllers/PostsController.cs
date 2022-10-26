using GameAuditor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GameAuditor.Models;
using GameAuditor.Models.ViewModels;
using AutoMapper;
using GameAuditor.Services.UserService;
using Microsoft.AspNetCore.Authorization;

namespace GameAuditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IEntityRepository<Post> _entityRepository;
        private readonly IEntityRepository<PostTag> _postTagRepository;
        private readonly IEntityRepository<TagNavigation> _tagNavigationRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;


        public PostsController(IEntityRepository<Post> repository, IEntityRepository<PostTag> postTag,
            IEntityRepository<TagNavigation> tagNavigationRepository, IMapper mapper, IUserService userService)
        {
            _entityRepository = repository;
            _postTagRepository = postTag;
            _tagNavigationRepository = tagNavigationRepository;
            _mapper = mapper;
            _userService = userService;
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

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreatePostViewModel entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var newPost = _mapper.Map<Post>(entity);
                newPost.OwnerId = _userService.GetMyId();
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

        [Authorize]
        [HttpPut]
        public IActionResult Update(UpdatePostViewModel entity)
        {
            var newPost = _mapper.Map<Post>(entity);
            if (newPost.OwnerId == _userService.GetMyId())
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                try
                {
                    _entityRepository.Update(newPost);
                    _entityRepository.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                return BadRequest("You are not the owner of the post");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var post = _entityRepository.Get(id);
            if (post.OwnerId == _userService.GetMyId())
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
                return BadRequest("You are not the owner of the post");
        }

        [HttpGet("{tagsfrompost}")]
        public IActionResult GetTag(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var postTags = _tagNavigationRepository.GetAll().Where(x => x.PostId == id);
            try
            {
                if (postTags.Any())
                {
                    var tags = from tag in postTags
                               select new { TagId = tag.TagId, Tag = tag.Tag };
                    tags.ToList();
                    return Ok(tags);
                }
                else
                    return BadRequest("Post has no tags");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
