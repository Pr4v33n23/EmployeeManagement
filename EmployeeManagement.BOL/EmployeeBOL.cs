using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.BOL
{
    public class EmployeeBOL
    {
        [Required(ErrorMessage = "EmployeeID is required.")]
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        public double Salary { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public string Department { get; set; }
    }
}