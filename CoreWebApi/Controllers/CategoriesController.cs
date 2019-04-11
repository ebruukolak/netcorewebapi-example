using Entity;
using Manager.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApi.Controllers
{
    [Authorize]  
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController:ControllerBase
    {
        ICategoryManager categoryManager;
        public CategoriesController(ICategoryManager manager)
        {
            categoryManager=manager;
        }
        [HttpGet]
        [Route("GetCategoriesList")]
        public ActionResult GetCategoriesList()
        {
           var category =categoryManager.GetCategoryList();
           if(category.Count>0){
               return Ok(category);
           }
           return BadRequest();
        }

         [HttpGet("GetCategoryByID/{categoryID}")]     
        public ActionResult GetCategoryByID(int categoryID)
        {
           if(categoryID>0)
           {
               return Ok(categoryManager.GetByID(categoryID));
           }
           else
             return BadRequest();
        }

        [HttpPost]
        [Route("AddCategory")]
        public ActionResult AddCategory(Categories Categories)
        {
              if(ModelState.IsValid)
              {
                    categoryManager.Add(Categories);
                    return StatusCode(201);
                
              }
              return BadRequest();
        }

        [HttpPost]
        [Route("UpdateCategory")]
        public ActionResult UpdateCategory([FromBody]Categories Categories)
        {
            if(ModelState.IsValid)
            {
               categoryManager.Update(Categories);
               return StatusCode(202);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("DeleteCategory")]
        public ActionResult DeleteCategory(Categories Categories)
        {
              if(Categories.category_id==0)
              {
                  return BadRequest();
              }
              else
              {
                  categoryManager.Delete(Categories.category_id);
                  return StatusCode(200);
              } 
        }

    }
}