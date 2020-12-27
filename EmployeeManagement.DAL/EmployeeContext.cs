using System.Data.Entity;
using EmployeeManagement.Entities;

namespace EmployeeManagement.DAL
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext() : base("SqlConnStr")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
    }
}