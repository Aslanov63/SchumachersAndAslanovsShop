using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchumachersAndAslanovsShop.Data;
using SchumachersAndAslanovsShop.Models;
//Controllers for user registration, login, logout, and profile management in an ASP.NET Core MVC application.
// It interacts with the database using Entity Framework Core to manage user data and sessions.
namespace SchumachersAndAslanovsShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Validate the model and check for existing username and email before creating a new user account.
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "This nickname is already taken.");
                    return View(model);
                }

                var existingEmail = await _context.Users.FirstOrDefaultAsync(u => u.Gmail == model.Gmail);
                if (existingEmail != null)
                {
                    ModelState.AddModelError("Gmail", "This email is already in use.");
                    return View(model);
                }
                // Create a new user with the provided information and save it to the database.
                var user = new User
                {
                    Username = model.Username,
                    Password = MySecurityHasher.HashPassword(model.Password),
                    Name = model.Name,
                    Surname = model.Surname,
                    Gmail = model.Gmail,
                    TelNumber = model.TelNumber,
                    UserRole = "User"
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }
            return View(model);
        }

        [HttpGet]
        // Display the login view for users to enter their credentials.
        public IActionResult Login()   
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            string hashedInput = MySecurityHasher.HashPassword(password);

            var user = await _context.Users
               .FirstOrDefaultAsync(u => u.Username == username && u.Password == hashedInput);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("UserRole", user.UserRole?.Trim() ?? "User");
                HttpContext.Session.SetInt32("UserId", user.UserId);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid nickname or password.");
            return View();
        }
        // Clear the user's session and redirect them to the home page upon logout.
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Profile()
        {
          
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                
                return RedirectToAction("Login");
            }

           
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId.Value);

            if (user == null) return NotFound();

            return View(user);
        }
        // Update the user's profile information in the database and redirect them back to the profile page with a success message.
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(User model)
        {
            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == model.UserId);

            if (user != null)
            {
               
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.TelNumber = model.TelNumber;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Profile updated successfully!";
            }

            return RedirectToAction("Profile");
        }
    }

}