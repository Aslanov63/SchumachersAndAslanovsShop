using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchumachersAndAslanovsShop.Models
//This class represents a customer's order in the system. It includes properties for the order's unique identifier, associated user, order date, status, total price, and an optional association to a car. The class also defines relationships to the User and Car entities, as well as a collection of OrderItems that represent the individual items within the order. Data annotations are used to specify database schema details and relationships for integration with Entity Framework Core.
{

    [Table("ORDERS")]
    public class Order
    {
        [Key]
        [Column("ORDER_ID")]
        public int Id { get; set; }

        [Column("USER_ID")]
        public int UserId { get; set; }

        [Column("ORDER_DATE")]
        public DateTime OrderDate { get; set; }

        [Column("STATUS")]
        public string Status { get; set; }

        [Column("TOTAL_PRICE")]
        public decimal TotalPrice { get; set; }

        [Column("CAR_ID")]
        public int? CarId { get; set; }

        [ForeignKey("CarId")]
        public virtual Car? Car { get; set; }





        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}