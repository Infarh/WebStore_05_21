using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using WebStore.Controllers;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            var configuration_mock = new Mock<IConfiguration>();
            var product_data_mock = new Mock<IProductData>();
            product_data_mock.Setup(s => s.GetProducts(It.IsAny<ProductFilter>()))
               .Returns(Enumerable.Empty<Product>());
               //.Returns(Enumerable.Range(1, 10).Select(i => new Product { Name = $"Product {i}" }));

            var controller = new HomeController(configuration_mock.Object);

            var result = controller.Index(product_data_mock.Object);

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Blog_Returns_View()
        {
            var configuration_mock = new Mock<IConfiguration>();

            var controller = new HomeController(configuration_mock.Object);

            var result = controller.Blog();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void SecondAction_Returns_View()
        {
            const string expected_result_str = "Test string!";
            var configuration_mock = new Mock<IConfiguration>();
            configuration_mock.Setup(c => c["Greetings"]).Returns(expected_result_str);

            var controller = new HomeController(configuration_mock.Object);

            var result = controller.SecondAction();

            var content_result = Assert.IsType<ContentResult>(result);
            Assert.Equal(expected_result_str, content_result.Content);
        }
    }
}
