using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.API.Models
{
    public class BasketModel
    {
        public string UserId { get; set; }

        public int BasketId { get; set; }

        public List<ProductModel> ProductsInBasket { get; set; }
    }
}
