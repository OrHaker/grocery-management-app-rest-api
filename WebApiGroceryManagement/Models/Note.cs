using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiGroceryManagement.Models
{
    public class Note
    {
        //fields
        protected int _familyCode;
        protected int _noteCode;
        protected string _description;
        protected string _timeAndDate;


        //props
        public int FamilyCode { get => _familyCode; set => _familyCode = value; }
        public int NoteCode { get => _noteCode; set => _noteCode = value; }
        public string Description { get => _description; set => _description = value; }
        public string TimeAndDate { get => _timeAndDate; set => _timeAndDate = value; }


        //ctor
        public Note(int noteCode, int familyCode, string description, string timeAndDate)
        {
            FamilyCode = familyCode;
            NoteCode = noteCode;
            Description = description;
            TimeAndDate = timeAndDate;
        }
        public Note() { }


        //methods
        public override string ToString()
        {
            return $"{FamilyCode},{NoteCode},{Description},{TimeAndDate}";
        }

        //check if the note is today
        public bool IsToday()
        {
            DateTime d = DateTime.Now;
            var stringDateArray = TimeAndDate.Split('/');
            if (stringDateArray[0] == d.Day.ToString() && stringDateArray[1] == d.Month.ToString() && stringDateArray[2] == d.Year.ToString())
                return true;
            return false;
        }
    }
}