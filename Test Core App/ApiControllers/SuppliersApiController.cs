using GrapeCity.DataService.DTO;
using GrapeCity.DataService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GrapeCity.DataService.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("northwind/api/Suppliers")]
    [Route("northwind/api/v{version:apiVersion}/Suppliers")]
    [ApiController]
    public class SuppliersApiController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public SuppliersApiController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<SupplierDto> GetSuppliers()
        {
            return _context.Suppliers.Select(DtoConverter.AsSupplierDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto>> GetSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return DtoConverter.ConvertToSupplierDto(supplier);
        }

        [HttpGet("{id:int}/Products")]
        public IQueryable<ProductDto> GetProducts(int id)
        {
            return _context.Suppliers.Where(s => s.SupplierId == id).SelectMany(s => s.Products).Select(DtoConverter.AsProductDto);
        }

    }
}
