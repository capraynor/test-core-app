using GrapeCity.DataService.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GrapeCity.DataService.Controllers
{
    [ApiVersion("1.0")]
    public class CategoriesController : ODataController
    {
        private readonly NorthwindContext _context;

        public CategoriesController(NorthwindContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Category> Get() {
            return _context.Categories;
        }

        [EnableQuery]
        public SingleResult<Category> Get([FromODataUri]int key) {
            return SingleResult.Create(_context.Categories.Where(category => category.CategoryId == key));
        }

        [EnableQuery]
        public IQueryable<Product> GetProducts([FromODataUri]int key)
        {
            return _context.Categories.Where(category => category.CategoryId == key).SelectMany(category => category.Products);
        }
    }
}
