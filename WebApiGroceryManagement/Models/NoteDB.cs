using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApiGroceryManagement.Models
{
    public class NoteDB
    {
        static bool local = false;
        static string _conStr = null;

        static string strConLocal = ConfigurationManager.ConnectionStrings["strConLocal"].ConnectionString;
        static string strConLIVEDNS = ConfigurationManager.ConnectionStrings["strConLIVEDNS"].ConnectionString;



        private static List<Note> ExecReader(string stringCommand)
        {
            List<Note> listToReturn = new List<Note>();
            SqlConnection con = new SqlConnection(_conStr);
            SqlCommand command = new SqlCommand();
            command.Connection = con;

            con.Open();
            command.CommandText = stringCommand;
            SqlDataReader dataReader;

            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                var NoteCode = (int)dataReader["NoteCode"];
                var FamilyCode = (int)dataReader["FamilyCode"];
                var Description = dataReader["Description"].ToString();
                var TimeAndDate = dataReader["TimeAndDate"].ToString();
                listToReturn.Add(new Note(NoteCode, FamilyCode, Description, TimeAndDate));
            }

            con.Close();

            return listToReturn;
        }

        static NoteDB()
        {
            if (local)
                _conStr = strConLocal;
            else
                _conStr = strConLIVEDNS;
        }

        //READ ALL NOTES
        public static List<Note> GetAllNote()
        {
            List<Note> listToReturn = ExecReader("SELECT * FROM Notes");
            return listToReturn;
        }


        //READ SPECIFIC FAMILY NOTES
        public static List<Note> GetAllNotesByFamilyCode(int FamilyCode)
        {
            List<Note> listToReturn = ExecReader($"SELECT * FROM Notes WHERE FamilyCode ={FamilyCode}");
            return listToReturn;
        }


        //GET ALL THE NOTES OF TODAY
        public static List<NoteWithToken> GetAllTodaysNotes()
        {
            List <Family>listOfFamilys = FamilyDB.GetAllFamilies();

            List<Note> listOfNotes = ExecReader("SELECT * FROM Notes");
            var todaysNotes = from n in listOfNotes
                              from f in listOfFamilys
                              where n.IsToday() is true && f.FamilyCode==n.FamilyCode
                              
                              select new NoteWithToken(n.NoteCode,n.FamilyCode,n.Description,n.TimeAndDate,f.Token);

            return todaysNotes.ToList();
        }

        //GET ALL THE NOTES 
        public static List<NoteWithToken> GetAllNotesWithTokens()
        {
            List<Family> listOfFamilys = FamilyDB.GetAllFamilies();

            List<Note> listOfNotes = ExecReader("SELECT * FROM Notes");
            var todaysNotes = from n in listOfNotes
                              from f in listOfFamilys
                              where  f.FamilyCode == n.FamilyCode
                              select new NoteWithToken(n.NoteCode, n.FamilyCode, n.Description, n.TimeAndDate, f.Token);

            return todaysNotes.ToList();
        }


        //DELETE SPECIFIC NOTE
        public static int DeleteNote(int NoteCode, int FamilyCode)
        {
            SqlConnection con = new SqlConnection(_conStr);
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = $@"DELETE FROM Notes WHERE NoteCode = {NoteCode} AND FamilyCode = {FamilyCode}";

            con.Open();
            int rowsAffected = command.ExecuteNonQuery();
            command.Connection.Close();

            return rowsAffected;
        }

        //INSERT NEW NOTE
        public static int InsertNewNote(int FamilyCode, string Description, string TimeAndDate)
        {
            SqlConnection con = new SqlConnection(_conStr);
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = $"INSERT INTO Notes (FamilyCode,Description,TimeAndDate) VALUES({FamilyCode},'{Description}','{TimeAndDate}') ";

            con.Open();
            int noteId = command.ExecuteNonQuery(); //(returns the number of rows affected)
            command.CommandText = "SELECT SCOPE_IDENTITY() as [IDENTITY]";
            SqlDataReader dataReader;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                noteId = int.Parse(dataReader["IDENTITY"].ToString());
            command.Connection.Close();
            return noteId;
        }

        //UPDATE
        public static int UpdateNote(int FamilyCode, int NoteCode, string Description, string TimeAndDate)
        {
            SqlConnection con = new SqlConnection(_conStr);
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = $@"UPDATE Notes SET  Description = '{Description}', TimeAndDate='{TimeAndDate}' 
                                  WHERE FamilyCode = {FamilyCode} AND NoteCode = {NoteCode}";

            con.Open();
            int rowsAffected = command.ExecuteNonQuery();
            command.Connection.Close();

            return rowsAffected;
        }
    }
}