namespace Kickit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateTimeToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RecepientForms", "DateTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RecepientForms", "DateTime", c => c.Boolean(nullable: false));
        }
    }
}
