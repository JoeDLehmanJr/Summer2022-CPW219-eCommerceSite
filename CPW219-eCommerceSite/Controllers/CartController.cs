using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CPW219_eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly SoftwareContext _context;
        private const string Cart = "ShoppingCart";

        public CartController(SoftwareContext context)
        {
            _context = context;
        }

        public IActionResult Add(int id)
        {
            Product? productToAdd = _context.Product.Where(s => s.ProductId == id).SingleOrDefault();

            if (productToAdd == null)
            {
                // Software with specified id doesn't exist
                TempData["Message"] = "Sorry that software doesn't exists";
                return RedirectToAction("Index", "Software");
            }

            CartSoftwareViewModel cartProduct = new()
            {
                ProductId = productToAdd.ProductId,
                Title = productToAdd.Title,
                Price = productToAdd.Price
            };

            List<CartSoftwareViewModel> cartProducts = GetExistingCartData();
            cartProducts.Add(cartProduct);

            WriteShoppinCartCookie(cartProducts);

            // ToDo: Add item to a shopping cart cookie
            TempData["Message"] = "Item added to the cart.";
            return RedirectToAction("Index", "Software");
        }

        private void WriteShoppinCartCookie(List<CartSoftwareViewModel> cartProducts)
        {
            string cookieData = JsonConvert.SerializeObject(cartProducts);


            HttpContext.Response.Cookies.Append(Cart, cookieData, new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddYears(1)
            });
        }


        /// <summary>
        /// Return the current list of software titles in the users shopping 
        /// cart cookie. If there is no cookie, an empty list will be return
        /// </summary>
        /// <returns></returns>
        private List<CartSoftwareViewModel> GetExistingCartData()
        {
            string? cookie = HttpContext.Request.Cookies[Cart];
            if (string.IsNullOrWhiteSpace(cookie)) 
            { 
                return new List<CartSoftwareViewModel>(); 
            }
            return JsonConvert.DeserializeObject<List<CartSoftwareViewModel>>(cookie);
        }

        public IActionResult Summary() 
        { 
            // Read shopping cart data and convert to list of view model
            List<CartSoftwareViewModel> cartProducts = GetExistingCartData();
            
            return View(cartProducts); 
        }

        public IActionResult Remove(int id)
        {
            List<CartSoftwareViewModel> cartProducts = GetExistingCartData();
            CartSoftwareViewModel? targetSoftware = cartProducts.FirstOrDefault(s => s.ProductId == id);

            cartProducts.Remove(targetSoftware);
            WriteShoppinCartCookie(cartProducts);
            return RedirectToAction("Summary");
        }
    }
}
