namespace sumarauto.database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsFeatured : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AutoParts", "IsFeatured", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "IsFeatured", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "IsFeatured");
            DropColumn("dbo.AutoParts", "IsFeatured");
        }
    }
}
