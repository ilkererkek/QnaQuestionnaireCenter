using DataAccess.Abstract.Entity;
using Entity.Abstract.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Entity
{
    public class EntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.Set<TEntity>().Remove(entity);
                context.SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public TEntity? Get(Expression<Func<TEntity, bool>>? filter = null)
        {
            using(var context = new TContext())
            {
                return context.Set<TEntity>().FirstOrDefault(filter);
            }
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>>? filter = null)
        {
            using (var context = new TContext())
            {
                return (filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList());
            }
        }

        public TEntity? GetWithInclude(Expression<Func<TEntity, bool>>? filter = null, List<string>? includes = null)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if(includes != null)
                {
                    includes.ForEach(x => query.Include(x));
                }
                if( filter != null)
                {
                    query.Where(filter);
                }
                return query.AsNoTracking().SingleOrDefault();
            }
        }

        public List<TEntity> GetListWithInclude(Expression<Func<TEntity, bool>>? filter = null, List<string>? includes = null)
        {

            using (var context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (includes != null)
                {
                    includes.ForEach(x => query.Include(x));
                }
                if (filter != null)
                {
                    query.Where(filter);
                }
                return query.AsNoTracking().ToList();
            }
        }

      

       
    }
}
