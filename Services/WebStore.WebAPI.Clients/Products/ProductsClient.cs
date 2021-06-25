using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
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

        public IEnumerable<Section> GetSections() => Get<IEnumerable<Section>>($"{Address}/sections");

        public Section GetSection(int id) => Get<Section>($"{Address}/sections/{id}");

        public IEnumerable<Brand> GetBrands() => Get<IEnumerable<Brand>>($"{Address}/brands");

        public Brand GetBrand(int id) => Get<Brand>($"{Address}/brands/{id}");

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            var response = Post(Address, Filter);
            var products = response.Content.ReadFromJsonAsync<IEnumerable<Product>>().Result;
            return products;
        }

        public Product GetProductById(int Id) => Get<Product>($"{Address}/{Id}");
    }
}
