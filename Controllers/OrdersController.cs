using System.Data.SqlTypes;
using api_orders.Data;
using api_orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_orders.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public OrdersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(Order order)
        {
            var existingOrder = await _appDbContext.HUB.FirstOrDefaultAsync(o => o.Number == order.Number);

            if (existingOrder != null)
            {
                return BadRequest($"Já existe uma comanda com o número {order.Number}.");
            }

            _appDbContext.HUB.Add(order);
            await _appDbContext.SaveChangesAsync();

             return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, order);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _appDbContext.HUB.ToListAsync();

            return Ok(orders);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _appDbContext.HUB.FindAsync(id);

            if (order == null)
            {
                return NotFound("Comanda não encontrada!");
            }

            _appDbContext.HUB.Remove(order);

            await _appDbContext.SaveChangesAsync();

            return Ok("Comanda deletada com sucesso!");
        }
    }
}