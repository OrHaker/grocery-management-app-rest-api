using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiGroceryManagement.Models;

namespace WebApiGroceryManagement.Controllers
{
    public class FamilyController : ApiController
    {

        //READ
        /// <summary>
        /// READ ALL FAMILIES WITHOUT PASSWORD
        /// </summary>
        /// <returns></returns>
        ///http://localhost:51853/GroceryManagement/api/Family
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get()
        {
            try
            {
                List<Family> temp = FamilyDB.GetAllFamilies();
                if (temp != null)
                {
                    //DELETE PASSWORD AND EMAIL
                    temp.ForEach(f => { f.Password = null; f.Email = null; });
                    return Ok(temp.ToArray());
                }
                else return Content(HttpStatusCode.NotFound, "Famileis dont found!");

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        //READ SPECIFIC FAMILY BY EMAIL AND PASSWORD
        /// <summary>
        /// READ SPECIFIC FAMILY BY EMAIL AND PASSWORD
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        //http://localhost:51853/GroceryManagement/api/Family?email=or@or.com&password=123456
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get(string email, string password)
        {
            try
            {
                Family f = FamilyDB.GetFamilyByEmailAndPassword(email, password);
                if (f != null)
                    return Ok(f);
                else return Content(HttpStatusCode.NotFound, "Family dont found!");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        //ISERT
        /// <summary>
        /// ADD NEW FAMILY
        /// </summary>
        /// <param name="f"></param>
        //http://localhost:51853/GroceryManagement/api/family
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Post([FromBody] Family f)
        {
            try
            {
                Family familyToCheck = FamilyDB.GetFamilyByEmail(f.Email);
                if (familyToCheck != null)//chek if email already taken
                    return Content(HttpStatusCode.Conflict, $"Family with Email '{f.Email}' already exists.");
                int newCode = FamilyDB.InsertNewFamily(f.FamilyName, f.Email, f.Password, f.ManagerName, f.FamilyImage, f.Token);
                f.FamilyCode = newCode;
                return Created(new Uri(Request.RequestUri.AbsoluteUri + f.Email + f.Password), f);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        //UPDATE
        /// <summary>
        /// UPDATE BY EMAIL
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        /////http://localhost:51853/GroceryManagement/api/family?id=1
        [HttpPut]
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Put(int id, [FromBody] Family f)
        {
            try
            {
                int val = FamilyDB.UpdateFamily(f.FamilyName, f.Email, f.Password, f.ManagerName, f.FamilyImage, f.Token);
                if (val > 0)
                {
                    Family temp = new Family(f.FamilyCode, f.FamilyName, f.Email, f.Password, f.ManagerName, f.FamilyImage, f.Token);
                    return Content(HttpStatusCode.OK, temp);
                }
                else return Content(HttpStatusCode.NotFound, $"Family {f.FamilyName} with Email {f.Email} was not found to update!");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        //DELETE
        /// <summary>
        /// DELETE FAMILY BY EMAIL, ALOWES ONLEY ON ADMIN NETWORKS
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        //http://localhost:51853/GroceryManagement/api/family?email=m@m.com
        public IHttpActionResult Delete(string Email)//[FromBody] string Email
        {
            try
            {
                int val = FamilyDB.DeleteFamily(Email);

                if (val > 0) return Ok($"Family with Email {Email} Successfully deleted!");
                else return Content(HttpStatusCode.NotFound, $"Family with Email {Email} was not found to delete!");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        
    }
}
