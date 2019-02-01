using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Products
    {
         [Key]
         public int product_id{get;set;}
        public string product_name{get;set;}
        public int supplier_id{get;set;}
         public int category_id{get;set;}
         public double unit_price{get;set;}
         public int discontinued{get;set;}
    }
}