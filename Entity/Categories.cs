using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Categories
    {        
       [Key]
        public int category_id{get;set;}
        public string category_name{get;set;}
        public string description{get;set;}
    }
}