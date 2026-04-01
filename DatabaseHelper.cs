using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace EmployeeManagementSystem
{
    public static class DatabaseHelper
    {
        private static string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=tempdb;Trusted_Connection=true;TrustServerCertificate=true;";

        public static async Task TestConnectionAsync()
        {
            {
                try
                {
                    using var connection = new SqlConnection(_connectionString);
                    await connection.OpenAsync();
                    Console.WriteLine("Connected to database successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Connection failed: {ex.Message}");
                }
            }
        }
        public static async Task<List<Employee>> GetAllEmployeesAsync()
        {
            var employees = new List<Employee>();

            using SqlConnection sql = new SqlConnection(_connectionString);
            await sql.OpenAsync();
            using SqlCommand cmd = new SqlCommand("SELECT ID,NAME,DEPARTMENT,SALARY,HIREDATE FROM EMPLOYEES", sql);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Employee employee = new Employee();
                employee.Id = reader.GetInt32(0);
                employee.Name = reader.GetString(1);
                employee.Department = reader.GetString(2);
                employee.Salary = reader.GetInt32(3);
                employee.Hired = reader.GetDateTime(4);

                employees.Add(employee);
            }



            return employees;
        }

        public static async Task AddEmployeeAsync(Employee emp)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            await con.OpenAsync();
            using SqlCommand cmd = new SqlCommand(("INSERT INTO Employees(Name,Department,Salary,HireDate) values(@Name,@Dept,@Salary,@HireDate)"), con);
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Dept", emp.Department);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            cmd.Parameters.AddWithValue("@HireDate", emp.Hired);
            await cmd.ExecuteNonQueryAsync();
        }
        public static async Task<List<Employee>> SearchByDepartmentAsync(string department)
        {
            var employees = new List<Employee>();
            using SqlConnection con = new SqlConnection(_connectionString);
            con.OpenAsync();
            using SqlCommand cmd = new SqlCommand($"SELECT NAME,DEPARTMENT,SALARY,HIREDATE FROM EMPLOYEES WHERE DEPARTMENT=@department", con);
            cmd.Parameters.AddWithValue("@department", department);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                employees.Add(new Employee
                {
                    Name = reader.GetString(0),
                    Department = reader.GetString(1),
                    Salary = reader.GetInt32(2),
                    Hired = reader.GetDateTime(3)
                });
            }

            return employees;

        }
        /*
        public static async Task<List<Employee>> TopEarnersAsync(int count)
        {
            var employees = new List<Employee>();
            using SqlConnection con = new SqlConnection(_connectionString);
            con.OpenAsync();
            using SqlCommand cmd = new SqlCommand($"select top {count} ID,Name,Department,Salary,HireDate from Employees order by Salary desc", con);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
             
                employees.Add(new Employee
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Department = reader.GetString(2),
                    Salary = reader.GetInt32(3),
                    Hired = reader.GetDateTime(4)
                });
               
            }
            return employees;
        }
        */
    }
}