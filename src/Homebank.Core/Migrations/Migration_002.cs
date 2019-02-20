using System.Data;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homebank.Core.Migrations
{
	[Migration(2)]
	public class Migration_002 : Migration
	{
		public override void Up()
		{
			Create.Table("Accounts")
				.WithColumn("Id").AsInt32().NotNullable().PrimaryKey("IX_Accounts_Id").Identity()
				.WithColumn("Name").AsString(255).NotNullable()
				.WithColumn("User_Id").AsInt32().NotNullable().ForeignKey("FK_User_Id", "Users", "Id").OnDelete(Rule.Cascade);
		}

		public override void Down()
		{
			Delete.Table("Accounts");
		}
	}
}
