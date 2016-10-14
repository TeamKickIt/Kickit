namespace Kickit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRecipientFormId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invitors", "RecipientFormId", c => c.Byte(nullable: false));
            AddColumn("dbo.Invitors", "RecipientForm_Id", c => c.Int());
            CreateIndex("dbo.Invitors", "RecipientForm_Id");
            AddForeignKey("dbo.Invitors", "RecipientForm_Id", "dbo.RecepientForms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invitors", "RecipientForm_Id", "dbo.RecepientForms");
            DropIndex("dbo.Invitors", new[] { "RecipientForm_Id" });
            DropColumn("dbo.Invitors", "RecipientForm_Id");
            DropColumn("dbo.Invitors", "RecipientFormId");
        }
    }
}
