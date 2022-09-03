using GameAuditor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GameAuditor.Models;
using GameAuditor.Models.ViewModels;

namespace GameAuditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        public IEntityRepository<Game> entityRepository;

        public GamesController(IEntityRepository<Game> repository)
        {
            entityRepository = repository;
        }

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
        public IActionResult Create(GameViewModel entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var game = new Game()
                {
                    Name = entity.Name,
                    Description = entity.Description,
                    ReleaseDate = entity.ReleaseDate,
                    Genre = entity.Genre,
                    Platform = entity.Platform
                };
                entityRepository.Create(game);
                entityRepository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update")]
        public IActionResult Update(GameViewModel entity, [FromQuery] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var game = new Game()
                {
                    Id = id,
                    Name = entity.Name,
                    Description = entity.Description,
                    ReleaseDate = entity.ReleaseDate,
                    Genre = entity.Genre,
                    Platform = entity.Platform
                };
                entityRepository.Update(game);
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
