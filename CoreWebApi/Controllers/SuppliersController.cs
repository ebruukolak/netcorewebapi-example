using System.Collections.Generic;
using Entity;
using Manager.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController:ControllerBase
    {
        ISupplierManager supplierManager;

        public SuppliersController(ISupplierManager manager)
        {
            supplierManager=manager;
        }

        [HttpGet("{supplierID}")]
        public ActionResult GetSupplierByID(int supplierID)
        {
           if(supplierID==0)
           {
               return BadRequest();
           }
           else
           {
              return    Ok(supplierManager.GetByID(supplierID));
           }
        }

        [HttpGet]
        public ActionResult GetSupplierList()
        {
            var suppliers=supplierManager.GetListSupplier();
            if(suppliers.Count>0)
               return Ok(suppliers);
            else
               return BadRequest();                

        }
    }
}