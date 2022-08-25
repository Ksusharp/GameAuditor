using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GameAuditor.Models;
using GameAuditor.Repositories.Interfaces;
using GameAuditor.Database;
using GameAuditor.Repositories.Implimentations;

namespace GameAuditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
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
        public void Create(Post entity)
        {
            entityRepository.Create(entity);
            entityRepository.Save();
        }
        public void Update(Post entity)
        {
            entityRepository.Update(entity);
            entityRepository.Save();
        }
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
