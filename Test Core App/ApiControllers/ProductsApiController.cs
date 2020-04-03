using GrapeCity.DataService.DTO;
using GrapeCity.DataService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GrapeCity.DataService.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("northwind/api/Products")]
    [Route("northwind/api/v{version:apiVersion}/Products")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public ProductsApiController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<ProductDto> GetProducts()
        {
            return _context.Products.Select(DtoConverter.AsProductDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return DtoConverter.ConvertToProductDto(product);
        }

        [HttpGet("{id}/Category")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return DtoConverter.ConvertToCategoryDto(product.Category);
        }

        [HttpGet("{id}/OrderDetails")]
        public IQueryable<OrderDetailDto> GetOrderDetails(int id)
        {
            return _context.Products.Where(p => p.ProductId == id).SelectMany(p => p.OrderDetails).Select(DtoConverter.AsOrderDetailDto);
        }

        [HttpGet("{id:int}/Supplier")]
        public async Task<ActionResult<SupplierDto>> GetSupplier(int id)
        {
            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return DtoConverter.ConvertToSupplierDto(product.Supplier);
        }
    }
}
