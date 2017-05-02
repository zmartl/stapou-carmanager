namespace stapolizeiuster_carmanager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatisticClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Creator = c.String(),
                        Car_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.Car_Id)
                .Index(t => t.Car_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Statistics", "Car_Id", "dbo.Cars");
            DropIndex("dbo.Statistics", new[] { "Car_Id" });
            DropTable("dbo.Statistics");
        }
    }
}
