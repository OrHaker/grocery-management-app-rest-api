using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApiGroceryManagement.Models
{
	public class FamilyUserDB
	{
		static bool local = false;
		static string _conStr = null;


		static string strConLocal = ConfigurationManager.ConnectionStrings["strConLocal"].ConnectionString;
		static string strConLIVEDNS = ConfigurationManager.ConnectionStrings["strConLIVEDNS"].ConnectionString;

		static FamilyUserDB()
		{
			if (local)
				_conStr = strConLocal;
			else
				_conStr = strConLIVEDNS;
		}

		private static List<FamilyUser> ExecReader(string commandString)
		{
			List<FamilyUser> listToReturn = new List<FamilyUser>();
			SqlConnection con = new SqlConnection(_conStr);
			SqlCommand command = new SqlCommand();
			command.Connection = con;
			con.Open();
			command.CommandText = commandString;
			SqlDataReader dataReader;
			dataReader = command.ExecuteReader();
			while (dataReader.Read())
			{
				var UserCode = (int)dataReader["UserCode"];
				var FirstName = dataReader["FirstName"].ToString();
				var LastName = dataReader["LastName"].ToString();
				var CanEditList = (bool)dataReader["CanEditList"];
				var FamilyCode = (int)dataReader["FamilyCode"];
				listToReturn.Add(new FamilyUser(UserCode, FirstName, LastName, CanEditList, FamilyCode));
			}

			con.Close();

			return listToReturn;
		}

		//READ ALL
		public static List<FamilyUser> GetAllFamilyUsers()
		{
			List<FamilyUser> listToReturn = ExecReader("SELECT * FROM FamilyUsers");
			return listToReturn;
		}

		//READ SPECIFIC BY CODE
		public static List<FamilyUser> GetFamilyUserByFamilyCode(int FamilyCode)
		{
			List<FamilyUser> listToReturn = ExecReader($"SELECT * FROM FamilyUsers WHERE FamilyCode=  {FamilyCode}");
			return listToReturn;
		}

		//READ ALL MANAGERS
		//SELECT * FROM MANAGERS --- VIEW
		public static List<FamilyUser> GetFamilyUserMANAGERS()
		{
			List<FamilyUser> listToReturn = ExecReader("SELECT * FROM MANAGERS");
			return listToReturn;
		}

		//INSERT FAMILYUSER
		public static int AddFamilyUser(string FirstName, string LastName, bool CanEditList, int FamilyCode)
		{
			SqlConnection con = new SqlConnection(_conStr);
			SqlCommand command = new SqlCommand();
			command.Connection = con;
			int localCanEdit = CanEditList ? 1 : 0;
			command.CommandText = $"INSERT INTO FamilyUsers (FirstName,LastName,CanEditList,FamilyCode) VALUES('{FirstName}' , '{LastName}', {localCanEdit}, {FamilyCode}) ";

			con.Open();
			int rowsAffected = command.ExecuteNonQuery();
			int newUserId = -1;
			//to get the new IDENTITY
			command.CommandText = "SELECT SCOPE_IDENTITY() as [IDENTITY]";
			SqlDataReader dataReader;
			dataReader = command.ExecuteReader();
			while (dataReader.Read())
				newUserId = int.Parse(dataReader["IDENTITY"].ToString());
			command.Connection.Close();
			return newUserId;

		}

		//UPDATE FAMILY USER DETAILS
		public static int UpdateFamilyUser(string FirstName, string LastName, bool CanEditList, int FamilyCode, int UserCode)
		{
			SqlConnection con = new SqlConnection(_conStr);
			SqlCommand command = new SqlCommand();
			command.Connection = con;
			int localCanEdit = CanEditList ? 1 : 0;
			command.CommandText = $@"UPDATE FamilyUsers SET FirstName = '{FirstName}', LastName = '{LastName}' ,
								  CanEditList = {localCanEdit} 
								  WHERE FamilyCode = {FamilyCode} AND UserCode = {UserCode}";

			con.Open();
			int rowsAffected = command.ExecuteNonQuery();
			command.Connection.Close();

			return rowsAffected;
		}

		//DELETE
		public static int DeleteFamilyUser(int UserCode, int FamilyCode)
		{
			SqlConnection con = new SqlConnection(_conStr);
			SqlCommand command = new SqlCommand();
			command.Connection = con;
			command.CommandText = $@"DELETE FROM FamilyUsers WHERE UserCode = {UserCode} AND FamilyCode ={FamilyCode}";

			con.Open();
			int rowsAffected = command.ExecuteNonQuery();
			command.Connection.Close();

			return rowsAffected;
		}
	}
}