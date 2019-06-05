namespace ClientManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTime2Type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Client", "BirthDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Client", "BirthDate", c => c.DateTime(nullable: false));
        }
    }
}
