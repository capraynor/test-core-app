using GrapeCity.DataService.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GrapeCity.DataService.Controllers
{
    [ApiVersion("1.0")]
    public class OrdersController : ODataController
    {
        private readonly NorthwindContext _context;

        public OrdersController(NorthwindContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Order> Get()
        {
            return _context.Orders;
        }

        [EnableQuery]
        public SingleResult<Order> Get([FromODataUri]int key)
        {
            return SingleResult.Create(_context.Orders.Where(o => o.OrderId == key));
        }

        [EnableQuery]
        public SingleResult<Customer> GetCustomer([FromODataUri]int key)
        {
            return SingleResult.Create(_context.Orders.Where(o => o.OrderId == key).Select(o => o.Customer));
        }

        [EnableQuery]
        public SingleResult<Employee> GetEmployee([FromODataUri]int key)
        {
            return SingleResult.Create(_context.Orders.Where(o => o.OrderId == key).Select(o => o.Employee));
        }


        [EnableQuery]
        public IQueryable<OrderDetail> GetOrderDetails([FromODataUri]int key)
        {
            return _context.Orders.Where(o => o.OrderId == key).SelectMany(o => o.OrderDetails);
        }

        [EnableQuery]
        public SingleResult<Shipper> GetShipper([FromODataUri]int key)
        {
            return SingleResult.Create(_context.Orders.Where(o => o.OrderId == key).Select(o => o.Shipper));
        }
    }
}
