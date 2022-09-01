using GameAuditor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GameAuditor.Models;

namespace GameAuditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        public IEntityRepository<Game> entityRepository;

        [HttpGet]
        public IEnumerable<Game> GetAll()
        {
            return entityRepository.GetAll();
        }
        [HttpGet("{id}")]
        public Game Get(Guid id)
        {
            return entityRepository.Get(id);
        }
        [HttpPost]
        public IActionResult Create(Game entity)
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
        public IActionResult Update(Game entity)
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
        [HttpGet("{platform}")]
        public ActionResult<Game> GetPlatform(Game platform)
        {
            return entityRepository.GetPlatform(platform);
        }
        [HttpGet("{genre}")]
        public ActionResult<Game> GetGenre(Game genre)
        {
            return entityRepository.GetGenre(genre);
        }
    }
}
