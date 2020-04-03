using GrapeCity.DataService.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GrapeCity.DataService.Controllers
{
    [ApiVersion("1.0")]
    public class SuppliersController : ODataController
    {
        private readonly NorthwindContext _context;

        public SuppliersController(NorthwindContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Supplier> Get()
        {
            return _context.Suppliers;
        }

        [EnableQuery]
        public SingleResult<Supplier> Get([FromODataUri]int key)
        {
            return SingleResult.Create(_context.Suppliers.Where(s => s.SupplierId == key));
        }

        [EnableQuery]
        public IQueryable<Product> GetProducts([FromODataUri] int key)
        {
            return _context.Suppliers.Where(s => s.SupplierId == key).SelectMany(s => s.Products);
        }
    }
}
