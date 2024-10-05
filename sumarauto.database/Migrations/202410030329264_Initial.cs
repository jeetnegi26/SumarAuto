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
                        AutoPartSId = c.String(),
                        Title = c.String(),
                        ExtraField = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category_Id = c.Int(nullable: false),
                        IsFeatured = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        DisplayOrder = c.Int(nullable: false),
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
                        Default = c.Boolean(nullable: false),
                        AutoPartId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AutoParts", t => t.AutoPartId, cascadeDelete: true)
                .Index(t => t.AutoPartId);
            
            CreateTable(
                "dbo.AutoPartMakes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Make_Id = c.Int(nullable: false),
                        Model_Title = c.String(),
                        Year_Title = c.String(),
                        Engine_Title = c.String(),
                        Chassis_Title = c.String(),
                        Liter_Title = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        DisplayOrder = c.Int(nullable: false),
                        RewriteUrl = c.String(),
                        AutoPart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AutoParts", t => t.AutoPart_Id)
                .Index(t => t.AutoPart_Id);
            
            CreateTable(
                "dbo.Banners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        Heading = c.String(),
                        Subheading = c.String(),
                        url = c.String(),
                        ButtonText = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        DisplayOrder = c.Int(nullable: false),
                        RewriteUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Date = c.DateTime(nullable: false),
                        Image = c.String(),
                        Description = c.String(),
                        IsFeatured = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        DisplayOrder = c.Int(nullable: false),
                        RewriteUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        IsFeatured = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        DisplayOrder = c.Int(nullable: false),
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
                        DisplayOrder = c.Int(nullable: false),
                        RewriteUrl = c.String(),
                    })
                .PrimaryKey(t => t.ChassisId);
            
            CreateTable(
                "dbo.ContactForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CompanyName = c.String(),
                        Address = c.String(),
                        Country = c.String(),
                        Phone = c.String(),
                        Comment = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedOn = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserHostAdd = c.String(),
                        DisplayOrder = c.Int(nullable: false),
                        RewriteUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        DisplayOrder = c.Int(nullable: false),
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
                        DisplayOrder = c.Int(nullable: false),
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
                "dbo.Keys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Description = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        DisplayOrder = c.Int(nullable: false),
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
                        DisplayOrder = c.Int(nullable: false),
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
                        DisplayOrder = c.Int(nullable: false),
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
                        DisplayOrder = c.Int(nullable: false),
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
                        DisplayOrder = c.Int(nullable: false),
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
                        DisplayOrder = c.Int(nullable: false),
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
                        DisplayOrder = c.Int(nullable: false),
                        RewriteUrl = c.String(),
                    })
                .PrimaryKey(t => t.YearId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GalleryImages", "GalleryId", "dbo.Galleries");
            DropForeignKey("dbo.AutoPartMakes", "AutoPart_Id", "dbo.AutoParts");
            DropForeignKey("dbo.AutoPartImages", "AutoPartId", "dbo.AutoParts");
            DropIndex("dbo.GalleryImages", new[] { "GalleryId" });
            DropIndex("dbo.AutoPartMakes", new[] { "AutoPart_Id" });
            DropIndex("dbo.AutoPartImages", new[] { "AutoPartId" });
            DropTable("dbo.Years");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.RoleAssigns");
            DropTable("dbo.MModels");
            DropTable("dbo.Makes");
            DropTable("dbo.Liters");
            DropTable("dbo.Keys");
            DropTable("dbo.GalleryImages");
            DropTable("dbo.Galleries");
            DropTable("dbo.Engines");
            DropTable("dbo.ContactForms");
            DropTable("dbo.Chassis");
            DropTable("dbo.Categories");
            DropTable("dbo.Blogs");
            DropTable("dbo.Banners");
            DropTable("dbo.AutoPartMakes");
            DropTable("dbo.AutoPartImages");
            DropTable("dbo.AutoParts");
        }
    }
}
