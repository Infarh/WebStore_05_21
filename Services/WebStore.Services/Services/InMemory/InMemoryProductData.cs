using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;

namespace WebStore.Services.Services.InMemory
{
    [Obsolete("Поддержка класса размещения товаров в памяти прекращена", true)]
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections() => TestData.Sections;

        public Section GetSection(int id) => throw new NotSupportedException();

        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public Brand GetBrand(int id) => throw new NotSupportedException();

        public ProductsPage GetProducts(ProductFilter Filter = null)
        {
            IEnumerable<Product> query = TestData.Products;

            //if(Filter?.SectionId != null)
            //    query = query.Where(product => product.SectionId == Filter.SectionId);

            if (Filter?.SectionId is { } section_id)
                query = query.Where(product => product.SectionId == section_id);

            if(Filter?.BrandId is { } brand_id)
                query = query.Where(product => product.BrandId == brand_id);

            var total_count = query.Count();

            if (Filter is { PageSize: > 0 and var page_size, Page: > 0 and var page_number })
                query = query
                   .Skip((page_number - 1) * page_size)
                   .Take(page_size);

            return new ProductsPage(query, total_count);
        }

        public Product GetProductById(int Id) => TestData.Products.SingleOrDefault(p => p.Id == Id);
    }
}
