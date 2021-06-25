using System.Collections.Generic;
using System.Net.Http;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(HttpClient Client) : base(Client, WebAPIAddress.Products) { }

        public IEnumerable<Section> GetSections() { throw new System.NotImplementedException(); }

        public Section GetSection(int id) { throw new System.NotImplementedException(); }

        public IEnumerable<Brand> GetBrands() { throw new System.NotImplementedException(); }

        public Brand GetBrand(int id) { throw new System.NotImplementedException(); }

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null) { throw new System.NotImplementedException(); }

        public Product GetProductById(int Id) { throw new System.NotImplementedException(); }
    }
}
