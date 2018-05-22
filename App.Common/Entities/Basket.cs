using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Common.Entities
{
    public class Basket
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public bool Purchased { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }

        public virtual ICollection<ProductInBasket> ProductsInBasket { get; set; }
    }
}
