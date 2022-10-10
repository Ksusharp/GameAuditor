using GameAuditor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GameAuditor.Models;
using GameAuditor.Models.ViewModels;
using AutoMapper;

namespace GameAuditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        public IEntityRepository<Game> entityRepository;
        private readonly IMapper _mapper;

        public GamesController(IEntityRepository<Game> repository)
        {
            entityRepository = repository;
        }

        public GamesController(IMapper mapper)
        {
            _mapper = mapper;
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
        public IActionResult Create(CreateGameViewModel entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                entityRepository.Create(_mapper.Map<Game>(entity));
                entityRepository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        public IActionResult Update(UpdateGameViewModel entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                entityRepository.Update(_mapper.Map<Game>(entity));
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
