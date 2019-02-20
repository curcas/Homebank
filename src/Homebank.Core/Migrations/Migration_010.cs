using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homebank.Core.Migrations
{
	[Migration(10)]
	public class Migration_010 : Migration
	{
		public override void Up()
		{
            Create.Index("UIX_Users_Name").OnTable("Users").OnColumn("Name").Ascending().WithOptions().Unique();
            Create.Index("UIX_Categories_Name_User_Id").OnTable("Categories").OnColumn("Name").Ascending().OnColumn("User_Id").Ascending().WithOptions().Unique();
            Create.Index("UIX_Accounts_Name_UserId").OnTable("Accounts").OnColumn("Name").Ascending().OnColumn("User_Id").Ascending().WithOptions().Unique();
            Create.Index("UIX_Templates_Name_Account_Id_User_Id").OnTable("Templates").OnColumn("Name").Ascending().OnColumn("Account_Id").Ascending().OnColumn("User_Id").Ascending().WithOptions().Unique();
        }

		public override void Down()
		{
            Delete.Index("UIX_Users_Name").OnTable("Users").OnColumn("Name");
            Delete.Index("UIX_Categories_Name_User_Id").OnTable("Categories").OnColumns("Name", "User_Id");
            Delete.Index("UIX_Accounts_Name_UserId").OnTable("Accounts").OnColumns("Name", "User_Id");
            Delete.Index("UIX_Templates_Name_Account_Id_User_Id").OnTable("Templates").OnColumns("Name", "Account_Id", "User_Id");
        }
	}
}
