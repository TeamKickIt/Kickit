namespace Kickit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteLineFromInvitorModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invitors", "RecipientForm_Id", "dbo.RecepientForms");
            DropIndex("dbo.Invitors", new[] { "RecipientForm_Id" });
            DropColumn("dbo.Invitors", "RecipientForm_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invitors", "RecipientForm_Id", c => c.Int());
            CreateIndex("dbo.Invitors", "RecipientForm_Id");
            AddForeignKey("dbo.Invitors", "RecipientForm_Id", "dbo.RecepientForms", "Id");
        }
    }
}
