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
    public class NoteController : ApiController
    {

        //READ ALL NOTES
        //http://localhost:51853/GroceryManagement/api/Note
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get()
        {
            try
            {
                Note[] temp = NoteDB.GetAllNote().ToArray();
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        //READ ALL TODAYS NOTES
        //http://localhost:51853/GroceryManagement/api/Note?isToday=true
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get(bool isToday)
        {
            try
            {
                NoteWithToken[] temp = NoteDB.GetAllTodaysNotes().ToArray();
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        //READ ONE NOTES BY  FamilyCode
        //http://localhost:51853/GroceryManagement/api/Note/1
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Note[] temp = NoteDB.GetAllNotesByFamilyCode(id).ToArray();
                if (temp.Length > 0) return Ok(temp);
                return Content(HttpStatusCode.NotFound, $"Note of family {id} dont found!");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        //ISERT
        /// <summary>
        /// ADD NOTE TO FAMILY
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        // http://localhost:51853/GroceryManagement/api/Note
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Post([FromBody] Note n)
        {
            try
            {
                int newCode = NoteDB.InsertNewNote(n.FamilyCode, n.Description, n.TimeAndDate);
                n.NoteCode = newCode;
                return Created(new Uri(Request.RequestUri.AbsoluteUri + newCode), n);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        //UPDATE
        /// <summary>
        /// UPDATE ITEM 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        /// //http://localhost:51853/GroceryManagement/api/Note?id=1
        [HttpPut]
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Put(int id, [FromBody] Note n)
        {
            try
            {
                int val = NoteDB.UpdateNote(n.FamilyCode, n.NoteCode, n.Description, n.TimeAndDate);
                if (val > 0) return Content(HttpStatusCode.OK, n);
                else return Content(HttpStatusCode.NotFound, $"Note {n.Description} of family {n.FamilyCode} was not found to update!");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }

        }



        //DELETE
        //http://localhost:51853/GroceryManagement/api/Note?NoteCode=1&FamilyCode=1
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Delete(int NoteCode, int FamilyCode)
        {
            try
            {
                int val = NoteDB.DeleteNote(NoteCode, FamilyCode);

                if (val > 0)
                    return Ok($"Note with NoteCode {NoteCode} and FamilyCode {FamilyCode} Successfully deleted!");
                else return Content(HttpStatusCode.NotFound, $"Note {NoteCode} of family {FamilyCode} was not found to delete!");


            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);

            }


        }
    }
}
