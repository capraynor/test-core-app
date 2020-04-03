using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GrapeCity.DataService.Models;
using GrapeCity.DataService.DTO;

namespace GrapeCity.DataService.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("northwind/api/Shippers")]
    [Route("northwind/api/v{version:apiVersion}/Shippers")]
    [ApiController]
    public class ShippersApiController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public ShippersApiController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<ShipperDto> GetShippers()
        {
            return _context.Shippers.Select(DtoConverter.AsShipperDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShipperDto>> GetShipper(int id)
        {
            var shipper = await _context.Shippers.FindAsync(id);

            if (shipper == null)
            {
                return NotFound();
            }

            return DtoConverter.ConvertToShipperDto(shipper);
        }

        [HttpGet("{id:int}/Orders")]
        public IQueryable<OrderDto> GetOrders(int id)
        {
            return _context.Shippers.Where(s => s.ShipperId == id).SelectMany(s => s.Orders).Select(DtoConverter.AsOrderDto);
        }

    }
}
