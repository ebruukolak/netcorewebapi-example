using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Commons.Helper;
using DAL.Abstract;
using Entity;
using Manager.Abstract;

namespace Manager.Concrete
{
    public class UserManager : IUserManager
    {
        IUserDAL userDAL;
        public UserManager(IUserDAL _userDAL)
        {
            userDAL=_userDAL;
        }

        public Users Authenticate(string username, string password)
        {
           if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
             return null;
            
             var user=userDAL.Get(x=>x.UserName==username);
            if(user==null)
              return null;
            if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
              return null;
            
            return user;
        }
        public Users GetByID(int userID)
        {
            return userDAL.Get(x=>x.ID==userID);
        }

        public IEnumerable<Users> GetUserList()
        {
            return userDAL.GetList();
        }
        public void Add(Users u, string password)
        {
            if(string.IsNullOrWhiteSpace(password))
                throw new CustomException("Password is required");
            var user =userDAL.Get(x=>x.UserName==u.UserName);
            if(user!=null)
               throw new CustomException(u.UserName+" already exists.");

            byte[] passwordHash,passwordSalt;
            CreatePaswordHash(password,out passwordHash, out passwordSalt);
             if(passwordHash.Length >0 && passwordSalt.Length>0)
               {
                 u.PasswordHash=passwordHash;
                 u.PasswordSalt=passwordSalt;
               }    
            userDAL.Add(u);            
        }
        
        public void Update(Users u, string password)
        {
            var user =userDAL.Get(x=>x.ID==u.ID);
            if(user==null)
              throw new CustomException("User not found!");
           
            user.FirstName = u.FirstName;
            user.LastName = u.LastName;
            user.UserName = u.UserName;
            
            if(!string.IsNullOrWhiteSpace(password))
            {
               byte[] passwordHash,passwordSalt;
               CreatePaswordHash(password,out passwordHash,out passwordSalt);
               if(passwordHash !=null && passwordSalt!=null)
               {
                 user.PasswordHash=passwordHash;
                 user.PasswordSalt=passwordSalt;
               }             
            }
            
            userDAL.Update(user);             
        }

        public void Delete(int ID)
        {
            var user = userDAL.Get(x=>x.ID==ID);
            if(user!=null)
            {
               userDAL.Delete(user);
            }
        }    
    #region PrivateMethods
        private static void CreatePaswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac= new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (passwordHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (passwordSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using(var hmac = new HMACSHA512(passwordSalt))
            {
               var computedHash= hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
               for(int i=0;i<computedHash.Length;i++)
               {
                  if(computedHash[i] !=passwordHash[i])
                   return false;
               }
            }
            return true;
        }
    #endregion
    }
    
}