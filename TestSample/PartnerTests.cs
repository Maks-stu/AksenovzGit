using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ClassLibrary.Classes;

namespace TestSample
{
    [TestClass]
    public class PartnerUnitTests
    {
        // Помогает создать экземпляр Partner с заданным Id
        private static Partner CreatePartner(int id)
        {
            return new Partner { Id = id };
        }

        // Создает список ProductSale для тестирования
        private static List<ProductSale> CreateSales(int partnerId, int count)
        {
            var sales = new List<ProductSale>();
            for (int i = 0; i < count; i++)
            {
                sales.Add(new ProductSale { PartnerId = partnerId, Count = 1 });
            }
            return sales;
        }

        [TestMethod]
        public void GetDiscount_Returns_0_When_TotalProducts_Less_Than_10000()
        {
            var partner = CreatePartner(1);
            var sales = CreateSales(1, 9999);
            var result = partner.GetDiscount(sales);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetDiscount_Returns_5_When_TotalProducts_Between_10000_And_50000()
        {
            var partner = CreatePartner(2);
            var sales = CreateSales(2, 15000);
            var result = partner.GetDiscount(sales);
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void GetDiscount_Returns_10_When_TotalProducts_Between_50000_And_300000()
        {
            var partner = CreatePartner(3);
            var sales = CreateSales(3, 100000);
            var result = partner.GetDiscount(sales);
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void GetDiscount_Returns_15_When_TotalProducts_Greater_Than_300000()
        {
            var partner = CreatePartner(4);
            var sales = CreateSales(4, 350000);
            var result = partner.GetDiscount(sales);
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void GetDiscount_Returns_0_When_No_Sales()
        {
            var partner = CreatePartner(5);
            var sales = new List<ProductSale>();
            var result = partner.GetDiscount(sales);
            Assert.AreEqual(0, result);
        }
    }
}