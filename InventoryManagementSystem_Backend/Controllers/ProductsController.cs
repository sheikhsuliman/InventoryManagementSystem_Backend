using InventoryManagementSystem_Backend.Data;
using InventoryManagementSystem_Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem_Backend.Controllers
{
 
        [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
        public class ProductsController : ControllerBase
        {
            private readonly InventoryContext _context;

            public ProductsController(InventoryContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<IActionResult> GetAllProducts()
            {
                var products = await _context.Products.ToListAsync();
                return Ok(products);
            }

            [HttpPost]
            public async Task<IActionResult> AddProduct(Product product)
            {
            await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Products ON");

            _context.Products.Add(product);
                await _context.SaveChangesAsync();
            await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Products OFF");
            return CreatedAtAction(nameof(GetAllProducts), new { id = product.ProductID }, product);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateProduct(int id, Product product)
            {
                if (id != product.ProductID)
                    return BadRequest();

                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteProduct(int id)
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                    return NotFound();

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
    }
