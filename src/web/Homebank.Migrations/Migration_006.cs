using System.Data;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homebank.Migrations
{
	[Migration(6)]
	public class Migration_006 : Migration
	{
		public override void Up()
		{
			Create.Table("Templates")
				.WithColumn("Id").AsInt32().NotNullable().PrimaryKey("IX_Templates_Id").Identity()
				.WithColumn("Name").AsString(255).NotNullable()
				.WithColumn("Description").AsString(255).NotNullable()
				.WithColumn("Amount").AsDecimal(10, 2).NotNullable()
				.WithColumn("User_Id").AsInt32().NotNullable().ForeignKey("FK_Templates_User_Id", "Users", "Id").OnDelete(Rule.Cascade)
				.WithColumn("Account_Id").AsInt32().NotNullable().ForeignKey("FK_Templates_Account_Id", "Accounts", "Id")
				.WithColumn("ReferenceAccount_Id").AsInt32().Nullable().ForeignKey("FK_Templates_ReferenceAccount_Id", "Accounts", "Id")
				.WithColumn("Category_Id").AsInt32().NotNullable().ForeignKey("FK_Templates_Category_Id", "Categories", "Id");
		}

		public override void Down()
		{
			Delete.Table("Templates");
		}
	}
}
