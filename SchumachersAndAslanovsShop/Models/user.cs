using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchumachersAndAslanovsShop.Models
// This class represents a user in the system. It includes properties for the user's unique identifier, username, password, name, surname, email, telephone number, and role. The class is decorated with data annotations to define database schema details and constraints, such as the table name and column mappings. This allows for easy integration with Entity Framework Core for database operations related to users.
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