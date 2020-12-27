using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Entities;

namespace EmployeeManagement.DAL
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly EmployeeContext _context;

        public EmployeeRepo(EmployeeContext context)
        {
            _context = context;
        }

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

        public async Task<bool> AddAsync(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return true;
            }

            catch (DbEntityValidationException ex)
            {
                var errorMessages = string.Join("; ", ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage));

                throw new DbEntityValidationException(errorMessages);
            }

            catch (Exception ex)
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

                entity.FirstName = employee.FirstName;
                entity.LastName = employee.LastName;
                entity.Salary = employee.Salary;
                entity.Department = employee.Department;
                await _context.SaveChangesAsync();

                return entity;
            }

            catch (DbEntityValidationException ex)
            {
                var errorMessages = string.Join("; ", ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage));

                throw new DbEntityValidationException(errorMessages);
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
                if (entity == null) return null;

                _context.Employees.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}