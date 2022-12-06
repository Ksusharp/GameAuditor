using GameAuditor.Models.Interfaces;

namespace GameAuditor.Repositories.Interfaces
{
    public interface IEntityRepository<T> where T : EntityBase
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        void Create(T entity);
        void CreateRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(Guid id);
        //void RemoveAll(Guid id);
        T GetTag(T tag);
        T GetGenre(T genre);
        T GetPlatform(T platform);
        void Save();
    }
}
