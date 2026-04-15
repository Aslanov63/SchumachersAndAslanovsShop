using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchumachersAndAslanovsShop.Models
{
    [Table("ORDER_ITEMS")] 
    public class OrderItem
    {
        [Key]
        [Column("ITEM_ID")]
        public int Id { get; set; }

        [Column("ORDER_ID")]
        public int OrderId { get; set; }

        [Column("PART_ID")]
        public int? PartId { get; set; }

        [Column("QUANTITY")]
        public int Quantity { get; set; }

        [Column("PRICE_AT_PURCHASE")]
        public decimal PriceAtPurchase { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }

        [ForeignKey("PartId")]
        public virtual Part? Part { get; set; }
    }
}