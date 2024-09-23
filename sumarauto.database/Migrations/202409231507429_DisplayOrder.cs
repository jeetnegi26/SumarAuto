namespace sumarauto.database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisplayOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AutoParts", "DisplayOrder", c => c.String());
            AddColumn("dbo.Banners", "DisplayOrder", c => c.String());
            AddColumn("dbo.Categories", "DisplayOrder", c => c.String());
            AddColumn("dbo.Chassis", "DisplayOrder", c => c.String());
            AddColumn("dbo.Engines", "DisplayOrder", c => c.String());
            AddColumn("dbo.Galleries", "DisplayOrder", c => c.String());
            AddColumn("dbo.Liters", "DisplayOrder", c => c.String());
            AddColumn("dbo.Makes", "DisplayOrder", c => c.String());
            AddColumn("dbo.MModels", "DisplayOrder", c => c.String());
            AddColumn("dbo.RoleAssigns", "DisplayOrder", c => c.String());
            AddColumn("dbo.Roles", "DisplayOrder", c => c.String());
            AddColumn("dbo.Users", "DisplayOrder", c => c.String());
            AddColumn("dbo.Years", "DisplayOrder", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Years", "DisplayOrder");
            DropColumn("dbo.Users", "DisplayOrder");
            DropColumn("dbo.Roles", "DisplayOrder");
            DropColumn("dbo.RoleAssigns", "DisplayOrder");
            DropColumn("dbo.MModels", "DisplayOrder");
            DropColumn("dbo.Makes", "DisplayOrder");
            DropColumn("dbo.Liters", "DisplayOrder");
            DropColumn("dbo.Galleries", "DisplayOrder");
            DropColumn("dbo.Engines", "DisplayOrder");
            DropColumn("dbo.Chassis", "DisplayOrder");
            DropColumn("dbo.Categories", "DisplayOrder");
            DropColumn("dbo.Banners", "DisplayOrder");
            DropColumn("dbo.AutoParts", "DisplayOrder");
        }
    }
}
