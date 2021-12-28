using System.ComponentModel.DataAnnotations;
using api.Domain.Models;

namespace api.Resources
{
    public class ProductResource : BaseModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int QuantityInPackage { get; set; }
        public string UnitOfMeasurement { get; set; }
        public CategoryResource Category { get; set; }
    }
}