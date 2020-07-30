using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiGroceryManagement.Models
{
    public class Family
    {
        //fields
        private int _familyCode;
        private string _familyName;
        private string _email;
        private string _password;
        private string _managerName;
        private string _image;
        private string _token;

        //props
        public int FamilyCode { get => _familyCode; set => _familyCode = value; }
        public string FamilyName { get => _familyName; set => _familyName = value; }
        public string Email { get => _email; set => _email = value; }
        public string Password { get => _password; set => _password = value; }
        public string ManagerName { get => _managerName; set => _managerName = value; }
        public string FamilyImage { get => _image; set => _image = value; }
        public string Token { get => _token; set => _token = value; }

        //ctor
        public Family( int familyCode,string familyName,string email,string password,string managerName, string image, string token)
        {
            FamilyCode = familyCode;
            FamilyName = familyName;
            Email = email;
            Password = password;
            ManagerName = managerName;
            FamilyImage = image;
            Token = token;
        }
        public Family(){}

        //methods
        public override string ToString()
        {
            return $"{FamilyCode},{FamilyName},{Email},{Password},{ManagerName},{FamilyImage},{Token}";
        }
    }
}