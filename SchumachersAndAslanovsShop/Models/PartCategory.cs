using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchumachersAndAslanovsShop.Models
{
    [Table("PART_CATEGORY")] 
    public class PartCategory
    {
        [Key]
        [Column("CATEGORY_ID")]
        public int CategoryId { get; set; }

        [Column("CATEGORY_NAME")]
        public string CategoryName { get; set; }

        [Column("CATEGORY_URL")]
        public string CategoryUrl { get; set; }

    }
}