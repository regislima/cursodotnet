using System.ComponentModel.DataAnnotations;
using api.Domain.Models;

namespace api.Resources
{
    public class UserResource : BaseModel
    {
        [Required]
        [MaxLength(50)]
        public string Login { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}