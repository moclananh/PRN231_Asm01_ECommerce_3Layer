using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.DataAccess;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;

namespace WebApp.Areas.User.Controllers
{
    [Area("User")]
    public class ProductsController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public ProductsController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:5001/api/Products";
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();


            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(stringData, options);
            return View(listProducts);
        }



        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Make a request to your API to get the product by id
                HttpResponseMessage productResponse = await client.GetAsync($"https://localhost:5001/api/Products/{id}");

                if (productResponse.IsSuccessStatusCode)
                {
                    string productData = await productResponse.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Product product = JsonSerializer.Deserialize<Product>(productData, options);

                    // Make a request to your API to get the list of categories
                    HttpResponseMessage categoriesResponse = await client.GetAsync("https://localhost:5001/api/Categories");

                    if (categoriesResponse.IsSuccessStatusCode)
                    {
                        string categoriesData = await categoriesResponse.Content.ReadAsStringAsync();
                        List<Category> categories = JsonSerializer.Deserialize<List<Category>>(categoriesData, options);

                        // Pass the list of categories and the product to the view
                        ViewData["CategoryViewBag"] = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
                        return View(product);
                    }

                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or errors, e.g., log the error
            }

            return NotFound();
        }
    }
}
