using GrapeCity.DataService.DTO;
using GrapeCity.DataService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GrapeCity.DataService.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("northwind/api/Regions")]
    [Route("northwind/api/v{version:apiVersion}/Regions")]
    [ApiController]
    public class RegionsApiController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public RegionsApiController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<RegionDto> GetRegions()
        {
            return _context.Regions.Select(DtoConverter.AsRegionDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegionDto>> GetRegion(int id)
        {
            var region = await _context.Regions.FindAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return DtoConverter.ConvertToRegionDto(region);
        }

        [HttpGet("{id}/Territories")]
        public IQueryable<TerritoryDto> GetTerritories(int id)
        {
            return _context.Regions.Where(r => r.RegionId == id).SelectMany(r => r.Territories).Select(DtoConverter.AsTerritoryDto);
        }
    }
}
