using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreshAndDelicious.Core.Data;
using FreshAndDelicious.Core.Models;
using Products = FreshAndDelicious.Core.Models.Products;


namespace FreshAndDeliciousApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FreshAndDeliciousController : ControllerBase
    {
        private readonly FreshAndDeliciousDbContext _context;

        public FreshAndDeliciousController(FreshAndDeliciousDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Products>>> GetProducts()
        {

            try
            {
                var productQueries = _context.Products
                .Include(p => p.Discount)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .OrderByDescending(o => o.Id)
                .Take(25);


                var product = await productQueries.ToListAsync();
                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProductById(int id)
        {

            try
            {
                var product = await _context.Products
                .Include(p => p.Discount)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                    return NotFound();

                return product;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }



    }
}