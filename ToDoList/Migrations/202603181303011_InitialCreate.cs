namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.TaskItemModels", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.TaskItemModels", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.TaskItemModels", "UserId");
            AddForeignKey("dbo.TaskItemModels", "UserId", "dbo.UserModels", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskItemModels", "UserId", "dbo.UserModels");
            DropIndex("dbo.TaskItemModels", new[] { "UserId" });
            AlterColumn("dbo.TaskItemModels", "UserId", c => c.String());
            AlterColumn("dbo.TaskItemModels", "Title", c => c.String());
            DropTable("dbo.UserModels");
        }
    }
}
