namespace NsuWorms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Behaviours",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Points = c.String(),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Behaviours");
        }
    }
}
