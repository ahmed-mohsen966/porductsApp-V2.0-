using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using porductsApp.Data;
using porductsApp.Models;

namespace porductsApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(ApplicationDbContext context , UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var products = _context.Products.ToList();

            return View(products);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var product = new Product()
            {
                Catalogs = _context.Catalogs.ToList()
            };

            return View("ProductFormViewModel" , product);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                var user =await _userManager.FindByNameAsync(User.Identity?.Name);
                if(user != null)
                {
                    product.UserId = user.Id.ToString();
                    product.CreatedBy = User.Identity?.Name ?? string.Empty;
                }
                
                if (!ModelState.IsValid)
                {
                    product.Catalogs = _context.Catalogs.ToList();
                    return View("ProductFormViewModel", product);
                }
                _context.Products.Add(product);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                throw ex;
            }  
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Update(Guid id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if(product != null)
             product.Catalogs = _context.Catalogs.ToList();

            if (product == null)
                return NotFound();

            return View("ProductFormViewModel", product);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Product model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Catalogs = _context.Catalogs.ToList();
                    return View("ProductFormViewModel", model);
                }

                var user = await _userManager.FindByNameAsync(User.Identity?.Name);

                var product = new Product()
                {
                    Name = model.Name,
                    CreationDate = model.CreationDate,
                    CreatedBy = model.CreatedBy,
                    StartDate = model.StartDate,
                    Price = model.Price,
                    CatalogId = model.CatalogId
                };

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

                if(product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
