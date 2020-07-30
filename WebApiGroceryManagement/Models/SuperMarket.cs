using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiGroceryManagement.Models
{
    public class SuperMarket
    {
        //fields
        private int _marketCode;
        private string _name;
        private string _address;

        //props
        public int MarketCode { get => _marketCode; set => _marketCode = value; }
        public string Name { get => _name; set => _name = value; }
        public string Address { get => _address; set => _address = value; }

        //ctor
        public SuperMarket(int marketCode,string name,string address)
        {
            MarketCode = marketCode;
            Name = name;
            Address = address;
        }
        public SuperMarket(){}

        //methods
        public override string ToString()
        {
            return $"{MarketCode},{Name},{Address}";
        }

    }
}