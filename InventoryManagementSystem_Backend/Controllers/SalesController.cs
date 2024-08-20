using InventoryManagementSystem_Backend.Data;
using InventoryManagementSystem_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly InventoryContext _context;

        public SalesController(InventoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            var sales = await _context.Sales.ToListAsync();
            return Ok(sales);
        }

        [HttpPost]
        public async Task<IActionResult> RecordSale(Sale sale)
        {
            var product = await _context.Products.FindAsync(sale.ProductID);
            if (product == null)
                return NotFound("Product not found.");

            if (product.Quantity < sale.QuantitySold)
                return BadRequest("Insufficient product quantity in inventory.");

            // Update product quantity
            product.Quantity -= sale.QuantitySold;
            _context.Entry(product).State = EntityState.Modified;

            // Record the sale
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllSales), new { id = sale.SaleID }, sale);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
                return NotFound();

            return Ok(sale);
        }
    }
}