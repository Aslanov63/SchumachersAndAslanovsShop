using Microsoft.AspNetCore.Mvc;
using SchumachersAndAslanovsShop.Data;
using System.Xml;
using System.ServiceModel.Syndication;


namespace SchumachersAndAslanovsShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult GetNews(string url)
        {
            try
            {
                using var reader = XmlReader.Create(url);
                var feed = SyndicationFeed.Load(reader);
                var items = feed.Items.Take(5).Select(i => new
                {
                    title = i.Title.Text,
                    link = i.Links.FirstOrDefault()?.Uri.ToString(),
                    date = i.PublishDate.ToString("g")
                });
                return Json(items);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
    }