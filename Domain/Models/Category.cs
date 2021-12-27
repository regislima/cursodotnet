using System.Collections.Generic;

namespace api.Domain.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}