using Fiorella_Webim.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Data.DAL;
using WebApplication1.Data.Entities;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [HttpGet]
        public IActionResult Get()
        {
            List<Product> products = _context.Products.Include(p=>p.Category).Where(p => p.IsDeleted == false).ToList();
            return StatusCode(200, products);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product products = _context.Products.Include(p => p.Category).Where(p => p.IsDeleted == false).FirstOrDefault(p=>p.Id==id);

            if (products==null)
            {
                return NotFound();
            }
            products.Image = $"https://localhost:44314/img/{products.Image}";
            return StatusCode(200, products);
        }


        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm]Product product)
        {
            

            Product newc = new Product();

            if (!newc.Photo.isImage())
            {
                return BadRequest("Sekil secin");
            }
            if (newc.Photo.CheckSize(20000))
            {
                return BadRequest("Olcu problemi");
            }
            newc.Name = product.Name;
            newc.Desc = product.Desc;
            newc.Price=product.Price;
            newc.CategoryId = product.CategoryId;
            newc.Image =await newc.Photo.SaveImage(_env,"img");






            _context.Add(newc);
            _context.SaveChanges();
            return StatusCode(200, newc);
        }





    }
}
