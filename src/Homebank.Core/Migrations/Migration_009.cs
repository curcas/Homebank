using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homebank.Core.Migrations
{
	[Migration(9)]
	public class Migration_009 : Migration
	{
		public override void Up()
		{
            Create.Column("ControlDate").OnTable("Accounts").AsDate().Nullable();
		}

		public override void Down()
		{
			Delete.Column("ControlDate").FromTable("Accounts");
		}
	}
}
