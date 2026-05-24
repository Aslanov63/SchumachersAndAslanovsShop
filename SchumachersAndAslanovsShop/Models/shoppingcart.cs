
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchumachersAndAslanovsShop.Models
//This class represents an item in a user's shopping cart. It includes properties for the cart's unique identifier, associated user, and the part that has been added to the cart. The class is decorated with data annotations to define database schema details and relationships for integration with Entity Framework Core.
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