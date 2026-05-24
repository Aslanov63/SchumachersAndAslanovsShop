using Microsoft.AspNetCore.Mvc;
using SchumachersAndAslanovsShop.Data;
using System.Xml;
using System.ServiceModel.Syndication;
// Controller for the home page and general site information, including fetching news from an RSS feed in an ASP.NET Core MVC application.

namespace SchumachersAndAslanovsShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context) // Initializes the HomeController with the application's database context, allowing it to interact
                                                    // with the database for any necessary operations related to the home page and news fetching.
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
        public IActionResult GetNews(string url) // Fetches news items from the specified RSS feed URL,
                                                 // processes the feed to extract relevant information such as title, link, and publish date,
                                                 // THIS DECISION WAS MADE BY HEAVY SUPPORT OF AI (I WANT ALSO UNDERLINE THE FACT THAT WE DIDNT HAVE ANY OTHER CHOICE) 
        {
            try
            {
                using var reader = XmlReader.Create(url); // Reads the RSS feed from the provided URL using an XML reader, allowing for efficient parsing of the feed's content.
                var feed = SyndicationFeed.Load(reader); // Loads the RSS feed into a SyndicationFeed object, which provides a structured representation of the feed's items and metadata for easier access and manipulation.
                var items = feed.Items.Take(5).Select(i => new // Selects the first 5 items from the loaded RSS feed and projects them into a new anonymous object containing the title, link, and publish date of each news item for easier consumption in the application.
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