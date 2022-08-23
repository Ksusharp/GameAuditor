using GameAuditor.Database;
using GameAuditor.Models;
using GameAuditor.Repositories.Interfaces;

namespace GameAuditor.Repositories.Implimentations
{
    public class EntityRepository : IEntityRepository
    {
        public ApplicationContext _context;
        public EntityRepository(ApplicationContext context)
        {
            _context = context;
        }
        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }
        public Post GetPost(Guid id)
        {
            return _context.Posts.Find(id);
        }
        public Post CreatePost(Post model)
        {
            _context.Posts.Add(model);
            _context.SaveChanges();
            return model;
        }
        public Post UpdatePost(Post postForUpdate)
        {
            _context.Update(postForUpdate);
            _context.SaveChanges();
            return postForUpdate;
        }
        public void DeletePost(Guid id)
        {
            var postForDelete = _context.Posts.Find(id);
            _context.Posts.Remove(postForDelete);
            _context.SaveChanges();
        }
        public Post GetTag(Post tag)
        {
            return _context.Posts.Find(tag);
        }
        public List<Game> GetAllGames()
        {
            return _context.Games.ToList();
        }
        public Game GetGame(Guid id)
        {
            return _context.Games.Find(id);
        }
        public Game CreateGame(Game model)
        {
            _context.Games.Add(model);
            _context.SaveChanges();
            return model;
        }
        public Game UpdateGame(Game gameForUpdate)
        {
            _context.Update(gameForUpdate);
            _context.SaveChanges();
            return gameForUpdate;
        }
        public Game GetPlatform(Game platform)
        {
            return _context.Games.Find(platform);
        }
        public Game GetGenre(Game genre)
        {
            return _context.Games.Find(genre);
        }
        public void DeleteGame(Guid id)
        {
            var gameForDelete = _context.Games.Find(id);
            _context.Games.Remove(gameForDelete);
            _context.SaveChanges();
        }
    }
}
