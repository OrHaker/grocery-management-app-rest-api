using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiGroceryManagement.Models
{
    public class Category
    {
        //fields
        private int _categoryCode;
        private string _categoryName;

        //props
        public int CategoryCode { get => _categoryCode; set => _categoryCode = value; }
        public string CategoryName { get => _categoryName; set => _categoryName = value; }
    
        //ctor
        public Category(int categoryCode, string categoryName)
        {
            CategoryCode = categoryCode;
            CategoryName = categoryName;
        }
        public Category(){}

        //methods
        public override string ToString()
        {
            return $"{CategoryCode},{CategoryName}";
        }

    }
}