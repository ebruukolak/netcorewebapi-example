using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Commons.DataAccess.Repository
{
    public class RepositoryAccess<TEntity, TContext> : IRepositoryAccess<TEntity> where TEntity : class, new()
    where TContext : DbContext, new()
    {

        public TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        {
            using(var context=new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using(var context = new TContext())
            { 
                  return filter==null ? context.Set<TEntity>().ToList() :context.Set<TEntity>().Where(filter).ToList();
            }
        }
        public void Add(TEntity entity)
        {
           using(var context = new TContext())
            { 
                 var addedEntity =context.Entry(entity);
                 addedEntity.State=EntityState.Added;
                 context.SaveChanges();
            }
        }    

        public void Update(TEntity entity)
        {
           using(var context = new TContext())
            { 
                 var updatedEntity =context.Entry(entity);
                 updatedEntity.State=EntityState.Modified;
                 context.SaveChanges();
            }
        }

        
        public void Delete(TEntity entity)
        {
             using(var context = new TContext())
            { 
                 var deletedEntity =context.Entry(entity);
                 deletedEntity.State=EntityState.Deleted;
                 context.SaveChanges();
            }
        }
    }
}