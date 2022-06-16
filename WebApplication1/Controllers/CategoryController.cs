using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Data.DAL;
using WebApplication1.Data.Entities;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Category> categories = _context.Categories.Where(p=>p.IsDeleted==false).ToList();
            return StatusCode(200,categories);
        }


        [HttpGet("isdelete")]
        public IActionResult GetAllDeleted()
        {
            List<Category> categories = _context.Categories.Where(p => p.IsDeleted).ToList();
            return StatusCode(200, categories);
        }


        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Category Category = _context.Categories.Where(p => p.IsDeleted == false).FirstOrDefault(p=>p.Id==id);
            if (Category == null)
            {
                return NotFound();
            }
            return StatusCode(200, Category);
        }


        [HttpPost("")]
        public IActionResult Create(Category category)
        {
            bool existName=_context.Categories.Any(c=>c.Name==category.Name);


            if (existName)
            {
                return BadRequest();
            }


            Category newc=new Category();
            newc.Name=category.Name;
            newc.Desc=category.Desc;
            _context.Add(newc);
            _context.SaveChanges();
            return StatusCode(200, newc);
        }


        [HttpPut("{id}")]
        public IActionResult Update(Category category,int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            Category dbCategory = _context.Categories.Where(c=>c.IsDeleted==false).FirstOrDefault(c=>c.Id==id);
            if (dbCategory == null)
            {
                return NotFound();
            }
            Category existName = _context.Categories.Where(c => c.IsDeleted == false)
                .FirstOrDefault(c=>c.Name==category.Name);


            if (existName!=null)
            {
                if (dbCategory!=existName)
                {
                    return BadRequest();
                }
            }

            dbCategory.Name=category.Name;
            dbCategory.Desc=category.Desc;
            _context.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Category Category = _context.Categories.Where(p => p.IsDeleted == false).FirstOrDefault(p => p.Id == id);
            if (Category == null)
            {
                return NotFound();
            }

            Category.IsDeleted = true;
            _context.SaveChanges();
            return StatusCode(200, Category);
        }
    }
}
