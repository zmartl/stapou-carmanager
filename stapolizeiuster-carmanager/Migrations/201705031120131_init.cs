namespace stapolizeiuster_carmanager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Radio = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Plannings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Car_Id = c.Int(),
                        State_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.Car_Id)
                .ForeignKey("dbo.States", t => t.State_Id)
                .Index(t => t.Car_Id)
                .Index(t => t.State_Id);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("dbo.Plannings", "State_Id", "dbo.States");
            DropForeignKey("dbo.Plannings", "Car_Id", "dbo.Cars");
            DropIndex("dbo.Statistics", new[] { "Car_Id" });
            DropIndex("dbo.Plannings", new[] { "State_Id" });
            DropIndex("dbo.Plannings", new[] { "Car_Id" });
            DropTable("dbo.Statistics");
            DropTable("dbo.States");
            DropTable("dbo.Plannings");
            DropTable("dbo.Cars");
        }
    }
}
