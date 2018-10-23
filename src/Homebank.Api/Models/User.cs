using System.ComponentModel.DataAnnotations;

namespace Homebank.Api.Models
{
    /// <summary>
    /// The User model.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the id of the user.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}