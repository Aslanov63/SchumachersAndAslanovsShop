using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchumachersAndAslanovsShop.Data;
// Controller for managing the car catalog, including displaying car details and listing cars with search, filter,
// and sorting capabilities in an ASP.NET Core MVC application.
namespace SchumachersAndAslanovsShop.Controllers
{
    public class CatalogController : Controller // Handles displaying car details and listing cars with search, filter, and sorting capabilities in the car catalog of an ASP.NET Core MVC application.
    {
        private readonly AppDbContext _context;

        public CatalogController(AppDbContext context)
        {
            _context = context;
        }

        // more details
        public async Task<IActionResult> Details(int? id) // Displays detailed information about a specific car, including its description and category, based on the provided car ID in an ASP.NET Core MVC application.
        {
            if (id == null) return NotFound();

            var car = await _context.Cars
                .Include(c => c.Description) 
                .Include(c => c.Category)   
                .FirstOrDefaultAsync(m => m.CarId == id);

            if (car == null) return NotFound();

            return View(car);
        }

        // CARALOG
        public async Task<IActionResult> Cars(string searchTerm, int? minPrice, int? maxPrice,
            int? minMileage, int? maxMileage, double? minVolume, double? maxVolume,
            bool? onlyCleanTitle, string sortBy)
        {
            var query = _context.Cars.Include(c => c.Description).AsQueryable();

            // SEARCH
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var search = searchTerm.ToLower();
                query = query.Where(c => c.CarBrand.ToLower().Contains(search) || c.CarModel.ToLower().Contains(search));
            }

            // FILTERS
            if (minPrice.HasValue) query = query.Where(c => c.Price >= minPrice);
            if (maxPrice.HasValue) query = query.Where(c => c.Price <= maxPrice);
            if (minMileage.HasValue) query = query.Where(c => c.CarMilage >= minMileage);
            if (maxMileage.HasValue) query = query.Where(c => c.CarMilage <= maxMileage);
            if (minVolume.HasValue) query = query.Where(c => c.Description != null && c.Description.EngineVolume >= minVolume);
            if (maxVolume.HasValue) query = query.Where(c => c.Description != null && c.Description.EngineVolume <= maxVolume);

            // + NOT CRASHED FILTER
            if (onlyCleanTitle == true)
            {
                query = query.Where(c => c.Description != null && c.Description.Crashed == 0);
            }

            // SORTING
            query = sortBy switch
            {
                "price_asc" => query.OrderBy(c => c.Price),
                "price_desc" => query.OrderByDescending(c => c.Price),
                "mile_asc" => query.OrderBy(c => c.CarMilage),
                "mile_desc" => query.OrderByDescending(c => c.CarMilage),
                _ => query.OrderByDescending(c => c.CarId)
            };

            // VIEWBAG FOR REMEMBERING FILTERS
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