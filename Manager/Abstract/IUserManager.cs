using System.Collections.Generic;
using Entity;

namespace Manager.Abstract
{
    public interface IUserManager
    {
        Users Authenticate(string username,string password);
         Users GetByID(int userID);
         //List<User> GetUserList();
         IEnumerable<Users> GetUserList();
         void Add(Users u,string password);
         void Update(Users u,string password);
         void Delete(int ID);
         
    }
}