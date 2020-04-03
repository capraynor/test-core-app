using GrapeCity.DataService.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GrapeCity.DataService.Controllers
{
    [ApiVersion("1.0")]
    public class OrderDetailsController : ODataController
    {
        private readonly NorthwindContext _context;

        public OrderDetailsController(NorthwindContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<OrderDetail> Get()
        {
            return _context.OrderDetails;
        }

        [EnableQuery]
        public SingleResult<OrderDetail> Get([FromODataUri]int keyOrderId, [FromODataUri]int keyProductId)
        {
            return SingleResult.Create(_context.OrderDetails.Where(od => od.OrderId == keyOrderId && od.ProductId == keyProductId));
        }

        [EnableQuery]
        public SingleResult<Order> GetOrder([FromODataUri]int keyOrderId, [FromODataUri]int keyProductId)
        {
            return SingleResult.Create(_context.OrderDetails.Where(od => od.OrderId == keyOrderId && od.ProductId == keyProductId).Select(od => od.Order));
        }

        [EnableQuery]
        public SingleResult<Product> GetProduct([FromODataUri]int keyOrderId, [FromODataUri]int keyProductId)
        {
            return SingleResult.Create(_context.OrderDetails.Where(od => od.OrderId == keyOrderId && od.ProductId == keyProductId).Select(od => od.Product));
        }
    }
}
