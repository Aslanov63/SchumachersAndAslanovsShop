using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchumachersAndAslanovsShop.Models
//This class represents a category of cars in the system. It includes properties for the category's unique identifier and name. The class is decorated with data annotations to define database schema details, such as the table name and column mappings. This allows for easy integration with Entity Framework Core for database operations related to car categories.
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
