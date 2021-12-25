using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api.Domain.Models;

namespace api.Resources
{
    public class CategoryResource : BaseModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public IEnumerable<ProductResource> Products { get; set; }
    }
}