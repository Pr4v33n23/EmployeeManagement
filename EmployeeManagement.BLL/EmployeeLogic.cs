using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.BLL.Interfaces;
using EmployeeManagement.BOL;
using EmployeeManagement.DAL.Interfaces;

namespace EmployeeManagement.BLL
{
    public class EmployeeLogic : IEmployeeLogic
    {
        private readonly IEmployeeModel _employeeModel;
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeeLogic()
        {
        }

        public EmployeeLogic(IEmployeeRepo employeeRepo, IEmployeeModel employeeModel)
        {
            _employeeRepo = employeeRepo;
            _employeeModel = employeeModel;
        }

        public async Task<IEnumerable<EmployeeBOL>> GetEmployeesAsync()
        {
            try
            {
                var employees = await _employeeRepo.ReadAllAsync();
                return _employeeModel.GetMapEmployees(employees);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Get/id
        public async Task<EmployeeBOL> GetEmployeeByIdAsync(string employeeId)
        {
            try
            {
                var employee = await _employeeRepo.ReadOneAsync(employeeId);

                if (employee == null)
                    return null;
                return _employeeModel.GetMapEmployee(employee);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AddEmployeeAsync(EmployeeBOL employeeBOL)
        {
            try
            {
                var employee = _employeeModel.GetMapEmployeeBOL(employeeBOL);
                await _employeeRepo.AddAsync(employee);
                return true;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeBOL> UpdateEmployeeAsync(string employeeId, EmployeeBOL employeeBOL)
        {
            try
            {
                var employeeEntity = _employeeModel.GetMapEmployeeBOL(employeeBOL);

                var employee =
                    await _employeeRepo.UpdateAsync(employeeId, employeeEntity);

                if (employee == null)
                    return null;
                return _employeeModel.GetMapEmployee(employee);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeBOL> DeleteEmployeeByIdAsync(string employeeId)
        {
            try
            {
                var employee = await _employeeRepo.DeleteAsync(employeeId);

                if (employee == null)
                    return null;
                return _employeeModel.GetMapEmployee(employee);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}