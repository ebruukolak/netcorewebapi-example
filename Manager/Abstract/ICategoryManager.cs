using System.Collections.Generic;
using Entity;

namespace Manager.Abstract
{
    public interface ICategoryManager
    {
        Categories GetByID(int categoryID);
         List<Categories> GetCategoryList();
         void Add(Categories c);
         void Update(Categories c);
         void Delete(int categoryID);
    }
}