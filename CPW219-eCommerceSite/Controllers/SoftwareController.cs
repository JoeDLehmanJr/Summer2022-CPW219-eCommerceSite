using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CPW219_eCommerceSite.Controllers
{
    public class SoftwareController : Controller
    {
        private readonly SoftwareContext _context;

        public SoftwareController(SoftwareContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get all Products from the DB (method Syntax)
            // List<Product> products = _context.Product.ToList();

            //Get all Products from the DB (Query Syntax)
            List<Product> products = await (from product in _context.Product select product).ToListAsync();

            // Show them on the page
            return View(products);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                // Prepares Insert
                _context.Product.Add(product);

                // add to DB
                // For Async Information in tutorial:
                // https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-6.0#asynchronous-code
                await _context.SaveChangesAsync();
                
                // Show success message on page
                ViewData["Message"] = $"{product.Title} has been added to the database";
                return View();
            }
            return View(product);
        }
    }
}
