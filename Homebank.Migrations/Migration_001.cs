using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homebank.Migrations
{
	[Migration(1)]
	public class Migration_001 : Migration
	{
		public override void Up()
		{
			Create.Table("Users")
				.WithColumn("Id").AsInt32().NotNullable().PrimaryKey("IX_Users_Id").Identity()
				.WithColumn("Name").AsString(255).NotNullable().Indexed("IX_Users_Name")
				.WithColumn("Password").AsString(128).NotNullable()
				.WithColumn("Salt").AsString(25).NotNullable();
		}

		public override void Down()
		{
			Delete.Table("Users");
		}
	}
}
