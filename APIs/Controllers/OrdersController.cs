using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject.DataAccess;
using DataAccess.IRepository;
using DataAccess.Repository;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderService repository = new OrderService();

        

        // GET: api/Orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            return repository.GetOrders();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = repository.GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }
            try
            {
                repository.UpdateOrder(order);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Order> PostOrder(Order order)
        {
            repository.AddOrder(order);

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = repository.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }

            repository.DeleteOrder(id);

            return NoContent();
        }

       
    }
}
