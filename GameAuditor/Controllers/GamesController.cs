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

        public void Post(IBaseEntity entity);

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
        public void Create(Post entity)
        {
            entityRepository.Create(entity);
            entityRepository.Save();
        }
        [HttpPut]
        public void Update(Post entity)
        {
            entityRepository.Update(entity);
            entityRepository.Save();
        }
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            entityRepository.Delete(id);
            entityRepository.Save();
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
