using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchumachersAndAslanovsShop.Data;
using SchumachersAndAslanovsShop.Models;
//  Controller for managing parts, including listing parts by category, searching for parts,
//  and viewing part details in an ASP.NET Core MVC application.
//  It interacts with the database using Entity Framework Core to
//  retrieve part and category information and pass it to the views for display.
namespace SchumachersAndAslanovsShop.Controllers
{
    public class PartsController : Controller
    {
        private readonly AppDbContext _context;

        public PartsController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Categories()
        {
           
            var categories = await _context.PartCategories.ToListAsync();
            return View(categories);
        }


        public async Task<IActionResult> Index(int? categoryId, string searchString)
        {
         
            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentCategory = categoryId;

            IQueryable<Part> query = _context.Part.Include(p => p.Category);

           
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.PartCategoryId == categoryId.Value);
                var category = await _context.PartCategories
                    .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
                ViewBag.CategoryName = category?.CategoryName;
            }

    
            if (!string.IsNullOrEmpty(searchString))
            {
             
                query = query.Where(p => p.PartName.ToUpper().Contains(searchString.ToUpper()));
            }

            var parts = await query.ToListAsync();
            return View(parts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var part = await _context.Part
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PartId == id);

            if (part == null) return NotFound();
            return View(part);
        }
    }
}