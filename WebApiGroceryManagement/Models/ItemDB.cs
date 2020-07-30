using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApiGroceryManagement.Models
{
    public class ItemDB
    {
        static bool local = false;
        static string _conStr = null;

        static string strConLocal = ConfigurationManager.ConnectionStrings["strConLocal"].ConnectionString;
        static string strConLIVEDNS = ConfigurationManager.ConnectionStrings["strConLIVEDNS"].ConnectionString;

        private static List<Item> ExecReader(string commandString)
        {
            List<Item> listToReturn = new List<Item>();
            SqlConnection con = new SqlConnection(_conStr);
            SqlCommand command = new SqlCommand(commandString, con);
            con.Open();

            SqlDataReader dataReader;

            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                int ItemCode = (int)dataReader["ItemCode"];
                int CategoryCode = (int)dataReader["CategoryCode"];
                var Description = dataReader["Description"].ToString();
                int Count = (int)dataReader["Count"];
                int FamilyCode = (int)dataReader["FamilyCode"];
                listToReturn.Add(new Item(ItemCode, CategoryCode, Description, Count, FamilyCode));
            }

            con.Close();

            return listToReturn;
        }

        static ItemDB()
        {
            if (local)
                _conStr = strConLocal;
            else
                _conStr = strConLIVEDNS;
        }

        //READ ALL ITEMS
        public static List<Item> GetAllItems()
        {
            List<Item> listToReturn = ExecReader("SELECT * FROM Items");

            return listToReturn;
        }

        //READ SPECIFIC FAMILY ITEMS
        public static List<Item> GetItemByFamilyCode(int FamilyCode)
        {
            List<Item> listToReturn = ExecReader($"SELECT * FROM Items WHERE FamilyCode = {FamilyCode}");
            return listToReturn;
        }


        //INSET NEW ITEM
        public static int InsertNewItem(int CategoryCode, string Description, int Count, int FamilyCode)
        {
            SqlConnection con = new SqlConnection(_conStr);
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = $"INSERT INTO Items (CategoryCode,Description,Count,FamilyCode) VALUES( {CategoryCode}, '{Description}', {Count},{FamilyCode}) ";

            con.Open();
            int rowsAffected = command.ExecuteNonQuery(); //(returns the number of rows affected)
            int itemId = -1;
            command.CommandText = "SELECT SCOPE_IDENTITY() as [IDENTITY]";
            SqlDataReader dataReader;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                itemId = int.Parse(dataReader["IDENTITY"].ToString());
            command.Connection.Close();
            return itemId;
        }


        //UPDATE
        public static int UpdateItem(int ItemCode, int CategoryCode, string Description, int Count, int FamilyCode)
        {
            SqlConnection con = new SqlConnection(_conStr);
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = $@"UPDATE Items SET CategoryCode = {CategoryCode}, Description = '{Description}' ,
                                  Count = {Count} 
                                  WHERE FamilyCode = {FamilyCode} AND ItemCode = {ItemCode}";

            con.Open();
            int rowsAffected = command.ExecuteNonQuery();
            command.Connection.Close();

            return rowsAffected;
        }

        //DELETE
        public static int DeleteItem(int ItemCode, int FamilyCode)
        {
            SqlConnection con = new SqlConnection(_conStr);
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = $@"DELETE FROM Items WHERE ItemCode = {ItemCode} AND FamilyCode ={FamilyCode}";

            con.Open();
            int rowsAffected = command.ExecuteNonQuery();
            command.Connection.Close();

            return rowsAffected;
        }
    }
}