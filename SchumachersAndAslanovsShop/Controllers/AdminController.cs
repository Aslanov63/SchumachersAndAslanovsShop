using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchumachersAndAslanovsShop.Data;
using SchumachersAndAslanovsShop.Models;

namespace SchumachersAndAslanovsShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // Проверка прав доступа
        private bool IsAdmin => HttpContext.Session.GetString("UserRole") == "Admin";
        private bool IsStaff => IsAdmin || HttpContext.Session.GetString("UserRole") == "Moder";

        // --- ГЛАВНАЯ ПАНЕЛЬ (Краткий обзор) ---
        public async Task<IActionResult> Dashboard()
        {
            if (!IsStaff) return RedirectToAction("Login", "Account");

            var orders = await _context.Orders
                .Include(o => o.Car)
                .Include(o => o.User) // Добавили юзера и сюда для информативности
                .OrderByDescending(o => o.OrderDate)
                .Take(10)
                .ToListAsync();

            return View(orders);
        }

        // --- УПРАВЛЕНИЕ ЗАКАЗАМИ (Фильтрация, Сортировка, Список) ---
        public async Task<IActionResult> OrderDashboard(string searchTerm, string status)
        {
            if (!IsStaff) return RedirectToAction("Login", "Account");

            var query = _context.Orders
                .Include(o => o.Car)
                .Include(o => o.User) // Теперь юзер будет виден в таблице
                .Include(o => o.OrderItems)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(o => o.UserId.ToString().Contains(searchTerm));

            if (!string.IsNullOrEmpty(status))
                query = query.Where(o => o.Status == status);

            var orders = await query.OrderByDescending(o => o.OrderDate).ToListAsync();

            ViewBag.CurrentSearch = searchTerm;
            ViewBag.CurrentStatus = status;

            return View(orders);
        }

        // --- ДЕТАЛИ ЗАКАЗА (Полная информация: Юзер + Машина + Запчасти) ---
        public async Task<IActionResult> OrderDetails(int id)
        {
            if (!IsStaff) return Forbid();

            var order = await _context.Orders
                .Include(o => o.User) // Навигационное свойство из модели Order
                .Include(o => o.Car).ThenInclude(c => c.Description)
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Part)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            return View(order);
        }

        // --- ОБНОВЛЕНИЕ СТАТУСА ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, string newStatus)
        {
            if (!IsStaff) return Forbid();

            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return NotFound();

            order.Status = newStatus;
            await _context.SaveChangesAsync();

            // Возвращаемся на страницу деталей, чтобы увидеть результат
            return RedirectToAction(nameof(OrderDetails), new { id = orderId });
        }

        // --- УПРАВЛЕНИЕ ПЕРСОНАЛОМ (Только Admin) ---
        public async Task<IActionResult> Staff(string searchTerm)
        {
            if (!IsAdmin) return RedirectToAction(nameof(Dashboard));

            IQueryable<User> usersQuery = _context.Users;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var lowerSearch = searchTerm.ToLower();
                usersQuery = usersQuery.Where(u =>
                    u.Username.ToLower().Contains(lowerSearch) ||
                    u.Name.ToLower().Contains(lowerSearch) ||
                    u.Surname.ToLower().Contains(lowerSearch));
            }

            ViewBag.CurrentSearch = searchTerm;
            return View(await usersQuery.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(int userId, string newRole)
        {
            if (!IsAdmin) return Forbid();

            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.UserRole = newRole;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Staff));
        }

        // --- УПРАВЛЕНИЕ КОНТЕНТОМ (Машины и Запчасти) ---
        public async Task<IActionResult> ManageParts()
        {
            if (!IsStaff) return Forbid();
            return View(await _context.Part.Include(p => p.Category).OrderByDescending(p => p.PartId).ToListAsync());
        }

        public async Task<IActionResult> ManageCars()
        {
            if (!IsStaff) return Forbid();
            return View(await _context.Cars.OrderByDescending(c => c.CarId).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (!IsStaff) return Forbid();

            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(OrderDashboard));
        }
    }
}