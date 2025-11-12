using Mango.Web_MVC.Models;
using Mango.Web_MVC.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Mango.Web_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto>? Productlist = new();
            var response = await _productService.GetAllProductsAsync();

            if(response != null && response.IsSuccess)
            {
                Productlist = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(Productlist);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

       [HttpPost]
       public async Task<IActionResult> ProductCreate(ProductDto model)
       {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductsAsync(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Create Successfull!";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }

            return View(model);
        }

        public async Task<IActionResult> ProductDelete(int ProductId)
        {
            var response = await _productService.GetProductByIdAsync(ProductId);
            if (response != null && response.IsSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
           

            var response = await _productService.DeleteProductsAsync(model.ProductId);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Delete successfull!";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }


            return View(model);
        }

        public async Task<IActionResult> ProductUpdate(int productId)
        {
            ResponseDto? response = await _productService.GetProductByIdAsync(productId);

            if (response != null && response.IsSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _productService.UpdateProductsAsync(productDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Product updated successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(productDto);
        }

    }
}
