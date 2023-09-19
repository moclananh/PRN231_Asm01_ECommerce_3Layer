using BusinessObject.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessObject.DTOs;
using System.Text;
using System;

namespace WebApp.Controllers
{
    public class StatisticalController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public StatisticalController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:5001/api/Statistical";
        }

        public IActionResult Index()
        {
            return View();
        }

        // POST: Authentication/Login
        [HttpGet]
        public async Task<IActionResult> ThongKe(DateTime startDate, DateTime endDate)
        {
            try
            {
                var st = startDate.Date.ToString("MM/dd/yyyy");
                var ed= endDate.Date.ToString("MM/dd/yyyy"); 
                // Create a JSON payload with the email and password

              //  var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.GetAsync(ProductApiUrl+ $"?startDate={st}&endDate={ed}");
                string stringData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                List<Order> list = JsonSerializer.Deserialize<List<Order>>(stringData, options);

                if (response.IsSuccessStatusCode)
                {
                    return View(list);
                }
                else
                {
                    // Handle authentication failure, e.g., display an error message
                    ModelState.AddModelError(string.Empty, "Invalid date.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log the error
                ModelState.AddModelError(string.Empty, "An error occurred during statistical.");
            }


            // If login fails, return the login view with validation errors
            return View();
        }   
    }
}
