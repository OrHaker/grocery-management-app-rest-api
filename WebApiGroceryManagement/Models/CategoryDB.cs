using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;

namespace WebApiGroceryManagement.Models
{
    public static class CategoryDB
    {


        static bool local = false;
        static string _conStr = null;
       
        static string strConLocal = ConfigurationManager.ConnectionStrings["strConLocal"].ConnectionString;
        static string strConLIVEDNS = ConfigurationManager.ConnectionStrings["strConLIVEDNS"].ConnectionString;


        private static List<Category> ExecReader(string commandString)
        {
            List<Category> listToReturn = new List<Category>();
            SqlConnection con = new SqlConnection(_conStr);
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            con.Open();
            command.CommandText = commandString;
            SqlDataReader dataReader;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                listToReturn.Add(new Category(int.Parse(dataReader["CategoryCode"].ToString()), dataReader["CategoryName"].ToString()));
            con.Close();
            return listToReturn;
        }


         static CategoryDB(){

            if (local)
                _conStr = strConLocal;
            else
                _conStr = strConLIVEDNS;
        }


        public static List<Category> GetAllCategories()
        {
            List<Category> listToReturn = new List<Category>();
            listToReturn = ExecReader( "SELECT * FROM Categories");
            return listToReturn;
        }

        public static Category GetCategoryByCategoryCode(int CategoryCode)
        {
            Category catToReturn = ExecReader($"SELECT * FROM Categories WHERE CategoryCode = {CategoryCode}").ToArray()[0];
            return catToReturn;
        }


    }
}