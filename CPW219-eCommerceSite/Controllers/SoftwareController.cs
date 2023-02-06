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

        public async Task<IActionResult> Index(int? id)
        {
            const int NumberSoftwareToDisplay = 10;
            
            // Need a page offset to use current page and figure out, number of software programs to skip
            const int PageOffset = 1;
            
            // Set currPage to id if it has a value, otherwise use 1
            int currPage = id ?? 1;

            int totalNumberOfProducts = await _context.Product.CountAsync();
            double MaxNumberOfPages = Math.Ceiling((double)totalNumberOfProducts / NumberSoftwareToDisplay);

            // Rounding pages up, to next whole page number
            int lastPage = Convert.ToInt32(MaxNumberOfPages);

            // Get all Products from the DB (method Syntax)
            // List<Product> products = _context.Product.Skip(NumberSoftwareToDisplay * (currPage - PageOffset)).Take(NumberSoftwareToDisplay).ToList();

            /**
             * Only allows log in people to see the catalog
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Members");
            }
            */


            //Get all Products from the DB (Query Syntax)
            List<Product> products = await (from product in _context.Product select product)
                .Skip(NumberSoftwareToDisplay * (currPage - PageOffset))
                .Take(NumberSoftwareToDisplay)
                .ToListAsync();

            SoftwareCatalogViewModel catalogModel = new(products, lastPage, currPage);

            // Show them on the page
            return View(catalogModel);
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Product productToEdit = await _context.Product.FindAsync(id);
            if (productToEdit == null)
            {
                return NotFound();
            }
            return View(productToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                // Prepares Insert
                _context.Product.Update(product);

                // add to DB
                // For Async Information in tutorial:
                // https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-6.0#asynchronous-code
                await _context.SaveChangesAsync();

                // Show success message on page
                TempData["Message"] = $"{product.Title} has been updated in the database";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Product productToDelete = await _context.Product.FindAsync(id);
            if (productToDelete == null)
            {
                return NotFound();
            }
            return View(productToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Product productToDelete = await _context.Product.FindAsync(id);

            if (productToDelete != null)
            {
                // Prepares Insert
                _context.Product.Remove(productToDelete);

                // add to DB
                // For Async Information in tutorial:
                // https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-6.0#asynchronous-code
                await _context.SaveChangesAsync();
                TempData["Message"] = productToDelete.Title + " was deleted successfully.";
                return RedirectToAction("Index");

            }
            TempData["Message"] = "This product was already deleted.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Product productDetails = await _context.Product.FindAsync(id);
            if (productDetails == null)
            {
                return NotFound();
            }
            return View(productDetails);
        }
    }
}
