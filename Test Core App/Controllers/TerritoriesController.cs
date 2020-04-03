// using GrapeCity.DataService.DTO;
using GrapeCity.DataService.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GrapeCity.DataService.Controllers
{
    [ApiVersion("1.0")]
    public class TerritoriesController : ODataController
    {
        private readonly NorthwindContext _context;

        public TerritoriesController(NorthwindContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Territory> Get()
        {
            return _context.Territories;
        }

        [EnableQuery]
        public SingleResult<Territory> Get([FromODataUri]string key)
        {
            return SingleResult.Create(_context.Territories.Where(t => t.TerritoryId == key));
        }

        //[Microsoft.AspNetCore.Mvc.Route("odata/Territories({key})/Employees")]
        //[EnableQuery]
        //public IQueryable<EmployeeDto> GetEmployees([FromODataUri] string key)
        //{
        //    // NOTE: use EmployeeDto instead of Employee to avoid circular navigation serialization
        //    return _context.EmployeeTerritories
        //        .Where(et => et.TerritoryId == key)
        //        .Join(_context.Employees, et => et.EmployeeId, e => e.EmployeeId, (et, e) => new { Employee = e, EmployeeTerritory = et })
        //        .Select(t => t.Employee).Select(DtoConverter.AsEmployeeDto);
        //}

        [EnableQuery]
        public IQueryable<EmployeeTerritory> GetEmployeeTerritories([FromODataUri] string key)
        {
            return _context.Territories.Where(t => t.TerritoryId == key).SelectMany(t => t.EmployeeTerritories);
        }

        [EnableQuery]
        public SingleResult<Region> GetRegion([FromODataUri] string key)
        {
            return SingleResult.Create(_context.Territories.Where(t => t.TerritoryId == key).Select(t => t.Region));
        }
    }
}
