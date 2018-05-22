using System.ComponentModel.DataAnnotations.Schema;

namespace App.Common.Entities
{
    public class ProductInBasket
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int BasketId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("BasketId")]
        public virtual Basket Basket { get; set; }
    }
}
