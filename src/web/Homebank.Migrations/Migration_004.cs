using System.Data;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homebank.Migrations
{
	[Migration(4)]
	public class Migration_004 : Migration
	{
		public override void Up()
		{
			Create.Table("Transactions")
				.WithColumn("Id").AsInt32().NotNullable().PrimaryKey("IX_Transactions_Id").Identity()
				.WithColumn("Description").AsString(255).NotNullable()
				.WithColumn("Date").AsDateTime().NotNullable().Indexed("IX_Transactions_Date")
				.WithColumn("Category_Id").AsInt32().NotNullable().ForeignKey("FK_Transactions_Category_Id", "Categories", "Id").OnDelete(Rule.Cascade);
		}

		public override void Down()
		{
			Delete.Table("Transactions");
		}
	}
}
