using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchumachersAndAslanovsShop.Models
{
    [Table("CARS")]
    public class Car
    {
        [Key]
        [Column("CAR_ID")]
        public int CarId { get; set; }

        [Column("CAR_BRAND")]
        public string? CarBrand { get; set; }

        [Column("CAR_MODEL")]
        public string? CarModel { get; set; }

        [Column("CAR_ENGINE")]
        public string? CarEngine { get; set; }

        [Column("CAR_IMAGE_URL")]
        public string? CarImageUrl { get; set; }

        [Column("PRICE")]
        public int Price { get; set; }

        [Column("CAR_YEAR")]
        public int? CarYear { get; set; }

        [Column("CAR_MILAGE")]
        public int? CarMilage { get; set; }

        [Column("CATEGORY_ID")]
        public int? CategoryId { get; set; }


        [ForeignKey("CategoryId")]
        public virtual CarCategory? Category { get; set; }

        [Column("DESC_ID")]
        public int? DescId { get; set; }


        [ForeignKey("DescId")]
        public virtual CarDescription? Description { get; set; }
    }
}