using GameAuditor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GameAuditor.Models.ViewModels;
using GameAuditor.Models;
using AutoMapper;

namespace GameAuditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        public IEntityRepository<Post> entityRepository;
        private readonly IMapper _mapper;

        public PostsController(IEntityRepository<Post> repository)
        {
            entityRepository = repository;
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
        public IActionResult Update(CreatePostViewModel entity)
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
    }
}
