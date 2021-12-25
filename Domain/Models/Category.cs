using System.Collections.Generic;

namespace api.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Product> Products = new List<Product>();
    }
}