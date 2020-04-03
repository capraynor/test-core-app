using GrapeCity.DataService.DTO;
using GrapeCity.DataService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GrapeCity.DataService.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("northwind/api/Orders")]
    [Route("northwind/api/v{version:apiVersion}/Orders")]
    [ApiController]
    public class OrdersApiController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public OrdersApiController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<OrderDto> GetOrders()
        {
            return _context.Orders.Select(DtoConverter.AsOrderDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return DtoConverter.ConvertToOrderDto(order);
        }

        [HttpGet("{id}/Customer")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            Order order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return DtoConverter.ConvertToCustomerDto(order.Customer);
        }

        [HttpGet("{id}/Employee")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            Order order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return DtoConverter.ConvertToEmployeeDto(order.Employee);
        }

        [HttpGet("{id}/OrderDetails")]
        public IQueryable<OrderDetailDto> GetOrderDetails(int id)
        {
            return _context.Orders.Where(o => o.OrderId == id).SelectMany(o => o.OrderDetails).Select(DtoConverter.AsOrderDetailDto);
        }

        [HttpGet("{id}/Shipper")]
        public async Task<ActionResult<ShipperDto>> GetShipper(int id)
        {
            Order order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return DtoConverter.ConvertToShipperDto(order.Shipper);
        }

        [HttpGet("{id}/Products")]
        public IQueryable<ProductDto> GetProducts(int id)
        {
            return _context.OrderDetails.Where(od => od.OrderId == id).Select(od => od.Product).Select(DtoConverter.AsProductDto);
        }
    }
}
