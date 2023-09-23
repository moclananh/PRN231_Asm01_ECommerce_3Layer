using BusinessObject.DataAccess;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using BusinessObject.DTOs;
using System.Linq;

namespace WebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";
        private readonly PRN221_OnPEContext _context;
        public AuthenticationController(PRN221_OnPEContext context)
        {
            _context = context;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:5001/api/Authentication";
        }

        public IActionResult Login()
        {
            return View();
        }

        // POST: Authentication/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                ModelState.AddModelError(string.Empty, "Email and password are required.");
                return View();
            }

            try
            {
                // Create a JSON payload with the email and password
                
                var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(ProductApiUrl+"/Login", content);
                var user = _context.Members.SingleOrDefault(s => s.Email == request.Email && s.Password == request.Password);
                if (response.IsSuccessStatusCode)
                {
                    
                    if (request.Email.Equals("admin@admin.com"))
                    {
                        HttpContext.Session.SetInt32("UserId", user.MemberId);
                        HttpContext.Session.SetString("email", request.Email);
                        return RedirectToAction("Index", "Members");
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("UserId", user.MemberId);
                        HttpContext.Session.SetString("email", request.Email);
                        //authorization not yet
                        return RedirectToAction("Index", "User");
                    }
                   
                }
                else
                {
                    // Handle authentication failure, e.g., display an error message
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log the error
                ModelState.AddModelError(string.Empty, "An error occurred during login.");
            }

            // If login fails, return the login view with validation errors
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home", new { area = "" });
        }


        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Member member)
        {
            if (string.IsNullOrWhiteSpace(member.Email) || string.IsNullOrWhiteSpace(member.Password))
            {
                ModelState.AddModelError(string.Empty, "Email and password are required.");
                return View();
            }

            try
            {
                // Create a JSON payload with the email and password

                var content = new StringContent(JsonSerializer.Serialize(member), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(ProductApiUrl + "/Register", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "Authentication");
                }
                else
                {
                    // Handle authentication failure, e.g., display an error message
                    ModelState.AddModelError(string.Empty, "Register failed.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log the error
                ModelState.AddModelError(string.Empty, "An error occurred during register.");
            }

            // If login fails, return the login view with validation errors
            return View();
        }
    }
}
