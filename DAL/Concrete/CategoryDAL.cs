using Commons.DataAccess.Repository;
using DAL.Abstract;
using Entity;

namespace DAL.Concrete
{
    public class CategoryDAL:RepositoryAccess<Categories,EFContext>,ICategoryDAL
    {
         
    }
}