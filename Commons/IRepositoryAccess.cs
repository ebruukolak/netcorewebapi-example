using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Commons
{
    public interface IRepositoryAccess<T> where T :class,new()
    {         
        T Get(Expression<Func<T,bool>> filter=null);
         List<T> GetList(Expression<Func<T,bool>> filter=null);
         void Add(T entity); 
         void Update(T entity);
         void Delete(T entity); 
    }
}