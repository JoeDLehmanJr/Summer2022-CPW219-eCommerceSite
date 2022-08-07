using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{
    public class SoftwareController : Controller
    {
        private readonly SoftwareContext _context;

        public SoftwareController(SoftwareContext context)
        {
            _context = context;
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
                _context.Products.Add(product);

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
