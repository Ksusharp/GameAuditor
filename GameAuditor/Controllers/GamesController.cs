using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GameAuditor.Models;
using GameAuditor.Repositories.Interfaces;
using GameAuditor.Database;
using GameAuditor.Repositories.Implimentations;
using GameAuditor.Models.Interfaces;

namespace GameAuditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        //private IEntityRepository<Post> entityRepository { get; set; }
        public IEntityRepository<Post> entityRepository;

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
        public IActionResult Create(Post entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                entityRepository.Create(entity);
                entityRepository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Update(Post entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                entityRepository.Update(entity);
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
        public Post GetTag(Post tag)
        {
            return entityRepository.GetTag(tag);
        }
        [HttpGet("{platform}")]
        public Post GetPlatform(Post platform)
        {
            return entityRepository.GetPlatform(platform);
        }
        [HttpGet("{genre}")]
        public Post GetGenre(Post genre)
        {
            return entityRepository.GetGenre(genre);
        }
    }
}
