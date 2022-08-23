using GameAuditor.Models;
using System.Collections.Generic;

namespace GameAuditor.Repositories.Interfaces
{
    public interface IEntityRepository
    {
        List<Post> GetAllPosts();
        Post GetPost(Guid id);
        Post CreatePost(Post model);
        Post UpdatePost(Post postForUpdate);
        Post GetTag(Post model);
        void DeletePost(Guid id);
        List<Game> GetAllGames();
        Game GetGame(Guid id);
        Game CreateGame(Game model);
        Game UpdateGame(Game gameForUpdate);
        Game GetPlatform(Game model);
        Game GetGenre(Game model);
        void DeleteGame(Guid id);
    }
}
