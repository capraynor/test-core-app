// using GrapeCity.DataService.DTO;
using GrapeCity.DataService.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GrapeCity.DataService.Controllers
{
    [ApiVersion("1.0")]
    public class EmployeesController : ODataController
    {
        private readonly NorthwindContext _context;

        public EmployeesController(NorthwindContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Employee> Get()
        {
            return _context.Employees;
        }

        [EnableQuery]
        public SingleResult<Employee> Get([FromODataUri]int key)
        {
            return SingleResult.Create(_context.Employees.Where(e => e.EmployeeId == key));
        }

        [EnableQuery]
        public SingleResult<Employee> GetSuperior([FromODataUri] int key)
        {
            return SingleResult.Create(_context.Employees.Where(e => e.EmployeeId == key).Select(e => e.Superior));
        }

        [EnableQuery]
        public IQueryable<Employee> GetSubordinates([FromODataUri] int key)
        {
            return _context.Employees.Where(e => e.ReportsTo == key);
        }

        [EnableQuery]
        public IQueryable<Order> GetOrders([FromODataUri] int key)
        {
            return _context.Employees.Where(e => e.EmployeeId == key).SelectMany(e => e.Orders);
        }

        [EnableQuery]
        public IQueryable<EmployeeTerritory> GetEmployeeTerritories([FromODataUri] int key)
        {
            return _context.Employees.Where(e => e.EmployeeId == key).SelectMany(e => e.EmployeeTerritories);
        }

        //[Microsoft.AspNetCore.Mvc.Route("odata/Employees({key})/Territories")]
        //[EnableQuery]
        //public IQueryable<TerritoryDto> GetTerritoryFromEmployeeTerritories([FromODataUri] int key)
        //{
        //    // NOTE: use TerritoryDto instead of Territory to avoid circular navigation serialization
        //    return _context.EmployeeTerritories.Where(et => et.EmployeeId == key).Select(et => et.Territory).Select(DtoConverter.AsTerritoryDto);
        //}
    }
}
