using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApiGroceryManagement.Models
{
    public class FamilyDB
    {

        static bool local = false;
        static string _conStr = null;

        static string strConLocal = ConfigurationManager.ConnectionStrings["strConLocal"].ConnectionString;
        static string strConLIVEDNS = ConfigurationManager.ConnectionStrings["strConLIVEDNS"].ConnectionString;

        private static List<Family> ExecReader(string commandString)
        {
            List<Family> listToReturn = new List<Family>();
            SqlConnection con = new SqlConnection(_conStr);
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            con.Open();
            command.CommandText = commandString;
            SqlDataReader dataReader;

            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                var FamilyCode = (int)dataReader["FamilyCode"];
                var FamilyName = dataReader["FamilyName"].ToString();
                var Password = dataReader["Password"].ToString();
                var ManagerName = dataReader["ManagerName"].ToString();
                var FamilyEmail = dataReader["FamilyEmail"].ToString();
                var FamilyImage = dataReader["FamilyImage"].ToString();
                var Token = dataReader["Token"].ToString();
                listToReturn.Add(new Family(FamilyCode, FamilyName, FamilyEmail, Password, ManagerName, FamilyImage, Token));
            }

            con.Close();
            return listToReturn;
        }



        static FamilyDB()
        {
            if (local)
                _conStr = strConLocal;
            else
                _conStr = strConLIVEDNS;
        }

        //READ ALL
        public static List<Family> GetAllFamilies()
        {
            List<Family> listToReturn = ExecReader("SELECT * FROM Families");

            return listToReturn;
        }

        //check if family email allready exist
        public static Family GetFamilyByEmail(string Email)
        {
            Family famToReturn = ExecReader($"SELECT * FROM Families WHERE FamilyEmail = '{Email}' ").ElementAtOrDefault(0);
            return famToReturn;
        }

        //READ SPECIFIC
        public static Family GetFamilyByEmailAndPassword(string Email, string Password)
        {
            Family famToReturn = null;
            string command = $"SELECT * FROM Families " +
                                  $"WHERE FamilyEmail = '{Email}' " +
                                  $" And Password = '{Password}'";
            famToReturn = ExecReader(command).ElementAtOrDefault(0);
            
            return famToReturn;
        }


        //INSERT FAMILY
        public static int InsertNewFamily(string FamilyName, string FamilyEmail, string Password, string ManagerName, string FamilyImage, string Token)
        {
            SqlConnection con = new SqlConnection(_conStr);
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = $"INSERT INTO Families (FamilyName,Password,ManagerName,FamilyEmail,FamilyImage,Token) VALUES('{FamilyName}', '{Password}', '{ManagerName}', '{FamilyEmail}','{FamilyImage}','{Token}') ";

            con.Open();
            command.ExecuteNonQuery();
            int FamilyId = -1;
            //select the last inserted identity (use for auto identity table)
            command.CommandText = "SELECT SCOPE_IDENTITY() as [IDENTITY]";
            SqlDataReader dataReader;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                FamilyId = int.Parse(dataReader["IDENTITY"].ToString());
            command.Connection.Close();
            return FamilyId;
        }



        //UPDATE FAMILY
        public static int UpdateFamily(string FamilyName, string FamilyEmail, string Password, string ManagerName, string FamilyImage, string Token)
        {
            SqlConnection con = new SqlConnection(_conStr);
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = $@"UPDATE Families SET FamilyName = '{FamilyName}', Password = '{Password}', FamilyImage = '{FamilyImage}' ,
                                  ManagerName = '{ManagerName}' ,Token='{Token}'
                                  WHERE FamilyEmail = '{FamilyEmail}'";

            con.Open();
            int rowsAffected = command.ExecuteNonQuery();
            command.Connection.Close();

            return rowsAffected;
        }








        //DELETE FAMILY
        public static int DeleteFamily(string FamilyEmail)
        {
            SqlConnection con = new SqlConnection(_conStr);
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = $@"DELETE FROM Families WHERE FamilyEmail = '{FamilyEmail}'";

            con.Open();
            int rowsAffected = command.ExecuteNonQuery();
            command.Connection.Close();

            return rowsAffected;
        }
    }
}