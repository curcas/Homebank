using Homebank.Api.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Homebank.Api.Database
{
    /// <summary>
    /// The DBbContext for Homebank.
    /// </summary>
    public class HomebankContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomebankContext" /> class.
        /// </summary>
        /// <param name="options">The options of the db context.</param>
        /// <returns>The instance.</returns>
        public HomebankContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the user of Homebank.
        /// </summary>
        /// <value>The users.</value>
        public DbSet<User> Users { get; set; }
    }
}