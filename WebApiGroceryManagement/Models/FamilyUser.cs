using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiGroceryManagement.Models
{
    public class FamilyUser
    {
        //fields
        private int _userCode;
        private string _firstName;
        private string _lastName;
        private bool _canEditList;
        private int _familyCode;

        //props
        public int UserCode { get => _userCode; set => _userCode = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public bool CanEditList { get => _canEditList; set => _canEditList = value; }
        public int FamilyCode { get => _familyCode; set => _familyCode = value; }

        //ctor
        public FamilyUser(int userCode,string firstName,string lastName,bool canEditList, int familyCode)
        {
            UserCode = userCode;
            FirstName = firstName;
            LastName = lastName;
            CanEditList = canEditList;
            FamilyCode = familyCode;
        }
        public FamilyUser(){}

        //methods
        public override string ToString()
        {
            return $"{UserCode},{FirstName},{LastName},{CanEditList},{FamilyCode}";
        }
    }
}