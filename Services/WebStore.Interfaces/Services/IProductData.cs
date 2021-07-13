using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;

namespace WebStore.Interfaces.Services
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();

        Section GetSection(int id);

        IEnumerable<Brand> GetBrands();

        Brand GetBrand(int id);

        ProductsPage GetProducts(ProductFilter Filter = null);

        Product GetProductById(int Id);
    }
}
