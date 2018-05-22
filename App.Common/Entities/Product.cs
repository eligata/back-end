using System;
using System.Collections.Generic;
using System.Text;

namespace App.Common.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Currency { get; set; }

        public decimal Price { get; set; }

        public decimal VAT { get; set; }
    }
}
