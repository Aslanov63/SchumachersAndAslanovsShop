using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchumachersAndAslanovsShop.Models
{
    [Table("USERS")]
    public class User
    {
        [Key]
        [Column("USER_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int UserId { get; set; }

        [Column("USERNAME")]
        public string Username { get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("SURNAME")]
        public string Surname { get; set; }

        [Column("GMAIL")]
        public string Gmail { get; set; }

        [Column("TEL_NUMBER")]
        public string TelNumber { get; set; }

        [Column("USER_ROLE")]
        public string UserRole { get; set; }
    }
}