namespace sumarauto.database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AutoPartSId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AutoParts", "AutoPartSId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AutoParts", "AutoPartSId");
        }
    }
}
