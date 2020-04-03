using GrapeCity.DataService.DTO;
using GrapeCity.DataService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapeCity.DataService.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("northwind/api/Territories")]
    [Route("northwind/api/v{version:apiVersion}/Territories")]
    [ApiController]
    public class TerritoriesApiController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public TerritoriesApiController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<TerritoryDto> GetTerritories()
        {
            return _context.Territories.Select(DtoConverter.AsTerritoryDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TerritoryDto>> GetTerritory(string id)
        {
            var territory = await _context.Territories.FindAsync(id);

            if (territory == null)
            {
                return NotFound();
            }

            return DtoConverter.ConvertToTerritoryDto(territory);
        }

        [HttpGet("{id}/Employees")]
        public IQueryable<EmployeeDto> GetEmployees(string id)
        {
            return _context.EmployeeTerritories
                .Where(et => et.TerritoryId == id)
                .Join(_context.Employees, et => et.EmployeeId, e => e.EmployeeId, (et, e) => new { Employee = e, EmployeeTerritory = et })
                .Select(t => t.Employee)
                .Select(DtoConverter.AsEmployeeDto);
        }

        [HttpGet("{id}/Region")]
        public async Task<ActionResult<RegionDto>> GetRegion(string id)
        {
            Territory territory = await _context.Territories.FindAsync(id);
            if (territory == null)
            {
                return NotFound();
            }
            return DtoConverter.ConvertToRegionDto(territory.Region);
        }

    }
}
