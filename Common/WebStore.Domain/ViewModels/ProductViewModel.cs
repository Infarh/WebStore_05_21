using System.Collections.Generic;

namespace WebStore.ViewModels
{
    public class CatalogViewModel
    {
        public int? SectionId { get; set; }

        public int? BrandId { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }

    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string Section { get; set; }

        public string Brand { get; set; }
    }
}
