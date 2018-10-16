using System.ComponentModel.DataAnnotations;

namespace Homebank.Api.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}