using Entity;
using Manager.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CoreWebApi.Controllers
{
    [Authorize]  
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController:ControllerBase
    {
        IProductManager productManager;

        public ProductsController(IProductManager product)
        {
            productManager=product;
        }

       [HttpGet]
       [Route("GetProductsList")]
        public ActionResult GetProductsList()
        {
           var product =productManager.GetProductList();
           if(product.Count>0){
               return Ok(product);
           }
           return BadRequest();
        }

         [HttpGet("{productID}")]
         [Route("GetProductByID")]
        public ActionResult GetProductByID(int productID)
        {
           if(productID>0)
           {
               return Ok(productManager.GetByID(productID));
           }
           else
             return BadRequest();
        }

        [HttpPost]
        [Route("AddProduct")]
        public ActionResult AddProduct(Products products)
        {
              if(ModelState.IsValid)
              {
                    productManager.Add(products);
                    return StatusCode(201);
                
              }
              return BadRequest();
        }

        [HttpPost]
        [Route("UpdateProduct")]
        public ActionResult Update([FromBody]Products products)
        {
            if(ModelState.IsValid)
            {
               productManager.Update(products);
               return StatusCode(202);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("DeleteProduct")]
        public ActionResult DeleteProduct(Products products)
        {
              if(products.product_id==0)
              {
                  return BadRequest();
              }
              else
              {
                  productManager.Delete(products);
                  return StatusCode(200);
              }              
        }
    }
}