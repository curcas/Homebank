using System.ComponentModel.DataAnnotations;

namespace Homebank.Api.Database.Entities
{
    /// <summary>
    /// A user of Homebank.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the id of the user.
        /// </summary>
        /// <value>The id of the user.</value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string Name { get; set; }
    }
}