using System.Collections.Generic;
using WebStore.Domain.Entities;

namespace WebStore.Domain
{
    public record ProductsPage(IEnumerable<Product> Products, int TotalCount);
}
