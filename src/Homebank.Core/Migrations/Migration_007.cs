using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homebank.Core.Migrations
{
	[Migration(7)]
	public class Migration_007 : Migration
	{
		public override void Up()
		{
			Create.Column("Active").OnTable("Accounts").AsBoolean().NotNullable().WithDefaultValue(true);
		}

		public override void Down()
		{
			Delete.Column("Active").FromTable("Accounts");
		}
	}
}
