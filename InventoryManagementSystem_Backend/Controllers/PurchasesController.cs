using InventoryManagementSystem_Backend.Data;
using InventoryManagementSystem_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasesController : ControllerBase
    {
        private readonly InventoryContext _context;

        public PurchasesController(InventoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPurchases()
        {
            var purchases = await _context.Purchases.ToListAsync();
            return Ok(purchases);
        }

        [HttpPost]
        public async Task<IActionResult> RecordPurchase(Purchase purchase)
        {
            var product = await _context.Products.FindAsync(purchase.ProductID);
            if (product == null)
                return NotFound("Product not found.");

            // Update product quantity
            product.Quantity += purchase.QuantityPurchased;
            _context.Entry(product).State = EntityState.Modified;

            // Record the purchase
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllPurchases), new { id = purchase.PurchaseID }, purchase);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchase(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
                return NotFound();

            return Ok(purchase);
        }
    }
}