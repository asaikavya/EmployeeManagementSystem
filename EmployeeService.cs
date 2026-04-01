using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementSystem
{
    public class EmployeeService
    {

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await DatabaseHelper.GetAllEmployeesAsync();
        }

      public async Task AddEmployeeAsync(Employee emp)
        {
            await DatabaseHelper.AddEmployeeAsync(emp);
        }

        public async  Task<List<Employee>> SearchByDepartmentAsync(string department){

           return await DatabaseHelper.SearchByDepartmentAsync(department);

       }


        //public async Task<List<Employee>> TopEarnersAsync(int count)
        //{
        //    return await DatabaseHelper.TopEarnersAsync(count);
        //}

    }
}
