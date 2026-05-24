using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchumachersAndAslanovsShop.Models
//This class represents a category of parts in the system. It includes properties for the category's unique identifier, name, and URL. The class is decorated with data annotations to define database schema details, such as the table name and column mappings. This allows for easy integration with Entity Framework Core for database operations related to part categories.
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