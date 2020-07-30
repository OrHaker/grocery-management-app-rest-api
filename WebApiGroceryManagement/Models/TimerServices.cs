using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace WebApiGroceryManagement.Models
{

    //public class NotesComparer : IComparer
    //{
    //    public int Compare(object x, object y)
    //    {
    //        NoteWithToken n1 = x as NoteWithToken;
    //        NoteWithToken n2 = y as NoteWithToken;
    //        return string.Compare(n1.FamilyCode.ToString(), n2.FamilyCode.ToString());
    //    }
    //}

    public static class TimerServices
    {

        public static void SendPushToAllTodaysNotes()
        {
            NoteWithToken[] todaysNotes = NoteDB.GetAllTodaysNotes().Distinct().ToArray();


            foreach (var n in todaysNotes)
            {
                if (n.Token == null || n.Token == "")
                    continue;
                SendPush(n);
            }

        }

        public static void SendPushToAllNotes()
        {
            NoteWithToken[] todaysNotes = NoteDB.GetAllNotesWithTokens().Distinct().ToArray();
            foreach (var n in todaysNotes)
            {
                if (n.Token == null || n.Token == "")
                    continue;
                SendPush(n);
            }

        }

        private static dynamic SendPush(NoteWithToken n)
        {
            // Create a request using a URL that can receive a post.   
            WebRequest request = WebRequest.Create("https://exp.host/--/api/v2/push/send");
            // Set the Method property of the request to POST.  
            request.Method = "POST";
            // Create POST data and convert it to a byte array.  
            var objectToSend = new
            {
                to = n.Token,
                title = "יש לך פתקים ברשימה",
                body = "יש לך פתק היום! אל תשכח לעבור על כל הרשימה, יתכן שתצטרך לעדכן משהו ...",
                badge = 7,
                data = new { name = "Notification from server", seconds = DateTime.Now.Second }
            };

            string postData = new JavaScriptSerializer().Serialize(objectToSend);

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/json";
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;
            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();
            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            string returnStatus = ((HttpWebResponse)response).StatusDescription;
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            // Display the content.  
            //Console.WriteLine(responseFromServer);
            // Clean up the streams.  
            reader.Close();
            dataStream.Close();
            response.Close();

            return "success:) --- " + responseFromServer + ", " + returnStatus;




            // Create a request using a URL that can receive a post.
            //WebRequest request = WebRequest.Create("https://exp.host/--/api/v2/push/send");
            //// Set the Method property of the request to POST.  
            //request.Method = "POST";
            //// Create POST data and convert it to a byte array.  
            //var objectToSend = new
            //{
            //    to = n.Token,
            //    title = "You Have a Note for today!",
            //    body = "You have a note today!Don\'t forget to go through the whole list, you may need to update something ...",
            //    badge = 1,
            //    data = ""
            //};

            //string postData = new JavaScriptSerializer().Serialize(objectToSend);

            //byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            //// Set the ContentType property of the WebRequest.  
            //request.ContentType = "application/json";
            //// Set the ContentLength property of the WebRequest.  
            //request.ContentLength = byteArray.Length;
            //// Get the request stream.  
            //Stream dataStream = request.GetRequestStream();
            //// Write the data to the request stream.  
            //dataStream.Write(byteArray, 0, byteArray.Length);
            //// Close the Stream object.  
            //dataStream.Close();
            //// Get the response.  
            //WebResponse response = request.GetResponse();
            //// Display the status.  
            //string returnStatus = ((HttpWebResponse)response).StatusDescription;
            //// Get the stream containing content returned by the server.  
            //dataStream = response.GetResponseStream();
            //// Open the stream using a StreamReader for easy access.  
            //StreamReader reader = new StreamReader(dataStream);
            //// Read the content.  
            //string responseFromServer = reader.ReadToEnd();
            //// Display the content.  
            //// Clean up the streams.  
            //reader.Close();
            //dataStream.Close();
            //response.Close();
        }
            
        
    }
}