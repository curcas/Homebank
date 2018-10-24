using FluentMigrator;
using JetBrains.Annotations;

namespace Homebank.Api.Database.Migrations
{
    /// <summary>
    /// Creates the Users table.
    /// </summary>
    [Migration(1, "Create the Users table.")]
    public class Migration001 : Migration
    {
        /// <summary>
        /// Changes to downgrade the database.
        /// </summary>
        public override void Down()
        {
            Delete.Table("Users");
        }

        /// <summary>
        /// Changes to update the database.
        /// </summary>
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString(255).NotNullable();
        }
    }
}