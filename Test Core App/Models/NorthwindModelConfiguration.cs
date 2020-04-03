using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace GrapeCity.DataService.Models
{
    public class NorthwindModelConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
        {
            builder.EntitySet<Category>("Categories");
            builder.EntitySet<Customer>("Customers");
            builder.EntitySet<Product>("Products");
            builder.EntitySet<Employee>("Employees");
            builder.EntitySet<Order>("Orders");
            builder.EntitySet<Territory>("Territories");
            builder.EntityType<OrderDetail>().HasKey(t => new { t.OrderId, t.ProductId });
            builder.EntitySet<OrderDetail>("OrderDetails");
            builder.EntitySet<Shipper>("Shippers");
            builder.EntitySet<Supplier>("Suppliers");
            builder.EntitySet<Region>("Regions");
        }
    }
}
