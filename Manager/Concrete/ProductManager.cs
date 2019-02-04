using System;
using System.Collections.Generic;
using DAL.Abstract;
using Entity;
using Manager.Abstract;

namespace Manager.Concrete
{
    public class ProductManager : IProductManager
    {      
        IProductDAL productDAL;
        public ProductManager(IProductDAL product)
        {
            productDAL=product;
        }

        public Products GetByID(int ID)
        {
            var products= new Products();
            if(ID>0)
            {
                products=productDAL.Get(x=>x.product_id==ID);
            }
            return products;
        }
        public List<Products> GetProductList()
        {
            return productDAL.GetList();
        }

        public void Add(Products product)
        {
           var products=GetByID(product.product_id);
            if(products==null)
               productDAL.Add(product);
            else 
               throw new Exception("Product ID must be uniq");
        }
        public void Update(Products product)
        {
            var products=GetByID(product.product_id);
            if(products!=null)
               productDAL.Update(product);
            else 
               throw new Exception("There is no product.");
        }
        public void Delete(Products product)
        {
            var products=GetByID(product.product_id);
            if(products!=null)
               productDAL.Delete(product);
            else 
               throw new Exception("There is no product.");
           
        }


       
    }
}