using System.Collections.Generic;
using Entity;

namespace Manager.Abstract
{
    public interface IProductManager
    {
        Products GetByID(int ID);
        List<Products> GetProductList();
        void Add(Products product);
        void Update(Products product);
        void Delete(Products product);

    }
}