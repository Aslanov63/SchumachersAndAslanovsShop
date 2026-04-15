
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchumachersAndAslanovsShop.Models
{
    [Table("SHOPPING_CART")]
    public class ShoppingCart
    {
        [Key]
        [Column("CART_ID")]
        public int Id { get; set; }

        [Column("USER_ID")]
        public int UserId { get; set; }

        [Column("PART_ID")]
        public int PartId { get; set; }

        [ForeignKey("PartId")]
        public virtual Part Part { get; set; }
    }
}