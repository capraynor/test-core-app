using GrapeCity.DataService.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GrapeCity.DataService.Controllers
{
    [ApiVersion("1.0")]
    public class CustomersController : ODataController
    {
        private readonly NorthwindContext _context;

        public CustomersController(NorthwindContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Customer> Get()
        {
            return _context.Customers;
        }

        [EnableQuery]
        public SingleResult<Customer> Get([FromODataUri]string key)
        {
            return SingleResult.Create(_context.Customers.Where(c => c.CustomerId == key));
        }

        [EnableQuery]
        public IQueryable<Order> GetOrders([FromODataUri]string key)
        {
            return _context.Customers.Where(c => c.CustomerId == key).SelectMany(c => c.Orders);
        }
    }
}
