using GrapeCity.DataService.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GrapeCity.DataService.Controllers
{
    [ApiVersion("1.0")]
    public class RegionsController : ODataController
    {
        private readonly NorthwindContext _context;

        public RegionsController(NorthwindContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Region> Get()
        {
            return _context.Regions;
        }

        [EnableQuery]
        public SingleResult<Region> Get([FromODataUri]int key)
        {
            return SingleResult.Create(_context.Regions.Where(r => r.RegionId == key));
        }

        [EnableQuery]
        public IQueryable<Territory> GetTerritories([FromODataUri]int key)
        {
            return _context.Regions.Where(r => r.RegionId == key).SelectMany(r => r.Territories);
        }
    }
}