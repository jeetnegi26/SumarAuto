namespace sumarauto.database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AutoParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MakeId = c.Int(nullable: false),
                        MModelId = c.Int(nullable: false),
                        YearId = c.Int(nullable: false),
                        EngineId = c.Int(nullable: false),
                        LiterId = c.Int(nullable: false),
                        ChassisId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        RewriteUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AutoPartImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Title = c.String(),
                        AutoPartId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BannerImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BannerId = c.Int(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Banners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeId = c.String(),
                        BannerHeading = c.String(),
                        BannerSubHeading = c.String(),
                        BannerButtonText = c.String(),
                        BannerButtonUrl = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        RewriteUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        RewriteUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Chassis",
                c => new
                    {
                        ChassisId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        RewriteUrl = c.String(),
                    })
                .PrimaryKey(t => t.ChassisId);
            
            CreateTable(
                "dbo.Engines",
                c => new
                    {
                        EngineId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        RewriteUrl = c.String(),
                    })
                .PrimaryKey(t => t.EngineId);
            
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        RewriteUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GalleryImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GalleryId = c.Int(nullable: false),
                        Image = c.String(),
                        DefaultImage = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Galleries", t => t.GalleryId, cascadeDelete: true)
                .Index(t => t.GalleryId);
            
            CreateTable(
                "dbo.Liters",
                c => new
                    {
                        LiterId = c.Int(nullable: false, identity: true),
                        Ttile = c.String(),
                        Description = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        RewriteUrl = c.String(),
                    })
                .PrimaryKey(t => t.LiterId);
            
            CreateTable(
                "dbo.Makes",
                c => new
                    {
                        MakeId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        RewriteUrl = c.String(),
                    })
                .PrimaryKey(t => t.MakeId);
            
            CreateTable(
                "dbo.MModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        RewriteUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleAssigns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        SchoolId = c.Int(),
                        IsFaculity = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        RewriteUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        RewriteUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        HashPassword = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        RewriteUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Years",
                c => new
                    {
                        YearId = c.Int(nullable: false, identity: true),
                        Title = c.Int(nullable: false),
                        Description = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        RewriteUrl = c.String(),
                    })
                .PrimaryKey(t => t.YearId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GalleryImages", "GalleryId", "dbo.Galleries");
            DropIndex("dbo.GalleryImages", new[] { "GalleryId" });
            DropTable("dbo.Years");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.RoleAssigns");
            DropTable("dbo.MModels");
            DropTable("dbo.Makes");
            DropTable("dbo.Liters");
            DropTable("dbo.GalleryImages");
            DropTable("dbo.Galleries");
            DropTable("dbo.Engines");
            DropTable("dbo.Chassis");
            DropTable("dbo.Categories");
            DropTable("dbo.Banners");
            DropTable("dbo.BannerImages");
            DropTable("dbo.AutoPartImages");
            DropTable("dbo.AutoParts");
        }
    }
}
