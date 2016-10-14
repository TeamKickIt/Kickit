namespace Kickit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveForeignKey : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Invitors", "RecipientFormId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invitors", "RecipientFormId", c => c.Byte(nullable: false));
        }
    }
}
