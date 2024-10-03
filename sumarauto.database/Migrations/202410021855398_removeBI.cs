namespace sumarauto.database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeBI : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Banners", "Image", c => c.String(nullable: false));
            AddColumn("dbo.Banners", "Type", c => c.String(nullable: false));
            AddColumn("dbo.Banners", "Heading", c => c.String());
            AddColumn("dbo.Banners", "Subheading", c => c.String());
            AddColumn("dbo.Banners", "url", c => c.String());
            AddColumn("dbo.Banners", "ButtonText", c => c.String());
            DropColumn("dbo.Banners", "TypeId");
            DropColumn("dbo.Banners", "BannerHeading");
            DropColumn("dbo.Banners", "BannerSubHeading");
            DropColumn("dbo.Banners", "BannerButtonText");
            DropColumn("dbo.Banners", "BannerButtonUrl");
            DropTable("dbo.BannerImages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BannerImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BannerId = c.Int(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Banners", "BannerButtonUrl", c => c.String());
            AddColumn("dbo.Banners", "BannerButtonText", c => c.String());
            AddColumn("dbo.Banners", "BannerSubHeading", c => c.String());
            AddColumn("dbo.Banners", "BannerHeading", c => c.String());
            AddColumn("dbo.Banners", "TypeId", c => c.String());
            DropColumn("dbo.Banners", "ButtonText");
            DropColumn("dbo.Banners", "url");
            DropColumn("dbo.Banners", "Subheading");
            DropColumn("dbo.Banners", "Heading");
            DropColumn("dbo.Banners", "Type");
            DropColumn("dbo.Banners", "Image");
        }
    }
}
