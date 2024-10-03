namespace sumarauto.database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DOINT : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AutoParts", "DisplayOrder", c => c.Int(nullable: false));
            AlterColumn("dbo.AutoPartMakes", "DisplayOrder", c => c.Int(nullable: false));
            AlterColumn("dbo.Banners", "DisplayOrder", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "DisplayOrder", c => c.Int(nullable: false));
            AlterColumn("dbo.Chassis", "DisplayOrder", c => c.Int(nullable: false));
            AlterColumn("dbo.Engines", "DisplayOrder", c => c.Int(nullable: false));
            AlterColumn("dbo.Galleries", "DisplayOrder", c => c.Int(nullable: false));
            AlterColumn("dbo.Liters", "DisplayOrder", c => c.Int(nullable: false));
            AlterColumn("dbo.Makes", "DisplayOrder", c => c.Int(nullable: false));
            AlterColumn("dbo.MModels", "DisplayOrder", c => c.Int(nullable: false));
            AlterColumn("dbo.RoleAssigns", "DisplayOrder", c => c.Int(nullable: false));
            AlterColumn("dbo.Roles", "DisplayOrder", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "DisplayOrder", c => c.Int(nullable: false));
            AlterColumn("dbo.Years", "DisplayOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Years", "DisplayOrder", c => c.String());
            AlterColumn("dbo.Users", "DisplayOrder", c => c.String());
            AlterColumn("dbo.Roles", "DisplayOrder", c => c.String());
            AlterColumn("dbo.RoleAssigns", "DisplayOrder", c => c.String());
            AlterColumn("dbo.MModels", "DisplayOrder", c => c.String());
            AlterColumn("dbo.Makes", "DisplayOrder", c => c.String());
            AlterColumn("dbo.Liters", "DisplayOrder", c => c.String());
            AlterColumn("dbo.Galleries", "DisplayOrder", c => c.String());
            AlterColumn("dbo.Engines", "DisplayOrder", c => c.String());
            AlterColumn("dbo.Chassis", "DisplayOrder", c => c.String());
            AlterColumn("dbo.Categories", "DisplayOrder", c => c.String());
            AlterColumn("dbo.Banners", "DisplayOrder", c => c.String());
            AlterColumn("dbo.AutoPartMakes", "DisplayOrder", c => c.String());
            AlterColumn("dbo.AutoParts", "DisplayOrder", c => c.String());
        }
    }
}
