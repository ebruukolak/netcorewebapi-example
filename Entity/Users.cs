using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    [Table("users")]
    public class Users
    { 
        [Key]
        public int ID {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string UserName {get;set;}
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}