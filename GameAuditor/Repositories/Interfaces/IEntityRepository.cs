using GameAuditor.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using GameAuditor.Models.Interfaces;

namespace GameAuditor.Repositories.Interfaces
{
    public interface IEntityRepository<T> where T : EntityBase
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
