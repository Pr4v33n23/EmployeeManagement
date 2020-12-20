using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DAL
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly EmployeeContext _context;

        public EmployeeRepo(EmployeeContext context) => _context = context;

        public async Task<IEnumerable<Employee>> ReadAllAsync()
        {
            try
            {
                return await _context.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> ReadOneAsync(string employeeId)
        {
            try
            {
                return await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> DeleteAsync(string employeeId)
        {
            try
            {
                var entity = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
                if(entity == null)
                {
                    return null;
                }
                else
                {
                    _context.Employees.Remove(entity);
                    await _context.SaveChangesAsync();
                    return entity;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            } 
        }

        public async Task AddAsync(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> UpdateAsync(string employeeId, Employee employee)
        {
            try
            {
                var entity = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

                if (entity == null)
                {
                    return null;
                }
                else
                {
                    entity.FirstName = employee.FirstName;
                    entity.LastName = employee.LastName;
                    entity.Salary = employee.Salary;
                    entity.Department = employee.Department;
                    await _context.SaveChangesAsync();

                    return entity;
                }
            }

            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
