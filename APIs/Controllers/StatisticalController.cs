using BusinessObject.DTOs;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private IStatisticalService repository = new StatisticalService();

        [HttpGet]
        public IActionResult Statistical([FromQuery] DateTime startDate, DateTime endDate)
        {
            if (ModelState.IsValid)
            {
                var statistical = repository.GetStatisticalByDate(startDate, endDate);
                if (statistical != null)
                {
                    return Ok(statistical);
                }
                else
                {
                    return BadRequest();
                }
            }
            return NoContent();
        }
    }
}
