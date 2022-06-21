using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.DTO
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Desc { get; set; }

        public int CategoryId { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
