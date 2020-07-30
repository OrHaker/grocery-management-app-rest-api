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
    public class FamilyUserController : ApiController
    {
        //GET ALL FAMILYUSERS
        //http://localhost:51853/GroceryManagement/api/FamilyUser
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get()
        {

            try
            {
                FamilyUser[] temp = FamilyUserDB.GetAllFamilyUsers().ToArray();
                if (temp != null)
                    return Ok(temp);
                else return Content(HttpStatusCode.NotFound, "Family Users dont found!");

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        //GET ALL FAMILY USERS OF SPECIFIC FAMILY BY FAMILY CODE
        //http://localhost:51853/GroceryManagement/api/FamilyUser?id=1
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get(int id)//[FromBody] int familyId
        {
            try
            {

                FamilyUser[] f = FamilyUserDB.GetFamilyUserByFamilyCode(id).ToArray();
                if (f != null && f.Length > 0)
                    return Ok(f);
                else return Content(HttpStatusCode.NotFound, "Family Users dont found!");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }



        }

        //INSERT NEW FAMILY USER TO FAMILY
        //http://localhost:51853/GroceryManagement/api/FamilyUser
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Post([FromBody] FamilyUser user)
        {
            try
            {
                int newCode = FamilyUserDB.AddFamilyUser(user.FirstName, user.LastName, user.CanEditList, user.FamilyCode);
                user.UserCode = newCode;
                return Created(new Uri(Request.RequestUri.AbsoluteUri + user.FamilyCode), user);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        //UPDATE FAMILY USER  int UserCode
        //http://localhost:51853/GroceryManagement/api/FamilyUser?id=1
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Put(int id,[FromBody] FamilyUser user)
        {
            try
            {
                int val = FamilyUserDB.UpdateFamilyUser(user.FirstName, user.LastName, user.CanEditList, user.FamilyCode, user.UserCode);

                if (val > 0) return Content(HttpStatusCode.OK, user);
                else return Content(HttpStatusCode.NotFound, $"{user.FirstName} {user.LastName} of family code {user.FamilyCode} was not found to update!");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }

        }

        //DELETE FAMILY USER 
        //http://localhost:51853/GroceryManagement/api/FamilyUser?FamilyCode=1&UserCode=1
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Delete(int FamilyCode, int UserCode)//[FromBody] FamilyUser user
        {
            int val = FamilyUserDB.DeleteFamilyUser(UserCode, FamilyCode);//user.UserCode, user.FamilyCode
            if (val > 0) return Ok($"FamilyUser with FamilyCode {FamilyCode} and UserCode {UserCode} Successfully deleted!");
            else return Content(HttpStatusCode.NotFound, $"FamilyUser with FamilyCode {FamilyCode} and UserCode {UserCode}  was not found to delete!");
        }
    }
}
