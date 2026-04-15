using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchumachersAndAslanovsShop.Data;
using SchumachersAndAslanovsShop.Models;

namespace SchumachersAndAslanovsShop.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var items = await _context.ShoppingCarts
                .Include(c => c.Part)
                .Where(c => c.UserId == userId.Value)
                .ToListAsync();

            ViewBag.TotalSum = items.Sum(i => i.Part?.PartPrice ?? 0);
            return View(items);
        }

        public async Task<IActionResult> AddToCart(int PartId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var part = await _context.Part.FirstOrDefaultAsync(p => p.PartId == PartId);
            if (part == null) return NotFound("Part not found.");

            var cartEntry = new ShoppingCart
            {
                UserId = userId.Value,
                PartId = PartId
            };

            try
            {
                _context.ShoppingCarts.Add(cartEntry);
                await _context.SaveChangesAsync();
                TempData["CartMessage"] = $"✅ {part.PartName} added to cart!";
            }
            catch (Exception)
            {
                TempData["CartError"] = "❌ Error adding to cart.";
            }

            return Redirect(Request.Headers["Referer"].ToString() ?? "/");
        }

        public async Task<IActionResult> Clear()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var items = await _context.ShoppingCarts.Where(c => c.UserId == userId.Value).ToListAsync();
            if (items.Any())
            {
                _context.ShoppingCarts.RemoveRange(items);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var items = await _context.ShoppingCarts
                .Include(c => c.Part)
                .Where(c => c.UserId == userId.Value)
                .ToListAsync();

            if (!items.Any()) return RedirectToAction("Index");

            var newOrder = new Order
            {
                UserId = userId.Value,
                OrderDate = DateTime.Now,
                Status = "1",
                TotalPrice = items.Sum(i => i.Part?.PartPrice ?? 0)
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            foreach (var cartItem in items)
            {
                var orderDetail = new OrderItem
                {
                    OrderId = newOrder.Id,
                    PartId = cartItem.PartId,
                    Quantity = 1,
                    PriceAtPurchase = cartItem.Part?.PartPrice ?? 0
                };
                _context.OrderItems.Add(orderDetail);
            }

            _context.ShoppingCarts.RemoveRange(items);
            await _context.SaveChangesAsync();

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}