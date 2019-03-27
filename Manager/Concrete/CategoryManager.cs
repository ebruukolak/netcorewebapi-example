using System;
using System.Collections.Generic;
using DAL.Abstract;
using Entity;
using Manager.Abstract;

namespace Manager.Concrete
{
    public class CategoryManager : ICategoryManager
    {
         ICategoryDAL categoryDAL;
        public CategoryManager(ICategoryDAL category)
        {
            categoryDAL=category;
        }
      
        public Categories GetByID(int categoryID)
        {
            var category = new Categories();
           if(categoryID>0)
           {
               category =categoryDAL.Get(x=>x.category_id==categoryID);              
           }
           return category;
        }
        public List<Categories> GetCategoryList()
        {
            return categoryDAL.GetList();
        }

        
        public void Add(Categories c)
        {
        
            var category=GetByID(c.category_id);
            if(category==null)
               categoryDAL.Add(c);
            else 
               throw new Exception("Category ID must be uniq");
        }
        public void Update(Categories c)
        {
            var category=GetByID(c.category_id);
            if(category!=null)
               categoryDAL.Update(c);
            else 
               throw new Exception("There is no category");
        }
        public void Delete(int categoryID)
        {
            var category=GetByID(categoryID);
            if(category!=null)
            {
                 categoryDAL.Delete(category);
            }
        }

    }
}