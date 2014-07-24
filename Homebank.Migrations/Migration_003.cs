using System.Data;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homebank.Migrations
{
	[Migration(3)]
	public class Migration_003 : Migration
	{
		public override void Up()
		{
			Create.Table("Categories")
				.WithColumn("Id").AsInt32().NotNullable().PrimaryKey("IX_Categories_Id").Identity()
				.WithColumn("Name").AsString(255).NotNullable()
				.WithColumn("User_Id").AsInt32().NotNullable().ForeignKey("FK_Categories_Id", "Users", "Id").OnDelete(Rule.Cascade).Indexed("IX_Categories_User_Id");
		}

		public override void Down()
		{
			Delete.Table("Categories");
		}
	}
}
