using System.Data;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homebank.Core.Migrations
{
	[Migration(5)]
	public class Migration_005 : Migration
	{
		public override void Up()
		{
			Create.Table("Bookings")
				.WithColumn("Id").AsInt32().NotNullable().PrimaryKey("IX_Bookings_Id").Identity()
				.WithColumn("Amount").AsDecimal(10, 2).NotNullable()
				.WithColumn("Account_Id").AsInt32().NotNullable().ForeignKey("FK_Bookings_Account_Id", "Accounts", "Id").OnDelete(Rule.Cascade)
				.WithColumn("Transaction_Id").AsInt32().NotNullable().ForeignKey("FK_Bookings_Transaction_Id", "Transactions", "Id");
		}

		public override void Down()
		{
			Delete.Table("Bookings");
		}
	}
}
