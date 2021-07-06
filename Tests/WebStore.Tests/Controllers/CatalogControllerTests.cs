using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using WebStore.Controllers;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class CatalogControllerTests
    {
        [TestMethod]
        public void Details_Returns_with_Correct_View()
        {
            // A-A-A = Arrange - Act - Assert

            #region Arrange

            const decimal expected_price = 10m;
            const int expected_id = 1;
            const string expected_name = "Product 1";

            var product_data_mock = new Mock<IProductData>();
            product_data_mock
               .Setup(p => p.GetProductById(It.IsAny<int>()))
               .Returns<int>(id => new Product
               {
                   Id = id,
                   Name = $"Product {id}",
                   Order = 1,
                   Price = expected_price,
                   ImageUrl = $"img_{id}.png",
                   Brand = new Brand { Id = 1, Name = "Brand", Order = 1 },
                   Section = new Section { Id = 1, Order = 1, Name = "Section" }
               });

            var controller = new CatalogController(product_data_mock.Object);

            #endregion

            #region Act

            var result = controller.Details(expected_id);

            #endregion

            #region Assert

            var view_result = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(view_result.Model);

            Assert.Equal(expected_id, model.Id);
            Assert.Equal(expected_name, model.Name);
            Assert.Equal(expected_price, model.Price);

            product_data_mock.Verify(s => s.GetProductById(It.IsAny<int>()));
            product_data_mock.VerifyNoOtherCalls();

            #endregion
        }
    }
}
