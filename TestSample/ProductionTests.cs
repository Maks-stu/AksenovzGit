using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary.Classes;

namespace TestSample
{
    [TestClass]
    public class ProductionTests
    {
        [TestMethod]
        public void CreateProduction_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var production = new Production
            {
                Id = 1,
                Article = "A123",
                Name = "Sample Product",
                Description = "Description of product",
                Photo = "photo_link.jpg",
                MinPartnerPrice = 99.99,
                Length = 50,
                Width = 20,
                Height = 10,
                NetWeight = 2.5,
                GrossWeight = 3.0,
                QualityCertificate = "QC12345",
                StandardNumber = "Std987",
                CostPrice = 70.00
            };

            // Act
            // Можно тут проверить свойства через Assert

            // Assert
            Assert.AreEqual(1, production.Id);
            Assert.AreEqual("A123", production.Article);
            Assert.AreEqual("Sample Product", production.Name);
            Assert.AreEqual("Description of product", production.Description);
            Assert.AreEqual("photo_link.jpg", production.Photo);
            Assert.AreEqual(99.99, production.MinPartnerPrice);
            Assert.AreEqual(50, production.Length);
            Assert.AreEqual(20, production.Width);
            Assert.AreEqual(10, production.Height);
            Assert.AreEqual(2.5, production.NetWeight);
            Assert.AreEqual(3.0, production.GrossWeight);
            Assert.AreEqual("QC12345", production.QualityCertificate);
            Assert.AreEqual("Std987", production.StandardNumber);
            Assert.AreEqual(70.00, production.CostPrice);
        }

        [TestMethod]
        public void Production_DefaultConstructor_ShouldInitializePropertiesWithDefaults()
        {
            // Arrange & Act
            var production = new Production();

            // Assert
            Assert.AreEqual(0, production.Id);
            Assert.IsNull(production.Article);
            Assert.IsNull(production.Name);
            Assert.IsNull(production.Description);
            Assert.IsNull(production.Photo);
            Assert.AreEqual(0, production.MinPartnerPrice);
            Assert.IsNull(production.Length);
            Assert.IsNull(production.Width);
            Assert.IsNull(production.Height);
            Assert.IsNull(production.NetWeight);
            Assert.IsNull(production.GrossWeight);
            Assert.IsNull(production.QualityCertificate);
            Assert.IsNull(production.StandardNumber);
            Assert.IsNull(production.CostPrice);
        }

        // Дополнительные тесты можно добавить по необходимости, например,
        // проверки на валидацию данных, сериализацию и т.д.
    }
}