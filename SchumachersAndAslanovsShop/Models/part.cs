using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchumachersAndAslanovsShop.Models
{
    [Table("PART")]
    public class Part
    {
        [Key]
        [Column("PART_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int PartId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("PART_NAME")]
        public string PartName { get; set; } = string.Empty;

        [Required]
        [Column("PART_PRICE", TypeName = "decimal(10, 2)")]
        public decimal PartPrice { get; set; }

        [Required]
        [Column("PART_QUANTITY")]
        public int PartQuantity { get; set; }

        [MaxLength(100)]
        [Column("PART_MANUFACTURER")] 
        public string? PartManufacturer { get; set; }

        [MaxLength(50)]
        [Column("PART_MATERIAL")]
        public string? PartMaterial { get; set; }

        [Column("CATEGORY_ID")]
        public int? PartCategoryId { get; set; } 

        [Column("CAR_ID")]
        public int? CarId { get; set; } 

        
      

     

        [ForeignKey("PartCategoryId")] 
        public virtual PartCategory? Category { get; set; }

    }
}