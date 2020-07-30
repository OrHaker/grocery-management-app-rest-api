using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiGroceryManagement.Models
{
    public class Item
    {
        //fields
        private int _itemCode;
        private int _categoryCode;
        private string _description;
        private int _count;
        private int _familyCode;
        
        //props
        public int ItemCode { get => _itemCode; set => _itemCode = value; }
        public int CategoryCode { get => _categoryCode; set => _categoryCode = value; }
        public string Description { get => _description; set => _description = value; }
        public int Count { get => _count; set => _count = value; }
        public int FamilyCode { get => _familyCode; set => _familyCode = value; }

        //ctor
        public Item(int itemCode,int categoryCode,string description,int count, int  familyCode)
        {
            ItemCode = itemCode;
            CategoryCode=categoryCode;
            Description = description;
            Count = count;
            FamilyCode = familyCode;
        }
        public Item(){}

        //methods
        public override string ToString()
        {
            return $"{ItemCode},{CategoryCode},{FamilyCode},{Description},{Count}";
        }

    }
}