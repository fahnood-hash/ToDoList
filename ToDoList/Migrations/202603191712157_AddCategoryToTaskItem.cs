namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryToTaskItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskItemModels", "Category", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskItemModels", "Category");
        }
    }
}
