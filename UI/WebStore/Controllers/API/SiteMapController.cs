using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using SimpleMvcSitemap;

using WebStore.Interfaces.Services;

namespace WebStore.Controllers.API
{
    public class SiteMapController : ControllerBase // http://localhost:5000/sitemap
    {
        public IActionResult Index([FromServices] IProductData ProductData)
        {
            var nodes = new List<SitemapNode>
            {
                new(Url.Action("Index", "Home")),
                new(Url.Action("SecondAction", "Home")),
                new(Url.Action("Blog", "Home")),
                new(Url.Action("Index", "Catalog")),
                new(Url.Action("Index", "WebAPI")),
            };

            nodes.AddRange(ProductData.GetSections().Select(section => new SitemapNode(Url.Action("Index", "Catalog", new { SectionId = section.Id }))));

            foreach (var brand in ProductData.GetBrands())
                nodes.Add(new SitemapNode(Url.Action("Index", "Catalog", new { BrandId = brand.Id })));

            foreach (var product in ProductData.GetProducts())
                nodes.Add(new SitemapNode(Url.Action("Details", "Catalog", new { product.Id })));

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}
