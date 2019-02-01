using Commons.DataAccess.Repository;
using DAL.Abstract;
using Entity;

namespace DAL.Concrete
{
    public class SupplierDAL:RepositoryAccess<Suppliers,EFContext>,ISupplierDAL
    {
        
    }
}