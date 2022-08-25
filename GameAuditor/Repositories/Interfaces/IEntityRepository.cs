using GameAuditor.Models;
using System.Collections.Generic;
using OfficeDevPnP.Core.Framework.Provisioning.Model;

namespace GameAuditor.Repositories.Interfaces
{
    public interface IEntityRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        void Create(T entity);
        void Update(T entity);
        void Delete(Guid id);
        T GetTag(T tag);
        T GetGenre(T genre);
        T GetPlatform(T platform);
        void Save();
    }
}
