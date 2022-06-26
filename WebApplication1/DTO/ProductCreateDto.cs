using FluentValidation;
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


    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("50 den yuxari ola bilmez");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("price 0-dan boyuk olmalidi");
        }
    }
}
