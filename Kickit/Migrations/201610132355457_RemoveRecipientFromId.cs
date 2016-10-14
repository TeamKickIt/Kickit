namespace Kickit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRecipientFromId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RecepientForms", "Recepient_Id", "dbo.RecepientForms");
            DropIndex("dbo.RecepientForms", new[] { "Recepient_Id" });
            DropColumn("dbo.RecepientForms", "RecipientFormId");
            DropColumn("dbo.RecepientForms", "Recepient_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecepientForms", "Recepient_Id", c => c.Int());
            AddColumn("dbo.RecepientForms", "RecipientFormId", c => c.Byte(nullable: false));
            CreateIndex("dbo.RecepientForms", "Recepient_Id");
            AddForeignKey("dbo.RecepientForms", "Recepient_Id", "dbo.RecepientForms", "Id");
        }
    }
}
