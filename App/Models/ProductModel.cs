using App.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.API.Models
{
    public class ProductModel
    {
        public string UserId { get; set; }

        public int BasketId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Product Product { get; set; }
    }
}
