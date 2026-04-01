
using EmployeeManagementSystem;
using Microsoft.IdentityModel.Tokens.Experimental;
using System.ComponentModel.Design;

await DatabaseHelper.TestConnectionAsync();
var employeeService = new EmployeeService();

while (true)
{
    Console.Clear();
    Console.WriteLine("==============================================");
    Console.WriteLine("Employee Management System");
    Console.WriteLine("===============================================");
    Console.WriteLine("1. View All Employees");
    Console.WriteLine("2. Add New Employee");
    Console.WriteLine("3. Search Employee by Department");
    Console.WriteLine("4. Get department Salary report");
    Console.WriteLine("5. Get top Earners");
    Console.WriteLine("6. Exit");
    Console.WriteLine("===============================================");
    Console.WriteLine("Choose an option from 1-6 : ");

    String choice= Console.ReadLine();

    switch (choice)
    {
        case "1": var result=await employeeService.GetAllEmployeesAsync();
            Console.WriteLine($"{"ID",-5} {"Name",-15} {"Department",-25} {"Salary",-35} {"Hired",-45}");

            foreach (var emp in result)
                {
                Console.WriteLine($" {emp.Id,-5} {emp.Name,-15}  {emp.Department,-25}  {emp.Salary,-35}  {emp.Hired,-45}");
            }
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
            break;
       case "2": 
            Console.WriteLine("Enter Employee Name: ");
             String name= Console.ReadLine();
            Console.WriteLine("Enter Employee Department: ");
                String dept= Console.ReadLine();
            Console.WriteLine("Enter Employee Salary: ");
       
                int salary = int.Parse(Console.ReadLine());
           
            if(!int.TryParse(Console.ReadLine(), out  salary))
            {
                Console.WriteLine("Invalid salary input. Please enter a valid integer value.");
                break;
            }
           
            Console.WriteLine("Enter Employee Hire Date (yyyy-MM-dd): ");
            DateTime hiredate=DateTime.Parse(Console.ReadLine());
            Employee empl = new Employee
            {
                Name = name,
                Department = dept,
                Salary = salary,
                Hired = hiredate
            };
            try
            {
                await employeeService.AddEmployeeAsync(empl); break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding employee: {ex.Message}");
                break;
            }

        case "3":
            Console.WriteLine("Enter department to search: ");
            String department= Console.ReadLine();
                var searchResult = await employeeService.SearchByDepartmentAsync(department);
            if(searchResult.Count == 0)
            {
                Console.WriteLine($"No employees found in department: {department}");
            }
         
            foreach(var emp in searchResult)
            {
                Console.WriteLine($" {emp.Id,-5} {emp.Name,-15}  {emp.Department,-25}  {emp.Salary,-35}  {emp.Hired,-45}");
            }
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
            break;
        case "4":
            var allemployees = await employeeService.GetAllEmployeesAsync();
            var groupedData =  allemployees.GroupBy(empl => empl.Department).Select(g => new 
            {
                Department = g.Key,
                Count = g.Count(),
                TotalSalary = g.Sum(empl => empl.Salary),
                AvgSalary=g.Average(empl=>empl.Salary)

            });
            Console.WriteLine($"{"Department",-5} {"Employee Count",-15} {"Total Salary",-25}  {"Average Salary:",-45}");

            foreach (var row in groupedData)
            {
                Console.WriteLine($" {row.Department,-5} | {row.Count,-15} |  {row.TotalSalary,-25} |  {row.AvgSalary,-35}");
            }
            Console.WriteLine("Company Average Salary: " + allemployees.Average(empl=>empl.Salary));
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();

            break;
        case "5": 

            //int count = 5;
            //var topearners= await employeeService.TopEarnersAsync(count);
            //Console.WriteLine($"{"ID",-5} {"Name",-15} {"Department",-25} {"Salary",-35} {"Hired",-45}");
            //foreach (var emp in topearners)
            //{
            //    Console.WriteLine($" {emp.Id,-5} {emp.Name,-15}  {emp.Department,-25}  {emp.Salary,-35}  {emp.Hired,-45}");
            //}
            //Console.WriteLine("Press any key to return to menu...");
            //Console.ReadKey();

            var allemps = await employeeService.GetAllEmployeesAsync();
            var topearners = allemps.OrderByDescending(e => e.Salary).Take(5);
            foreach (var emp in topearners)
            {
                Console.WriteLine($" {emp.Id,-5} {emp.Name,-15}  {emp.Department,-25}  {emp.Salary,-35}  {emp.Hired,-45}");
            }
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();

            break;
        case "6":
            Console.WriteLine("Exiting the application...");
            return;
        default:
            Console.WriteLine("Invalid choice. Please select a valid option (1-6).");
            break;
    }

}



