using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly SoftwareContext _context;

        public CartController(SoftwareContext context)
        {
            _context = context;
        }

        public IActionResult Add(int id)
        {
            Product? productToAdd = _context.Product.Where( s => s.ProductId == id ).SingleOrDefault();

            if(productToAdd == null)
            {
                // Software with specified id doesn't exist
                TempData["Message"] = "Sorry that software doesn't exists";
                return RedirectToAction("Index", "Software");
            }

            // ToDo: Add item to a shopping cart cookie
            TempData["Message"] = "Item added to the cart.";
            return RedirectToAction("Index", "Software");
        }
    }
}
