using GrapeCity.DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace GrapeCity.DataService.DTO
{
    public static class DtoConverter
    {
        // Typed lambda expression for Select() method.
        public static readonly Expression<Func<Category, CategoryDto>> AsCategoryDto =
            source => new CategoryDto()
            {
                CategoryId = source.CategoryId,
                CategoryName = source.CategoryName,
                Description = source.Description
            };

        public static readonly Func<Category, CategoryDto> ConvertToCategoryDto = AsCategoryDto.Compile();

        public static readonly Expression<Func<Category, CategoryDetailDto>> AsCategoryDetailDto =
            source => new CategoryDetailDto()
            {
                CategoryId = source.CategoryId,
                CategoryName = source.CategoryName,
                Description = source.Description,
                Picture = Convert.ToBase64String(source.Picture),
                ProductNames = source.Products.Select(p => p.ProductName)
            };

        public static readonly Func<Category, CategoryDetailDto> ConvertToCategoryDetailDto = AsCategoryDetailDto.Compile();


        public static readonly Expression<Func<Product, ProductDto>> AsProductDto =
            source => new ProductDto()
            {
                ProductId = source.ProductId,
                ProductName = source.ProductName,
                SupplierId = source.SupplierId,
                CategoryId = source.CategoryId,
                QuantityPerUnit = source.QuantityPerUnit,
                UnitPrice = source.UnitPrice,
                UnitsInStock = source.UnitsInStock,
                UnitsOnOrder = source.UnitsOnOrder,
                ReorderLevel = source.ReorderLevel,
                Discontinued = source.Discontinued
            };

        public static readonly Func<Product, ProductDto> ConvertToProductDto = AsProductDto.Compile();

        public static readonly Expression<Func<Customer, CustomerDto>> AsCustomerDto =
            source => new CustomerDto()
            {
                CustomerId = source.CustomerId,
                CompanyName = source.CompanyName,
                ContactName = source.ContactName,
                ContactTitle = source.ContactTitle,
                Address = source.Address,
                City = source.City,
                Region = source.Region,
                PostalCode = source.PostalCode,
                Country = source.Country,
                Phone = source.Phone,
                Fax = source.Fax
            };

        public static readonly Func<Customer, CustomerDto> ConvertToCustomerDto = AsCustomerDto.Compile();

        public static readonly Expression<Func<Order, OrderDto>> AsOrderDto =
            source => new OrderDto()
            {
                OrderId = source.OrderId,
                CustomerId = source.CustomerId,
                EmployeeId = source.EmployeeId,
                OrderDate = source.OrderDate,
                RequiredDate = source.RequiredDate,
                ShippedDate = source.ShippedDate,
                ShipVia = source.ShipVia,
                Freight = source.Freight,
                ShipName = source.ShipName,
                ShipAddress = source.ShipAddress,
                ShipCity = source.ShipCity,
                ShipRegion = source.ShipRegion,
                ShipPostalCode = source.ShipPostalCode,
                ShipCountry = source.ShipCountry
            };

        public static readonly Func<Order, OrderDto> ConvertToOrderDto = AsOrderDto.Compile();

        public static readonly Expression<Func<Employee, EmployeeDto>> AsEmployeeDto =
            source => new EmployeeDto()
            {
                EmployeeId = source.EmployeeId,
                LastName = source.LastName,
                FirstName = source.FirstName,
                Title = source.Title,
                TitleOfCourtesy = source.TitleOfCourtesy,
                BirthDate = source.BirthDate,
                HireDate = source.HireDate,
                Address = source.Address,
                City = source.City,
                Region = source.Region,
                PostalCode = source.PostalCode,
                Country = source.Country,
                HomePhone = source.HomePhone,
                Extension = source.Extension,
                Notes = source.Notes,
                ReportsTo = source.ReportsTo,
                Photo = Convert.ToBase64String(source.Photo),
                PhotoPath = source.PhotoPath
            };

        public static readonly Func<Employee, EmployeeDto> ConvertToEmployeeDto = AsEmployeeDto.Compile();

        public static readonly Expression<Func<OrderDetail, OrderDetailDto>> AsOrderDetailDto =
            source => new OrderDetailDto()
            {
                OrderId = source.OrderId,
                ProductId = source.ProductId,
                UnitPrice = source.UnitPrice,
                Quantity = source.Quantity,
                Discount = source.Discount
            };

        public static readonly Func<OrderDetail, OrderDetailDto> ConvertToOrderDetailDto = AsOrderDetailDto.Compile();

        public static readonly Expression<Func<Region, RegionDto>> AsRegionDto =
            source => new RegionDto()
            {
                RegionId = source.RegionId,
                RegionDescription = source.RegionDescription
            };

        public static readonly Func<Region, RegionDto> ConvertToRegionDto = AsRegionDto.Compile();

        public static readonly Expression<Func<Shipper, ShipperDto>> AsShipperDto =
            source => new ShipperDto()
            {
                ShipperId = source.ShipperId,
                CompanyName = source.CompanyName,
                Phone = source.Phone
            };

        public static readonly Func<Shipper, ShipperDto> ConvertToShipperDto = AsShipperDto.Compile();

        public static readonly Expression<Func<Supplier, SupplierDto>> AsSupplierDto =
            source => new SupplierDto()
            {
                SupplierId = source.SupplierId,
                CompanyName = source.CompanyName,
                ContactName = source.ContactName,
                ContactTitle = source.ContactTitle,
                Address = source.Address,
                City = source.City,
                Region = source.Region,
                PostalCode = source.PostalCode,
                Country = source.Country,
                Phone = source.Phone,
                Fax = source.Fax,
                HomePage = source.HomePage
            };

        public static readonly Func<Supplier, SupplierDto> ConvertToSupplierDto = AsSupplierDto.Compile();

        public static readonly Expression<Func<Territory, TerritoryDto>> AsTerritoryDto =
            source => new TerritoryDto()
            {
                TerritoryId = source.TerritoryId,
                TerritoryDescription = source.TerritoryDescription,
                RegionId = source.RegionId
            };

        public static readonly Func<Territory, TerritoryDto> ConvertToTerritoryDto = AsTerritoryDto.Compile();
    }
}