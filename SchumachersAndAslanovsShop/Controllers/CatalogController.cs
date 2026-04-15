using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchumachersAndAslanovsShop.Data;

namespace SchumachersAndAslanovsShop.Controllers
{
    public class CatalogController : Controller
    {
        private readonly AppDbContext _context;

        public CatalogController(AppDbContext context)
        {
            _context = context;
        }

        // МЕТОД ДЕТАЛЕЙ (Исправляет ошибку 404)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var car = await _context.Cars
                .Include(c => c.Description) // Подгружаем тех. характеристики
                .Include(c => c.Category)    // Подгружаем категорию
                .FirstOrDefaultAsync(m => m.CarId == id);

            if (car == null) return NotFound();

            return View(car);
        }

        // МЕТОД КАТАЛОГА
        public async Task<IActionResult> Cars(string searchTerm, int? minPrice, int? maxPrice,
            int? minMileage, int? maxMileage, double? minVolume, double? maxVolume,
            bool? onlyCleanTitle, string sortBy)
        {
            var query = _context.Cars.Include(c => c.Description).AsQueryable();

            // Поиск
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var search = searchTerm.ToLower();
                query = query.Where(c => c.CarBrand.ToLower().Contains(search) || c.CarModel.ToLower().Contains(search));
            }

            // Фильтры
            if (minPrice.HasValue) query = query.Where(c => c.Price >= minPrice);
            if (maxPrice.HasValue) query = query.Where(c => c.Price <= maxPrice);
            if (minMileage.HasValue) query = query.Where(c => c.CarMilage >= minMileage);
            if (maxMileage.HasValue) query = query.Where(c => c.CarMilage <= maxMileage);
            if (minVolume.HasValue) query = query.Where(c => c.Description != null && c.Description.EngineVolume >= minVolume);
            if (maxVolume.HasValue) query = query.Where(c => c.Description != null && c.Description.EngineVolume <= maxVolume);

            // НОВОЕ: Фильтр "No Crashed" (только не битые)
            if (onlyCleanTitle == true)
            {
                query = query.Where(c => c.Description != null && c.Description.Crashed == 0);
            }

            // Сортировка
            query = sortBy switch
            {
                "price_asc" => query.OrderBy(c => c.Price),
                "price_desc" => query.OrderByDescending(c => c.Price),
                "mile_asc" => query.OrderBy(c => c.CarMilage),
                "mile_desc" => query.OrderByDescending(c => c.CarMilage),
                _ => query.OrderByDescending(c => c.CarId)
            };

            // Сохраняем состояние для View
            ViewBag.CurrentSearch = searchTerm;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.MinMileage = minMileage;
            ViewBag.MaxMileage = maxMileage;
            ViewBag.MinVolume = minVolume;
            ViewBag.MaxVolume = maxVolume;
            ViewBag.OnlyCleanTitle = onlyCleanTitle;
            ViewBag.SortBy = sortBy;

            return View(await query.ToListAsync());
        }
    }
}