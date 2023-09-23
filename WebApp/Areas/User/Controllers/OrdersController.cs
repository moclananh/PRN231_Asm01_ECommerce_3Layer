using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.DataAccess;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace WebApp.Areas.User.Controllers
{
    [Area("User")]
    public class OrdersController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";
        private readonly PRN221_OnPEContext _context;
        public OrdersController(PRN221_OnPEContext context)
        {
            _context = context;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:5001/api/Orders";
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

            List<Order> allOrders = JsonSerializer.Deserialize<List<Order>>(stringData, options);

            // Lọc danh sách các đơn hàng chỉ với MemberId tương ứng
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // Sử dụng ?? để xác định giá trị mặc định nếu Session không có giá trị

            List<Order> filteredOrders = allOrders.Where(order => order.MemberId == userId).ToList();

            return View(filteredOrders);
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
                HttpResponseMessage productResponse = await client.GetAsync($"https://localhost:5001/api/Orders/{id}");

                if (productResponse.IsSuccessStatusCode)
                {
                    string productData = await productResponse.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Order order = JsonSerializer.Deserialize<Order>(productData, options);

                    // Make a request to your API to get the list of categories
                    HttpResponseMessage membersResponse = await client.GetAsync("https://localhost:5001/api/Members");

                    if (membersResponse.IsSuccessStatusCode)
                    {
                        string membersData = await membersResponse.Content.ReadAsStringAsync();
                        List<Member> members = JsonSerializer.Deserialize<List<Member>>(membersData, options);

                        // Pass the list of categories and the product to the view
                        ViewData["MembersViewBag"] = new SelectList(members, "MemberId", "Email", order.OrderId);
                        return View(order);

                    }

                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or errors, e.g., log the error
            }

            return NotFound();
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();


            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };


            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                order.MemberId = (int)HttpContext.Session.GetInt32("UserId");
                order.OrderDate = DateTime.Now;
                order.RequiredDate = DateTime.Now;
                var content = new StringContent(JsonSerializer.Serialize(order), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(ProductApiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    // Handle successful creation (e.g., redirect to the product list)
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle errors
                    ModelState.AddModelError(string.Empty, "Error creating the order.");
                }
            }
            return View(order);
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Make a request to your API to get the product by id
                HttpResponseMessage productResponse = await client.GetAsync($"https://localhost:5001/api/Orders/{id}");

                if (productResponse.IsSuccessStatusCode)
                {
                    string productData = await productResponse.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Order order = JsonSerializer.Deserialize<Order>(productData, options);

                    return View(order);
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions or errors, e.g., log the error
            }

            return NotFound();
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                order.OrderDate = DateTime.Now;
                order.RequiredDate = DateTime.Now;
                var content = new StringContent(JsonSerializer.Serialize(order), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{ProductApiUrl}/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Handle successful update (e.g., redirect to the product list)
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle errors
                    ModelState.AddModelError(string.Empty, "Error updating the order.");
                }
            }
            return View(order);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Make a request to your API to get the product by id
                HttpResponseMessage productResponse = await client.GetAsync($"https://localhost:5001/api/Orders/{id}");

                if (productResponse.IsSuccessStatusCode)
                {
                    string productData = await productResponse.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Order order = JsonSerializer.Deserialize<Order>(productData, options);

                    /* // Make a request to your API to get the list of categories
                     HttpResponseMessage categoriesResponse = await client.GetAsync("https://localhost:5001/api/Categories");

                     if (categoriesResponse.IsSuccessStatusCode)
                     {
                         string categoriesData = await categoriesResponse.Content.ReadAsStringAsync();
                         List<Category> categories = JsonSerializer.Deserialize<List<Category>>(categoriesData, options);

                         // Pass the list of categories and the product to the view
                         ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
                         return View(product);
                     }*/
                    return View(order);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or errors, e.g., log the error
            }

            return NotFound();
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{ProductApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Handle successful deletion (e.g., redirect to the product list)
                return RedirectToAction("Index");
            }
            else
            {
                // Handle errors
                return NotFound();
            }
        }
    }
}
