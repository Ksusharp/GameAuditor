﻿using GameAuditor.Database;
using GameAuditor.Models;
using GameAuditor.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using OfficeDevPnP.Core.Framework.Provisioning.Model;

namespace GameAuditor.Repositories.Implimentations
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        public readonly ApplicationContext context;
        public readonly DbSet<T> dbSet;
        public EntityRepository(ApplicationContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }
        public T Get(Guid id)
        {
            return dbSet.Find(id);
        }
        public void Create(T entity)
        {
            dbSet.Add(entity);
        } 
        
        public void Update(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(Guid id)
        {
            T entity = dbSet.Find(id);
            dbSet.Remove(entity);
        }
        public T GetTag(T tag)
        {
            return dbSet.Find(tag);
        }
        public T GetPlatform(T platform)
        {
            return dbSet.Find(platform);
        }
        public T GetGenre(T genre)
        {
            return dbSet.Find(genre);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
