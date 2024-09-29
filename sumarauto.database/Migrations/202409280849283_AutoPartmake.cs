namespace sumarauto.database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AutoPartmake : DbMigration
    {
        public override void Up()
        {
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
                        DisplayOrder = c.String(),
                        RewriteUrl = c.String(),
                        AutoPart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AutoParts", t => t.AutoPart_Id)
                .Index(t => t.AutoPart_Id);
            
            AddColumn("dbo.AutoParts", "ExtraField", c => c.String());
            AddColumn("dbo.AutoParts", "Category_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.AutoPartImages", "AutoPartId");
            AddForeignKey("dbo.AutoPartImages", "AutoPartId", "dbo.AutoParts", "Id", cascadeDelete: true);
            DropColumn("dbo.AutoParts", "MakeId");
            DropColumn("dbo.AutoParts", "MModelId");
            DropColumn("dbo.AutoParts", "YearId");
            DropColumn("dbo.AutoParts", "EngineId");
            DropColumn("dbo.AutoParts", "LiterId");
            DropColumn("dbo.AutoParts", "ChassisId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AutoParts", "ChassisId", c => c.Int(nullable: false));
            AddColumn("dbo.AutoParts", "LiterId", c => c.Int(nullable: false));
            AddColumn("dbo.AutoParts", "EngineId", c => c.Int(nullable: false));
            AddColumn("dbo.AutoParts", "YearId", c => c.Int(nullable: false));
            AddColumn("dbo.AutoParts", "MModelId", c => c.Int(nullable: false));
            AddColumn("dbo.AutoParts", "MakeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.AutoPartMakes", "AutoPart_Id", "dbo.AutoParts");
            DropForeignKey("dbo.AutoPartImages", "AutoPartId", "dbo.AutoParts");
            DropIndex("dbo.AutoPartMakes", new[] { "AutoPart_Id" });
            DropIndex("dbo.AutoPartImages", new[] { "AutoPartId" });
            DropColumn("dbo.AutoParts", "Category_Id");
            DropColumn("dbo.AutoParts", "ExtraField");
            DropTable("dbo.AutoPartMakes");
        }
    }
}
