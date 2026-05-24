using System.ComponentModel.DataAnnotations;

namespace SchumachersAndAslanovsShop.Models
//This class represents the data model for user registration in the Schumachers and Aslanovs Shop application. It includes properties for the user's nickname, password, name, surname, email, and phone number. Data annotations are used to enforce validation rules such as required fields, string length constraints, email format, and password confirmation. This model is typically used in the registration process to capture and validate user input before creating a new user account in the system.
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Nickname is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Nickname must be between 3 and 20 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }



        [Required(ErrorMessage = "First name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

       
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address (must contain @)")]
        public string Gmail { get; set; }

       
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\+.*", ErrorMessage = "Phone number must start with '+' (e.g., +371...)")]
        public string TelNumber { get; set; }
    }
}