using System.Collections.Generic;
using DAL.Abstract;
using Entity;
using Manager.Abstract;

namespace Manager.Concrete
{
    public class SupplierManager : ISupplierManager
    {
        ISupplierDAL supplierDAL ;

        public SupplierManager(ISupplierDAL supplier)
        {
            supplierDAL=supplier;
        }

        public Suppliers GetByID(int ID)
        {
            var suppliers=new Suppliers();
           if(ID>0)
           {
               suppliers=supplierDAL.Get(x=>x.supplier_id==ID);
           }
           return suppliers;
        }

        public List<Suppliers> GetListSupplier()
        {
            return supplierDAL.GetList();
        }
    }
}