using IsparkDoluluk.DataAccess.Abstract;
using IsparkDoluluk.DataAccess.Concrete.Context;
using IsparkDoluluk.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IsparkDoluluk.DataAccess.Concrete.Repository
{
    public class EfGenericRepository<TEntity> : IGenericDal<TEntity> where TEntity : class, IEntity, new()
    {
        public async Task<List<TEntity>> GetAll()
        {
            using var context = new IsparkDolulukDbContext();
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllByFilter(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new IsparkDolulukDbContext();
            return await context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            using var context = new IsparkDolulukDbContext();
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new IsparkDolulukDbContext();
            return await context.Set<TEntity>().Where(filter).FirstOrDefaultAsync();
        }

        public async Task Update(TEntity entity)
        {
            using var context = new IsparkDolulukDbContext();
            context.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task Add(TEntity entity)
        {
            using var context = new IsparkDolulukDbContext();
            context.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task Remove(TEntity entity)
        {
            using var context = new IsparkDolulukDbContext();
            context.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
