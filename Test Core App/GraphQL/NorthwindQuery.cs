using GrapeCity.DataService.Models;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GrapeCity.DataService.GraphQL
{
    public class CategoryType : ObjectGraphType<Category>
    {
        public CategoryType()
        {
            Field(x => x.CategoryId);
            Field(x => x.CategoryName);
            Field(x => x.Description);
            Field("Picture", x => Convert.ToBase64String(x.Picture));
            Field<ListGraphType<ProductType>>("Products");
        }
    }

    public class CustomerType : ObjectGraphType<Customer>
    {
        public CustomerType()
        {
            Field(x => x.CustomerId);
            Field(x => x.CompanyName);
            Field(x => x.ContactName);
            Field(x => x.ContactTitle);
            Field(x => x.Address);
            Field(x => x.City);
            Field(x => x.Region);
            Field(x => x.PostalCode);
            Field(x => x.Country);
            Field(x => x.Phone);
            Field(x => x.Fax);
            Field<ListGraphType<OrderType>>("Orders");
        }
    }

    public class EmployeeType : ObjectGraphType<Employee>
    {
        public EmployeeType()
        {
            Field(x => x.EmployeeId);
            Field(x => x.LastName);
            Field(x => x.FirstName);
            Field(x => x.Title);
            Field(x => x.TitleOfCourtesy);
            Field(x => x.BirthDate, true);
            Field(x => x.HireDate, true);
            Field(x => x.Address);
            Field(x => x.City);
            Field(x => x.Region);
            Field(x => x.PostalCode);
            Field(x => x.Country);
            Field(x => x.HomePhone);
            Field(x => x.Extension);
            Field(x => x.Notes);
            Field(x => x.ReportsTo, true);
            Field(x => x.PhotoPath);
            Field("Photo", x => Convert.ToBase64String(x.Photo));
            Field<EmployeeType>("Superior");
            Field<ListGraphType<EmployeeType>>("Subordinates");
            Field<ListGraphType<OrderType>>("Orders");
            Field<ListGraphType<EmployeeTerritoryType>>("EmployeeTerritories");
        }
    }

    public class EmployeeTerritoryType : ObjectGraphType<EmployeeTerritory> 
    {
        public EmployeeTerritoryType() 
        {
            Field(x => x.EmployeeId);
            Field(x => x.TerritoryId);
            Field<EmployeeType>("Employee");
            Field<TerritoryType>("Territory");
        }
    }

    public class OrderDetailType : ObjectGraphType<OrderDetail>
    {
        public OrderDetailType()
        {
            Field(x => x.OrderId);
            Field(x => x.ProductId);
            Field(x => x.UnitPrice);
            Field("Quantity", x => (int)x.Quantity);
            Field(x => x.Discount);
            Field<ListGraphType<OrderType>>("Order");
            Field<ProductType>("Product");
        }

    }
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType()
        {
            Field(x => x.OrderId);
            Field(x => x.CustomerId);
            Field(x => x.EmployeeId, true);
            Field(x => x.OrderDate, true);
            Field(x => x.RequiredDate, true);
            Field(x => x.ShippedDate, true);
            Field(x => x.ShipVia, true);
            Field(x => x.Freight, true);
            Field(x => x.ShipName);
            Field(x => x.ShipAddress);
            Field(x => x.ShipCity);
            Field(x => x.ShipRegion);
            Field(x => x.ShipPostalCode);
            Field(x => x.ShipCountry);
            Field<CustomerType>("Customer");
            Field<EmployeeType>("Employee");
            Field<ListGraphType<OrderDetailType>>("OrderDetails");
            Field<ShipperType>("Shipper");
        }
    }

    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType()
        {
            Field(x => x.ProductName).Description("Product Name");
            Field(x => x.ProductId).Description("Product ID");
            Field(x => x.SupplierId, true);
            Field(x => x.CategoryId, true);
            Field(x => x.QuantityPerUnit);
            Field(x => x.UnitPrice, true);
            Field("UnitsInStock", x => (int)x.UnitsInStock, true);
            Field("UnitsOnOrder", x => (int)x.UnitsOnOrder, true);
            Field("ReorderLevel", x => (int)x.ReorderLevel, true);
            Field(x => x.Discontinued);
            Field<CategoryType>("Category");
            Field<SupplierType>("Supplier");
            Field<ListGraphType<OrderDetailType>>("OrderDetails");
        }
    }

    public class RegionType : ObjectGraphType<Region>
    {
        public RegionType()
        {
            Field(x => x.RegionId);
            Field(x => x.RegionDescription);
            Field<ListGraphType<TerritoryType>>("Territories");
        }
    }

    public class ShipperType : ObjectGraphType<Shipper>
    {
        public ShipperType()
        {
            Field(x => x.ShipperId);
            Field(x => x.CompanyName);
            Field(x => x.Phone);
            Field<ListGraphType<OrderType>>("Orders");
        }
    }

    public class SupplierType : ObjectGraphType<Supplier>
    {
        public SupplierType()
        {
            Field(x => x.SupplierId);
            Field(x => x.CompanyName);
            Field(x => x.ContactName);
            Field(x => x.ContactTitle);
            Field(x => x.Address);
            Field(x => x.City);
            Field(x => x.Region);
            Field(x => x.PostalCode);
            Field(x => x.Country);
            Field(x => x.Phone);
            Field(x => x.Fax);
            Field(x => x.HomePage);
            Field<ListGraphType<ProductType>>("Products");
        }
    }

    public class TerritoryType : ObjectGraphType<Territory>
    {
        public TerritoryType()
        {
            Field(x => x.TerritoryId);
            Field(x => x.TerritoryDescription);
            Field(x => x.RegionId);
            Field<RegionType>("Region");
            Field<ListGraphType<EmployeeTerritoryType>>("EmployeeTerritories");
        }
    }

    public class NorthwindQuery : ObjectGraphType
    {
        private readonly NorthwindContext _context;

        public NorthwindQuery(NorthwindContext dbContext)
        {
            _context = dbContext;

            Field<ListGraphType<CategoryType>>("categories",
                  resolve: context => this.GetCategories());

            Field<CategoryType>("category",
                  arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                  resolve: context => this.GetCategory(context.GetArgument<int>("id")));

            Field<ListGraphType<CustomerType>>("customers",
                  resolve: context => this.GetCustomers());

            Field<CustomerType>("customer",
                  arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "id" }),
                  resolve: context => this.GetCustomer(context.GetArgument<string>("id")));

            Field<ListGraphType<EmployeeType>>("employees",
                  resolve: context => this.GetEmployees());

            Field<EmployeeType>("employee",
                  arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                  resolve: context => this.GetEmployee(context.GetArgument<int>("id")));

            Field<ListGraphType<OrderDetailType>>("orderdetails",
                  resolve: context => this.GetOrderDetails());

            Field<OrderDetailType>("orderdetail",
                  arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "orderID" }, new QueryArgument<IntGraphType> { Name = "productID" }),
                  resolve: context => this.GetOrderDetail(context.GetArgument<int>("orderID"), context.GetArgument<int>("productID")));

            Field<ListGraphType<OrderType>>("orders",
                  resolve: context => this.GetCategories());

            Field<OrderType>("order",
                  arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                  resolve: context => this.GetOrder(context.GetArgument<int>("id")));

            Field<ListGraphType<ProductType>>("products",
                  resolve: context => this.GetProducts());

            Field<ProductType>("product",
                  arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                  resolve: context => this.GetProduct(context.GetArgument<int>("id")));

            Field<ListGraphType<RegionType>>("regions",
                  resolve: context => this.GetRegions());

            Field<RegionType>("region",
                  arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                  resolve: context => this.GetRegion(context.GetArgument<int>("id")));

            Field<ListGraphType<ShipperType>>("shippers",
                  resolve: context => this.GetShippers());

            Field<ShipperType>("shipper",
                  arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                  resolve: context => this.GetShipper(context.GetArgument<int>("id")));

            Field<ListGraphType<SupplierType>>("suppliers",
                  resolve: context => this.GetSuppliers());

            Field<SupplierType>("supplier",
                  arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                  resolve: context => this.GetSupplier(context.GetArgument<int>("id")));

            Field<ListGraphType<TerritoryType>>("territories",
                  resolve: context => this.GetTerritories());

            Field<TerritoryType>("territory",
                  arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "id" }),
                  resolve: context => this.GetTerritory(context.GetArgument<string>("id")));

        }

        [GraphQLMetadata("categories")]
        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        [GraphQLMetadata("category")]
        public Category GetCategory(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        [GraphQLMetadata("customers")]
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        [GraphQLMetadata("customer")]
        public Customer GetCustomer(string id)
        {
            return _context.Customers.FirstOrDefault(c => c.CustomerId == id);
        }

        [GraphQLMetadata("employees")]
        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }

        [GraphQLMetadata("employee")]
        public Employee GetEmployee(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
        }

        [GraphQLMetadata("orderdetails")]
        public IEnumerable<OrderDetail> GetOrderDetails()
        {
            return _context.OrderDetails.ToList();
        }

        [GraphQLMetadata("orderdetail")]
        public OrderDetail GetOrderDetail(int orderId, int productId)
        {
            return _context.OrderDetails.FirstOrDefault(o => o.OrderId == orderId && o.ProductId == productId);
        }

        [GraphQLMetadata("orders")]
        public IEnumerable<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        [GraphQLMetadata("order")]
        public Order GetOrder(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == id);
        }

        [GraphQLMetadata("products")]
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        [GraphQLMetadata("product")]
        public Product GetProduct(int id)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == id);
        }

        [GraphQLMetadata("regions")]
        public IEnumerable<Region> GetRegions()
        {
            return _context.Regions.ToList();
        }

        [GraphQLMetadata("region")]
        public Region GetRegion(int id)
        {
            return _context.Regions.FirstOrDefault(r => r.RegionId == id);
        }

        [GraphQLMetadata("shippers")]
        public IEnumerable<Shipper> GetShippers()
        {
            return _context.Shippers.ToList();
        }

        [GraphQLMetadata("shipper")]
        public Shipper GetShipper(int id)
        {
            return _context.Shippers.FirstOrDefault(s => s.ShipperId == id);
        }

        [GraphQLMetadata("suppliers")]
        public IEnumerable<Supplier> GetSuppliers()
        {
            return _context.Suppliers.ToList();
        }

        [GraphQLMetadata("supplier")]
        public Supplier GetSupplier(int id)
        {
            return _context.Suppliers.FirstOrDefault(s => s.SupplierId == id);
        }

        [GraphQLMetadata("territories")]
        public IEnumerable<Territory> GetTerritories()
        {
            return _context.Territories.ToList();
        }

        [GraphQLMetadata("territory")]
        public Territory GetTerritory(string id)
        {
            return _context.Territories.FirstOrDefault(t => t.TerritoryId == id);
        }
    }
}