using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchumachersAndAslanovsShop.Data;
using SchumachersAndAslanovsShop.Models;
// Controller for managing orders, including viewing order details, placing new orders for cars,
// and listing the user's past orders in an ASP.NET Core MVC application.
namespace SchumachersAndAslanovsShop.Controllers
{
    public class OrdersController : Controller // Handles order-related operations such as viewing order details, placing new orders for cars, and listing the user's past orders in an ASP.NET Core MVC application.
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Details(int id) // Displays detailed information about a specific order, including the associated car and any parts included in the order, based on the provided order ID and ensuring the user is authorized to view the order in an ASP.NET Core MVC application.
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var order = await _context.Orders
                .Include(o => o.Car)
                    .ThenInclude(c => c.Description) 
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Part)
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);

            if (order == null) return NotFound();

            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> OrderCar(int carId, decimal price) // Places a new order for a specified car with the given price, ensuring the user is logged in and associating the order with the user's ID in an ASP.NET Core MVC application.
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var newOrder = new Order
            {
                UserId = userId.Value,
                OrderDate = DateTime.Now,
                Status = "Pending",
                TotalPrice = price,
                CarId = carId
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return RedirectToAction("Success", "Cart");
        }

        public async Task<IActionResult> MyOrders() // Displays a list of the current user's past orders, including associated car and part details, ensuring the user is logged in and retrieving the orders from the database in an ASP.NET Core MVC application.
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var orders = await _context.Orders
                .Include(o => o.Car)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Part) 
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }
    }
}