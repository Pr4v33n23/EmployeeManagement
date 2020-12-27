using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeManagement.DAL.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Employees",
                    c => new
                    {
                        Id = c.Int(false, true),
                        EmployeeId = c.String(false),
                        FirstName = c.String(false),
                        LastName = c.String(false),
                        Salary = c.Double(false),
                        Department = c.String(false)
                    })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}