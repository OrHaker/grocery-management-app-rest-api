using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiGroceryManagement.Models;

namespace WebApiGroceryManagement.Controllers
{
    //http://localhost:51853/GroceryManagement/api/Categories
    public class CategoriesController : ApiController
    {
        //READ ALL 
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get()
        {
            try
            {
                Category[] c = CategoryDB.GetAllCategories().ToArray();
                return Ok(c);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        //READ ONE BY ID
        //http://localhost:51853/GroceryManagement/api/Categories/1
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Category c = CategoryDB.GetCategoryByCategoryCode(id);
                if (c != null) return Ok(c);
                else return Content(HttpStatusCode.NotFound, "Category Code dont found!");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
