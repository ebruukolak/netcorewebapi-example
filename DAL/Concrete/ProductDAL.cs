using Commons.DataAccess.Repository;
using DAL.Abstract;
using Entity;

namespace DAL.Concrete
{
    public class ProductDAL:RepositoryAccess<Products,EFContext>,IProductDAL    
    {
        
    }
}