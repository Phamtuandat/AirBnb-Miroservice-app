using Microsoft.AspNetCore.Mvc;
using Mvc.Areas.Product.ViewModels;
using Mvc.Infrastructure;
using Mvc.Models;
using Newtonsoft.Json;

namespace Mvc.Areas.Product.Controllers
{
    [Area("product")]
    [Route("product/[action]")]
    public class ProductController : Controller
    {
        internal string Message = string.Empty;
        internal string BaseUri = string.Empty;

        private readonly HttpClient _httpClient;
        public ProductController(HttpClient httpClient)
        {
            BaseUri = "http://localhost:5116/api/";
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index([FromForm] LocationViewModel model)
        {
            ViewBag.title = "List of product page";
            var location = model;

            var uri = API.Catalog.GetAllProducts(BaseUri, 1, 10, null, null);

            var responseString = await _httpClient.GetStringAsync(uri);

            var catalog = JsonConvert.DeserializeObject<PaginatedItemsViewModel<ProductItem>>(responseString);
            var productList = catalog.Data.Select(c => new ProductViewModel()
            {

                Name = c.Name,
                BrandId = c.BrandId,
                CreateAt = DateTime.Parse(c.CreateAt),
                Description = c.Description,
                ExpiryDate = DateOnly.Parse(c.ExpiryDate),
                Id = c.Id,
                Price = c.Price,
                UpdateAt = DateTime.Parse(c.UpdateAt)

            }).ToList();
            return View(productList);
        }

        [Route("product/item/{id:int}")]
        public async Task<ActionResult> ProductDetail(int id)
        {

            return View();
        }
    }
}
