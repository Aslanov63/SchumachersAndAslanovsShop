using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchumachersAndAslanovsShop.Models
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