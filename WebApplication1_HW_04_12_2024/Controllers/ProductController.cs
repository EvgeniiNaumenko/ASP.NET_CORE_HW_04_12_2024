using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1_HW_04_12_2024.Services;
using WebApplication1_HW_04_12_2024.ViewModels;

namespace WebApplication1_HW_04_12_2024.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View("ProductList", products);
        }

        public IActionResult Details(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View("Details", product);
        }

        public IActionResult Create()
        {
            var model = new ProductViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var newProduct = _productService.AddProduct(model);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View("Edit", product);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var updatedProduct = _productService.UpdateProduct(model);

            if (updatedProduct == null)
            {
                return NotFound();
            }

            return RedirectToAction("Details", new { id = updatedProduct.Id });
        }

        public IActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View("Delete", product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var success = _productService.DeleteProduct(id);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

    }
}
