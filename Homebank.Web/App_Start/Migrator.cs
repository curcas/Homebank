using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using Homebank.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Homebank.Web.App_Start
{
	public class Migrator
	{
		private readonly string _connectionString;

		public Migrator(string connectionString)
		{
			_connectionString = connectionString;
		}

		private class MigrationOptions : IMigrationProcessorOptions
		{
			public bool PreviewOnly { get; set; }
			public int Timeout { get; set; }


			public string ProviderSwitches
			{
				get { throw new NotImplementedException(); }
			}
		}

		public void Migrate(Action<IMigrationRunner> runnerAction)
		{
			var options = new MigrationOptions { PreviewOnly = false, Timeout = 0 };
			var factory = new FluentMigrator.Runner.Processors.SqlServer.SqlServer2012ProcessorFactory();
			var assembly = Assembly.GetAssembly(typeof(Migration_001));
			var announcer = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));
			var migrationContext = new RunnerContext(announcer);
			var processor = factory.Create(_connectionString, announcer, options);
			var runner = new MigrationRunner(assembly, migrationContext, processor);

			runnerAction(runner);
		}
	}
}