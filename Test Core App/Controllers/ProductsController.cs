using GrapeCity.DataService.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GrapeCity.DataService.Controllers
{
    [ApiVersion("1.0")]
    public class ProductsController : ODataController
    {
        private readonly NorthwindContext _context;

        public ProductsController(NorthwindContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Product> Get()
        {
            return _context.Products;
        }

        [EnableQuery]
        public SingleResult<Product> Get([FromODataUri]int key)
        {
            return SingleResult.Create(_context.Products.Where(p => p.ProductId == key));
        }

        [EnableQuery]
        public SingleResult<Category> GetCategory([FromODataUri]int key)
        {
            return SingleResult.Create(_context.Products.Where(p => p.ProductId == key).Select(p => p.Category));
        }

        [EnableQuery]
        public IQueryable<OrderDetail> GetOrderDetails([FromODataUri]int key)
        {
            return _context.Products.Where(p => p.ProductId == key).SelectMany(p => p.OrderDetails);
        }

        [EnableQuery]
        public SingleResult<Supplier> GetSupplier([FromODataUri]int key)
        {
            return SingleResult.Create(_context.Products.Where(p => p.ProductId == key).Select(p => p.Supplier));
        }
    }
}
