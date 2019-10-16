namespace ClinicApp.Repo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clinics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArabicName = c.String(nullable: false, maxLength: 50),
                        EnglishName = c.String(nullable: false, maxLength: 50),
                        Title = c.String(),
                        PhoneNumber = c.String(),
                        Status = c.Boolean(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArabicName = c.String(nullable: false, maxLength: 50),
                        EnglishName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clinics", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Clinics", new[] { "DepartmentId" });
            DropTable("dbo.Departments");
            DropTable("dbo.Clinics");
        }
    }
}
