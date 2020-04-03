using GrapeCity.DataService.DTO;
using GrapeCity.DataService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GrapeCity.DataService.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("northwind/api/Customers")]
    [Route("northwind/api/v{version:apiVersion}/Customers")]
    [ApiController]
    public class CustomersApiController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public CustomersApiController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<CustomerDto> GetCustomers()
        {
            return _context.Customers.Select(DtoConverter.AsCustomerDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(string id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return DtoConverter.ConvertToCustomerDto(customer);
        }

        [HttpGet("{id}/Orders")]
        public IQueryable<OrderDto> GetOrdersOfCustomer(string id)
        {
            return _context.Customers.Where(c => c.CustomerId == id).SelectMany(c => c.Orders).Select(DtoConverter.AsOrderDto);
        }
    }
}
