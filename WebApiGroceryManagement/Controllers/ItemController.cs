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
    public class ItemController : ApiController
    {
        //GET ALL
        /// <summary>
        /// GET ALL ITEMS OF ALL THE FAMILES
        /// </summary>
        /// <returns></returns>
        /// http://localhost:51853/GroceryManagement/api/Item
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get()
        {
            try
            {
                Item[] ia = ItemDB.GetAllItems().ToArray();
                return Ok(ia);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }

        }

        //GET 
        /// <summary>
        /// GET ITEMS OF SPECIFIC FAMILY
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [EnableCors("*", "*", "*")]
        //http://localhost:51853/GroceryManagement/api/Item/1
        public IHttpActionResult Get(int id)
        {
            try
            {
                Item[] ia = ItemDB.GetItemByFamilyCode(id).ToArray();
                if(ia.Length==0) return Content(HttpStatusCode.NotFound, $"Item of family {id} dont found!");
                return Ok(ia);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        //ISERT
        /// <summary>
        /// ADD ITEM TO FAMILY
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        /// http://localhost:51853/GroceryManagement/api/Item
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Post([FromBody] Item i)
        {
            try
            {
                int newCode = ItemDB.InsertNewItem( i.CategoryCode, i.Description, i.Count, i.FamilyCode);
                i.ItemCode = newCode;
                return Created(new Uri(Request.RequestUri.AbsoluteUri + i.FamilyCode), i);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        //UPDATE
        //http://localhost:51853/GroceryManagement/api/Item?id=1
        [HttpPut]
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Put(int id,[FromBody] Item i)
        {
            try
            {
                int val = ItemDB.UpdateItem(i.ItemCode, i.CategoryCode, i.Description, i.Count, i.FamilyCode);
                if (val > 0) return Content(HttpStatusCode.OK, i);
                else return Content(HttpStatusCode.NotFound, $"Item {i.Description} of family {i.FamilyCode} was not found to update!");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }

        }



        //DELETE
        //http://localhost:51853/GroceryManagement/api/Item?ItemCode=1&FamilyCode=2
        /// <summary>
        /// DELETE ITEM BY ITEMCODE AND FAMILY CODE FOR MORE SAFER DELETION 
        /// NOT ONLY ITEM CODE TO MAKE SURE THAT THE SPECIFIC FAMILY DELETE THE ITEM.
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <param name="FamilyCode"></param>
        /// <returns></returns>
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Delete(int ItemCode, int FamilyCode)
        {
            try
            {
                int val = ItemDB.DeleteItem(ItemCode, FamilyCode);

                if (val > 0) return Ok($"Item with ItemCode {ItemCode} and FamilyCode {FamilyCode} Successfully deleted!");
                else return Content(HttpStatusCode.NotFound, $"Item with ItemCode {ItemCode} of {FamilyCode} was not found to delete!");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
