using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Entities;

namespace WebStore.Domain.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public BrandDTO Brand { get; set; }

        public SectionDTO Section { get; set; }
    }

    public class BrandDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }
    }

    public class SectionDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public int? ParentId { get; set; }
    }

    public static class ProductDTOMapper
    {
        public static ProductDTO ToDTO(this Product Product) => Product is null
            ? null
            : new ProductDTO
            {
                Id = Product.Id,
                Name = Product.Name,
                Order = Product.Order,
                Price = Product.Price,
                ImageUrl = Product.ImageUrl,
                Brand = Product.Brand.ToDTO(),
                Section = Product.Section.ToDTO(),
            };

        public static Product FromDTO(this ProductDTO Product) => Product is null
            ? null
            : new Product
            {
                Id = Product.Id,
                Name = Product.Name,
                Order = Product.Order,
                Price = Product.Price,
                ImageUrl = Product.ImageUrl,
                Brand = Product.Brand.FromDTO(),
                Section = Product.Section.FromDTO(),
            };

        public static IEnumerable<ProductDTO> ToDTO(this IEnumerable<Product> Products) => Products.Select(ToDTO);
        public static IEnumerable<Product> FromDTO(this IEnumerable<ProductDTO> Products) => Products.Select(FromDTO);
    }

    public static class SectionDTOMapper
    {
        public static SectionDTO ToDTO(this Section Section) => Section is null
            ? null
            : new SectionDTO
            {
                Id = Section.Id,
                Name = Section.Name,
                Order = Section.Order,
                ParentId = Section.ParentId,
            };

        public static Section FromDTO(this SectionDTO Section) => Section is null
            ? null
            : new Section
            {
                Id = Section.Id,
                Name = Section.Name,
                Order = Section.Order,
                ParentId = Section.ParentId,
            };

        public static IEnumerable<SectionDTO> ToDTO(this IEnumerable<Section> Sections) => Sections.Select(ToDTO);
        public static IEnumerable<Section> FromDTO(this IEnumerable<SectionDTO> Sections) => Sections.Select(FromDTO);
    }

    public static class BrandDTOMapper
    {
        public static BrandDTO ToDTO(this Brand Brand) => Brand is null
            ? null
            : new BrandDTO
            {
                Id = Brand.Id,
                Name = Brand.Name,
                Order = Brand.Order,
            };

        public static Brand FromDTO(this BrandDTO Brand) => Brand is null
            ? null
            : new Brand
            {
                Id = Brand.Id,
                Name = Brand.Name,
                Order = Brand.Order,
            };

        public static IEnumerable<BrandDTO> ToDTO(this IEnumerable<Brand> Brands) => Brands.Select(ToDTO);
        public static IEnumerable<Brand> FromDTO(this IEnumerable<BrandDTO> Brands) => Brands.Select(FromDTO);
    }
}
