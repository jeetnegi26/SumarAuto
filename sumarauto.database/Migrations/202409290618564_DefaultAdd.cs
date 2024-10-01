namespace sumarauto.database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DefaultAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AutoPartImages", "Default", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AutoPartImages", "Default");
        }
    }
}
