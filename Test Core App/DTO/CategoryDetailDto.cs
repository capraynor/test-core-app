using System.Collections.Generic;

namespace GrapeCity.DataService.DTO
{
    public partial class CategoryDetailDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public IEnumerable<string> ProductNames { get; set; }
    }
}