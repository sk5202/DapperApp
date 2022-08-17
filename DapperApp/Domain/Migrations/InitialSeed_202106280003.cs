using FluentMigrator;

namespace DapperApp.Domain.Migrations
{
    [Migration(202106280003)]
	public class InitialSeed_202106280003 : Migration
	{
        public override void Down()
        {
            Delete.Table("User");
          
        }

        public override void Up()
        {
            Create.Table("User")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                .WithColumn("Name").AsString().NotNullable();
        }
    }
}
