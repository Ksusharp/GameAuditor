using GameAuditor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GameAuditor.Models;
using GameAuditor.Models.ViewModels;
using AutoMapper;
using GameAuditor.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult Create(CreatePostViewModel postEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var newPost = _mapper.Map<Post>(postEntity);
                newPost.OwnerId = _userService.GetMyId();
                _entityRepository.Create(newPost);
                _entityRepository.Save();
                if (postEntity.Tags.Any())
                {
                    var existTags = postEntity.Tags.Select(x => x.Tag).ToList();
                    var tags = _postTagRepository.GetAll().Where(x => existTags.Contains(x.Tag)).ToList();
                    if (tags.Any())
                    {
                        var newTagsNav = tags.Select(x => new TagNavigation() { TagId = x.Id, PostId = newPost.Id });
                        _tagNavigationRepository.CreateRange(newTagsNav);
                    }
                }
                _tagNavigationRepository.Save();

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
        
        [HttpGet("{getpostwithtagorsearch}")]
        public async Task<IActionResult> GetPost(string tag, string searchString)
        {
            var tags = _postTagRepository.GetAll().Select(x => x.Tag);
            var posts = _entityRepository.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(p => p.Title!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(tag))
            {
                //posts = posts.Where(p => p.Tag == tag);

                //posts = await posts
               //      .Include(t => t.TagNavigation)
                //    .Where(t => t.Tag == tag);

                //var viewModel = new Post();
                //viewModel.TagNavigation = await _entityRepository.TagNavigation;


            }

            var postTagViewModel = new PostTagViewModel
            {
                Tags = new SelectList(tags.Distinct().ToList()),
                Posts = posts.ToList()
            };

            return Ok(postTagViewModel);
        }
    }
}
