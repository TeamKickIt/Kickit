namespace Kickit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRecipientFromId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecepientForms", "RecipientFormId", c => c.Byte(nullable: false));
            AddColumn("dbo.RecepientForms", "Recepient_Id", c => c.Int());
            CreateIndex("dbo.RecepientForms", "Recepient_Id");
            AddForeignKey("dbo.RecepientForms", "Recepient_Id", "dbo.RecepientForms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecepientForms", "Recepient_Id", "dbo.RecepientForms");
            DropIndex("dbo.RecepientForms", new[] { "Recepient_Id" });
            DropColumn("dbo.RecepientForms", "Recepient_Id");
            DropColumn("dbo.RecepientForms", "RecipientFormId");
        }
    }
}
