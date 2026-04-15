using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchumachersAndAslanovsShop.Models
{
    [Table("CAR_CATEGORY")]
    public class CarCategory
    {
        [Key]
        [Column("CATEGORY_ID")]
        public int Id { get; set; }
        [Column("CATEGORY_NAME")]
        public string Name { get; set; }
    }

    
}
