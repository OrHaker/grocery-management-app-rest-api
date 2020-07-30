using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiGroceryManagement.Models
{
    public class NoteWithToken:Note, IEquatable<NoteWithToken>
    {
        //fields
        private string _token;


        //props
        public string Token { get => _token; set => _token = value; }


        //ctor
        public NoteWithToken(int noteCode, int familyCode, string description, string timeAndDate, string token)
        {
            FamilyCode = familyCode;
            NoteCode = noteCode;
            Description = description;
            TimeAndDate = timeAndDate;
            Token = token;
        }
        public NoteWithToken() { }


        //methods
        public override string ToString()
        {
            return $"{FamilyCode},{NoteCode},{Description},{TimeAndDate},{Token}";
        }

      

        public bool Equals(NoteWithToken other)
        {
            return ((other as NoteWithToken).FamilyCode).Equals(FamilyCode);
        }

        public override int GetHashCode()
        {
            return FamilyCode;
        }
    }
}