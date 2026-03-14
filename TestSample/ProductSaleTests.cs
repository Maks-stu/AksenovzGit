using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ClassLibrary.Classes; // импортируйте пространство имён вашего класса

namespace TestSample
{
    [TestClass]
    public class ProductSaleTests
    {
        [TestMethod]
        public void CreateProductSale_ShouldInitializeProperties()
        {
            // Arrange
            var saleId = 1;
            var partnerId = 100;
            var productionId = 200;
            var count = 10.5;
            var saleDate = DateOnly.FromDateTime(DateTime.Now);
            double? totalPrice = 150.75;

            // Act
            var productSale = new ProductSale
            {
                Id = saleId,
                PartnerId = partnerId,
                ProductionId = productionId,
                Count = count,
                SaleDate = saleDate,
                TotalPrice = totalPrice
            };

            // Assert
            Assert.AreEqual(saleId, productSale.Id);
            Assert.AreEqual(partnerId, productSale.PartnerId);
            Assert.AreEqual(productionId, productSale.ProductionId);
            Assert.AreEqual(count, productSale.Count);
            Assert.AreEqual(saleDate, productSale.SaleDate);
            Assert.AreEqual(totalPrice, productSale.TotalPrice);
        }

        [TestMethod]
        public void ProductSale_TotalPrice_CanBeNull()
        {
            // Arrange
            var sale = new ProductSale { TotalPrice = null };

            // Act & Assert
            Assert.IsNull(sale.TotalPrice);
        }
    }
}