using System;
using System.Collections.Generic;
using OfficeOpenXml;
using ClassLibrary.DTOs;

namespace ClassLibrary.Classes
{
    public class MarketExcelGenerator
    {
        public static byte[] Generate(TableReport tableReport, int isInOneFile)
        {
            ExcelPackage.License.SetNonCommercialPersonal("Aksenov");
            var package = new ExcelPackage();

            switch (isInOneFile)
            {
                case 0:
                case 1:
                    // Создаем таблицу для партнеров
                    CreatePartnerSheet(package, tableReport.partnerDtos);
                    // Создаем таблицу для истории продаж, если нужен
                    if (isInOneFile == 0 || isInOneFile == 2)
                    {
                        CreateSalesSheet(package, tableReport.salesDtos);
                    }
                    break;

                case 2:
                    CreatePartnerSheet(package, tableReport.partnerDtos);
                    CreateSalesSheet(package, tableReport.salesDtos);
                    break;
            }

            return package.GetAsByteArray();
        }

        // Метод для создания таблицы партнеров
        private static void CreatePartnerSheet(ExcelPackage package, List<PartnerDto> partnerDTOs)
        {
            var worksheet = package.Workbook.Worksheets.Add("Партнеры");
            // Заголовки таблицы
            worksheet.Cells["B2"].Value = "Тип";
            worksheet.Cells["C2"].Value = "Наименование";
            worksheet.Cells["D2"].Value = "Скидка";
            worksheet.Cells["E2"].Value = "ФИО директора";
            worksheet.Cells["F2"].Value = "Телефон";
            worksheet.Cells["G2"].Value = "Рейтинг";

            int row = 3;
            foreach (var partner in partnerDTOs)
            {
                worksheet.Cells[$"B{row}"].Value = partner.Type;
                worksheet.Cells[$"C{row}"].Value = partner.Name;
                worksheet.Cells[$"D{row}"].Value = partner.Discount;
                worksheet.Cells[$"E{row}"].Value = partner.DirectorName;
                worksheet.Cells[$"F{row}"].Value = partner.Phone;
                worksheet.Cells[$"G{row}"].Value = partner.Rating;
                row++;
            }
        }

        // Метод для создания таблицы истории продаж
        private static void CreateSalesSheet(ExcelPackage package, List<ProductSalesDto> salesDTOs)
        {
            var worksheet = package.Workbook.Worksheets.Add("История закупок");
            // Заголовки таблицы
            worksheet.Cells["B2"].Value = "Дата";
            worksheet.Cells["C2"].Value = "Количество";
            worksheet.Cells["D2"].Value = "Продукция";
            worksheet.Cells["E2"].Value = "Итоговая сумма";

            int row = 3;
            foreach (var sale in salesDTOs)
            {
                worksheet.Cells[$"B{row}"].Value = sale.Date;
                worksheet.Cells[$"C{row}"].Value = sale.Count;
                worksheet.Cells[$"D{row}"].Value = sale.Production;
                worksheet.Cells[$"E{row}"].Value = sale.TotalPrice;
                row++;
            }
        }

        public static byte[] GenerateBackUp(RootObject rootObject)
        {
            ExcelPackage.License.SetNonCommercialPersonal("Aksenov");
            var package = new ExcelPackage();

            // Создание таблицы для партнеров
            CreateBackupPartnerSheet(package, rootObject.Partners);
            // Создание таблицы для производств
            CreateBackupProductionSheet(package, rootObject.Productions);
            // Создание таблицы для продаж товаров
            CreateBackupSalesSheet(package, rootObject.Products);

            return package.GetAsByteArray();
        }

        private static void CreateBackupPartnerSheet(ExcelPackage package, List<Partner> partners)
        {
            var sheet = package.Workbook.Worksheets.Add("Партнеры");
            sheet.Cells["B2"].Value = "Id";
            sheet.Cells["C2"].Value = "Тип";
            sheet.Cells["D2"].Value = "Наименование";
            sheet.Cells["E2"].Value = "Юридический адрес";
            sheet.Cells["F2"].Value = "ИНН";
            sheet.Cells["G2"].Value = "ФИО директора";
            sheet.Cells["H2"].Value = "Телефон";
            sheet.Cells["I2"].Value = "Email";
            sheet.Cells["J2"].Value = "Логотип";
            sheet.Cells["K2"].Value = "Рейтинг";

            int row = 3;
            foreach (var partner in partners)
            {
                sheet.Cells[$"B{row}"].Value = partner.Id;
                sheet.Cells[$"C{row}"].Value = partner.Type;
                sheet.Cells[$"D{row}"].Value = partner.Name;
                sheet.Cells[$"E{row}"].Value = partner.LegalAddress;
                sheet.Cells[$"F{row}"].Value = partner.Inn;
                sheet.Cells[$"G{row}"].Value = partner.DirectorName;
                sheet.Cells[$"H{row}"].Value = partner.Phone;
                sheet.Cells[$"I{row}"].Value = partner.Email;
                sheet.Cells[$"J{row}"].Value = partner.Logo;
                sheet.Cells[$"K{row}"].Value = partner.Rating;
                row++;
            }
        }

        private static void CreateBackupProductionSheet(ExcelPackage package, List<Production> productions)
        {
            var sheet = package.Workbook.Worksheets.Add("Производство");
            sheet.Cells["B2"].Value = "Id";
            sheet.Cells["C2"].Value = "Артикул";
            sheet.Cells["D2"].Value = "Наименование";
            sheet.Cells["E2"].Value = "Описание";
            sheet.Cells["F2"].Value = "Фото";
            sheet.Cells["G2"].Value = "Мини цена партнера";
            sheet.Cells["H2"].Value = "Длина";
            sheet.Cells["I2"].Value = "Ширина";
            sheet.Cells["J2"].Value = "Высота";
            sheet.Cells["K2"].Value = "Вес.net";
            sheet.Cells["L2"].Value = "Вес.брутт";
            sheet.Cells["M2"].Value = "Сертификат качества";
            sheet.Cells["N2"].Value = "Стандартный номер";
            sheet.Cells["O2"].Value = "Цена себестоимости";

            int row = 3;
            foreach (var prod in productions)
            {
                sheet.Cells[$"B{row}"].Value = prod.Id;
                sheet.Cells[$"C{row}"].Value = prod.Article;
                sheet.Cells[$"D{row}"].Value = prod.Name;
                sheet.Cells[$"E{row}"].Value = prod.Description;
                sheet.Cells[$"F{row}"].Value = prod.Photo;
                sheet.Cells[$"G{row}"].Value = prod.MinPartnerPrice;
                sheet.Cells[$"H{row}"].Value = prod.Length;
                sheet.Cells[$"I{row}"].Value = prod.Width;
                sheet.Cells[$"J{row}"].Value = prod.Height;
                sheet.Cells[$"K{row}"].Value = prod.NetWeight;
                sheet.Cells[$"L{row}"].Value = prod.GrossWeight;
                sheet.Cells[$"M{row}"].Value = prod.QualityCertificate;
                sheet.Cells[$"N{row}"].Value = prod.StandardNumber;
                sheet.Cells[$"O{row}"].Value = prod.CostPrice;
                row++;
            }
        }

        private static void CreateBackupSalesSheet(ExcelPackage package, List<ProductSale> sales)
        {
            var sheet = package.Workbook.Worksheets.Add("Продажи товаров");
            sheet.Cells["B2"].Value = "Id";
            sheet.Cells["C2"].Value = "Partner Id";
            sheet.Cells["D2"].Value = "Production Id";
            sheet.Cells["E2"].Value = "Кол-во";
            sheet.Cells["F2"].Value = "Дата";
            sheet.Cells["G2"].Value = "Общая цена";

            int row = 3;
            foreach (var sale in sales)
            {
                sheet.Cells[$"B{row}"].Value = sale.Id;
                sheet.Cells[$"C{row}"].Value = sale.PartnerId;
                sheet.Cells[$"D{row}"].Value = sale.ProductionId;
                sheet.Cells[$"E{row}"].Value = sale.Count;
                sheet.Cells[$"F{row}"].Value = sale.SaleDate.ToString(); // Можно оставить так или применить форматирование
                sheet.Cells[$"G{row}"].Value = sale.TotalPrice;
                row++;
            }
        }
    }

    // Остальные классы данных - без изменений, их структура выглядит логичной.
    public class RootObject
    {
        public List<Partner> Partners { get; set; }
        public List<Production> Productions { get; set; }
        public List<ProductSale> Products { get; set; }
    }

    public class TableReport
    {
        public List<PartnerDto> partnerDtos { get; set; }
        public List<ProductSalesDto> salesDtos { get; set; }
    }
}