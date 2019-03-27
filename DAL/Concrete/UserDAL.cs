using Commons.DataAccess.Repository;
using DAL.Abstract;
using Entity;

namespace DAL.Concrete
{
    public class UserDAL:RepositoryAccess<Users,EFContext>,IUserDAL
    {
        
    }
}