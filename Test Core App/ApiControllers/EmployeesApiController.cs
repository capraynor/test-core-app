using GrapeCity.DataService.DTO;
using GrapeCity.DataService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GrapeCity.DataService.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("northwind/api/Employees")]
    [Route("northwind/api/v{version:apiVersion}/Employees")]
    [ApiController]
    public class EmployeesApiController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public EmployeesApiController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<EmployeeDto> GetEmployees()
        {
            return _context.Employees.Select(DtoConverter.AsEmployeeDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpGet("{id}/Superior")]
        public async Task<ActionResult<EmployeeDto>> GetSuperior(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return DtoConverter.ConvertToEmployeeDto(employee.Superior);
        }

        [HttpGet("{id}/Subordinates")]
        public IQueryable<EmployeeDto> GetSubordinates(int id)
        {
            return _context.Employees.Where(e => e.ReportsTo == id).Select(DtoConverter.AsEmployeeDto);
        }

        [HttpGet("{id}/Orders")]
        public IQueryable<OrderDto> GetOrders(int id)
        {
            return _context.Employees.Where(e => e.EmployeeId == id).SelectMany(e => e.Orders).Select(DtoConverter.AsOrderDto);
        }

        [HttpGet("{id}/Territories")]
        public IQueryable<TerritoryDto> GetTerritories(int id)
        {
            return _context.EmployeeTerritories.Where(e => e.EmployeeId == id).Select(e => e.Territory).Select(DtoConverter.AsTerritoryDto);
        }
    }
}
