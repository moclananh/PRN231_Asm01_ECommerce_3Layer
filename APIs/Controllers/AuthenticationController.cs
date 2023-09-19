using BusinessObject.DataAccess;
using BusinessObject.DTOs;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticate authenticate = new Authenticate();

        [HttpPost("Login")]
        public IActionResult Login([FromBody]LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = authenticate.Login(request.Email, request.Password);
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest();
                }
            }
            return NoContent();
        }

        [HttpPost("Register")]
        public IActionResult Register(Member member)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    authenticate.Register(member);
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return NoContent();
        }
    }
}
