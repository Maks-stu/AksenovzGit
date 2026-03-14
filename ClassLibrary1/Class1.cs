using OfficeOpenXml;
using ClassLibrary.Classes;
using ClassLibrary.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class MarketExcelGeneratorTests
    {
        private TableReport CreateTestTableReport()
        {
            return new TableReport
            {
                partnerDtos = new List<PartnerDto>
                {
                    new PartnerDto { Type = "Type1", Name = "Partner1", Discount = 10, DirectorName = "Director1", Phone = "123", Rating = 4 },
                    new PartnerDto { Type = "Type2", Name = "Partner2", Discount = 15, DirectorName = "Director2", Phone = "456", Rating = 4 },
                },
                salesDtos = new List<ProductSalesDto>
                {
                    new ProductSalesDto { Date = "2024-01-01", Count = 5, Production = "ProdA", TotalPrice = 1000 },
                    new ProductSalesDto { Date = "2024-01-02", Count = 3, Production = "ProdB", TotalPrice = 600 },
                }
            };
        }

        [TestMethod]
        public void Generate_ShouldCreatePartnerAndSalesSheets_WhenIsInOneFileIs0()
        {
            // Arrange
            var report = CreateTestTableReport();

            // Act
            var bytes = MarketExcelGenerator.Generate(report, 0);

            // Assert
            using (var stream = new MemoryStream(bytes))
            using (var package = new ExcelPackage(stream))
            {
                // Проверка листа "Партнеры"
                var sheet = package.Workbook.Worksheets["Партнеры"];
                Assert.IsNotNull(sheet);
                Assert.AreEqual("Тип", sheet.Cells["B2"].Text);
                Assert.AreEqual("Partner1", sheet.Cells["C3"].Text);
                Assert.AreEqual(10, sheet.Cells["D3"].GetValue<int>());
                Assert.AreEqual("Director1", sheet.Cells["E3"].Text);

                // Проверка листа "История закупок"
                var salesSheet = package.Workbook.Worksheets["История закупок"];
                Assert.IsNotNull(salesSheet);
                Assert.AreEqual("Дата", salesSheet.Cells["B2"].Text);
                Assert.AreEqual("2024-01-01", salesSheet.Cells["B3"].Text);
                Assert.AreEqual(5, salesSheet.Cells["C3"].GetValue<int>());
            }
        }

        [TestMethod]
        public void Generate_ShouldCreateOnlyPartnerSheet_WhenIsInOneFileIs1()
        {
            var report = CreateTestTableReport();

            var bytes = MarketExcelGenerator.Generate(report, 1);

            using (var stream = new MemoryStream(bytes))
            using (var package = new ExcelPackage(stream))
            {
                Assert.IsNotNull(package.Workbook.Worksheets["Партнеры"]);
                Assert.IsNull(package.Workbook.Worksheets["История закупок"]);
            }
        }

        [TestMethod]
        public void Generate_ShouldCreateBothSheets_WhenIsInOneFileIs2()
        {
            var report = CreateTestTableReport();

            var bytes = MarketExcelGenerator.Generate(report, 2);

            using (var stream = new MemoryStream(bytes))
            using (var package = new ExcelPackage(stream))
            {
                Assert.IsNotNull(package.Workbook.Worksheets["Партнеры"]);
                Assert.IsNotNull(package.Workbook.Worksheets["История закупок"]);
            }
        }

        [TestMethod]
        public void GenerateBackUp_ShouldCreateAllBackupSheets()
        {
            // Arrange
            var rootObject = new RootObject
            {
                Partners = new List<Partner>
                {
                    new Partner { Id=1, Type="TypeA", Name="PartnerA", LegalAddress="Addr1", Inn="123456", DirectorName="DirA", Phone="987", Email="a@b.com", Logo="logo.png", Rating=4 }
                },
                Productions = new List<Production>
                {
                    new Production { Id=1, Article="A001", Name="ProdName", Description="desc", Photo="photo.png", MinPartnerPrice=100, Length=10, Width=5, Height=2, NetWeight=1.5, GrossWeight=2.0, QualityCertificate="Cert", StandardNumber="Std", CostPrice=50 }
                },
                Products = new List<ProductSale>
                {
                    new ProductSale { Id=1, PartnerId=1, ProductionId=1, Count=10, SaleDate= new DateOnly(2024, 1, 15), TotalPrice=1000 }
                }
            };

            // Act
            var bytes = MarketExcelGenerator.GenerateBackUp(rootObject);

            // Assert
            using (var stream = new MemoryStream(bytes))
            using (var package = new ExcelPackage(stream))
            {
                Assert.IsNotNull(package.Workbook.Worksheets["Партнеры"]);
                Assert.IsNotNull(package.Workbook.Worksheets["Производство"]);
                Assert.IsNotNull(package.Workbook.Worksheets["Продажи товаров"]);

                var partnerSheet = package.Workbook.Worksheets["Партнеры"];
                Assert.AreEqual("Id", partnerSheet.Cells["B2"].Text);
                Assert.AreEqual(1, partnerSheet.Cells["B3"].GetValue<int>());

                var productionSheet = package.Workbook.Worksheets["Производство"];
                Assert.AreEqual(1, productionSheet.Cells["B3"].GetValue<int>());

                var salesSheet = package.Workbook.Worksheets["Продажи товаров"];
                Assert.AreEqual(1, salesSheet.Cells["B3"].GetValue<int>());
            }
        }
    }
}