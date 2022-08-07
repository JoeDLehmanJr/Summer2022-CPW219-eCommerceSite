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
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                // Prepares Insert
                _context.Products.Add(product);

                // add to DB
                _context.SaveChanges();
                ViewData["Message"] = $"{product.Title} has been added to the database";

                // Show success message on page
                // return RedirectToAction("Index", "Home");

                return View();
            }
            return View(product);
        }
    }
}
