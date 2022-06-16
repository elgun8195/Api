using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Desc { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [NotMapped]
        public IFormFile    Photo { get; set; }

    }
}
