using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EmployeeManagement.App_Start;
using EmployeeManagement.BLL.Interfaces;
using EmployeeManagement.BOL;

namespace EmployeeManagement.WebApi.Controllers
{
    [CustomExceptionFilter]
    public class EmployeesController : ApiController
    {
        private readonly IEmployeeLogic _employeeLogic;

        public EmployeesController()
        {
        }

        public EmployeesController(IEmployeeLogic employeeLogic)
        {
            _employeeLogic = employeeLogic;
        }

        [HttpGet]
        [Route("api/Employees")]
        public async Task<HttpResponseMessage> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _employeeLogic.GetEmployeesAsync();
                return Request.CreateResponse(HttpStatusCode.OK, employees);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Employees/{employeeId}")]
        public async Task<HttpResponseMessage> GetEmployeeByIdAsync(string employeeId)
        {
            try
            {
                var entity = await _employeeLogic.GetEmployeeByIdAsync(employeeId);

                if (entity == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"Employee with Id={employeeId} not found.");
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route("api/Employees/")]
        public async Task<HttpResponseMessage> PostEmployeeAsync([FromBody] EmployeeBOL employeeBol)
        {
            try
            {
                await _employeeLogic.AddEmployeeAsync(employeeBol);
                var message = Request.CreateResponse(HttpStatusCode.Created, employeeBol);
                message.Headers.Location = new Uri($"{Request.RequestUri}/{employeeBol.EmployeeId}");
                return message;
            }

            
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        [Route("api/Employees/{employeeId}")]
        public async Task<HttpResponseMessage> PutEmployeeAsync(string employeeId, [FromBody] EmployeeBOL employeeBol)
        {
            try
            {
                var entity = await _employeeLogic.UpdateEmployeeAsync(employeeId, employeeBol);
                return entity == null
                    ? Request.CreateResponse(HttpStatusCode.NotFound, $"Employee with Id={employeeId} not found.")
                    : Request.CreateResponse(HttpStatusCode.OK, entity);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/Employees/{employeeId}")]
        public async Task<HttpResponseMessage> DeleteEmployeeByIdAsync(string employeeId)
        {
            try
            {
                var entity = await _employeeLogic.DeleteEmployeeByIdAsync(employeeId);

                return entity == null
                    ? Request.CreateResponse(HttpStatusCode.NotFound, $"Employee with Id={employeeId} not found.")
                    : Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}