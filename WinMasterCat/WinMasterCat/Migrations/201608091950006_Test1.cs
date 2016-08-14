namespace WinMasterCat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SName = c.String(),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.ProductProps");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductProps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PropertyName = c.String(),
                        PropertyVal = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.TestUsers");
        }
    }
}
