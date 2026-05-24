using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchumachersAndAslanovsShop.Models
{
    //This class represents the detailed technical specifications of a car in the system. It includes properties for interior color, air conditioning presence, transmission type, wheel drive configuration, engine type and volume, previous ownership count, and crash/paint history. The class is decorated with data annotations to define database schema details and relationships.
    [Table("CAR_DESCRIPTION")] 
    public class CarDescription
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("INTERIOR_COLOR")]
        public string? InteriorColor { get; set; }

        
        [Column("AC")]
        public int? Ac { get; set; }

        [Column("TRANSMISSION")]
        public string? Transmission { get; set; }

        [Column("WHEEL_DRIVE")]
        public string? WheelDrive { get; set; }

        [Column("ENGINE_TYPE")]
        public string? EngineType { get; set; }

        [Column("ENGINE_VOLUME")]
        public double? EngineVolume { get; set; }

        [Column("OWNERS_BEFORE")]
        public int? OwnersBefore { get; set; }

        [Column("CRASHED")]
        public int? Crashed { get; set; }

        [Column("PAINTED")]
        public int? Painted { get; set; }
    }
}