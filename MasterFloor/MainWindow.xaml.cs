using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Application.Services.Services;
using ClassLibrary.Classes;
using ClassLibrary.DTOs;
using MasterFloor.DataForms;
using Microsoft.Win32;
using OfficeOpenXml;

namespace MasterFloor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly PartnerService partnerService = new();
        public readonly ProductionService productionService = new();
        public readonly ProductSaleService productSaleService = new();
        public List<Partner> partners = [];
        public List<Production> productions = [];
        public List<ProductSale> productSales = [];
        public List<PartnerDto> partnerDtos = [];
        public List<ProductSalesDto> psti = [];
        public MainWindow()
        {
            InitializeComponent();
            ReWrite();
        }

        private void ReWrite()
        {
            Partners_LB.Items.Clear();
            ProductSales_Table.Items.Clear();

            partners = partnerService.GetPartnersAll();
            productions = productionService.GetProductionsAll();
            productSales = productSaleService.GetProductSalesAll();

            partnerDtos.Clear();
            psti.Clear();

            foreach (Partner partner in partners)
            {
                int Discount = partner.GetDiscount(productSales);
                partnerDtos.Add(new PartnerDto
                {
                    Id = partner.Id,
                    Type = partner.Type,
                    Name = partner.Name,
                    Discount = Discount,
                    DirectorName = partner.DirectorName,
                    Phone = partner.Phone,
                    Rating = partner.Rating
                });
            }

            foreach (PartnerDto partnerDto in partnerDtos)
            {
                Partners_LB.Items.Add(partnerDto);
            }
        }

        private void ReWrite(Partner partner)
        {
            ProductSales_Table.Items.Clear();
            psti.Clear();
            foreach (ProductSale pstii in productSaleService.GetProductSalesAll().FindAll(x => x.PartnerId == partner.Id))
            {
                DateOnly date = pstii.SaleDate;
                string dateStr = date.ToString("d MMMM yyyy 'г.'", new System.Globalization.CultureInfo("ru-RU"));
                psti.Add(new ProductSalesDto
                {
                    Id = pstii.Id,
                    Date = dateStr,
                    Count = pstii.Count,
                    Production = productionService.GetProductionById(pstii.ProductionId).Name,
                    TotalPrice = pstii.TotalPrice.GetValueOrDefault()
                });
            }

            string filterText = Source_TB.Text.ToLower();
            List<ProductSalesDto> psti_filter = [];
            if (!string.IsNullOrEmpty(filterText))
            {
                psti_filter = [.. psti.Where(tetii =>
                    tetii.Id.ToString().Contains(filterText, StringComparison.CurrentCultureIgnoreCase) ||
                    tetii.Date.ToString().Contains(filterText, StringComparison.CurrentCultureIgnoreCase) ||
                    tetii.Count.ToString().Contains(filterText, StringComparison.CurrentCultureIgnoreCase) ||
                    tetii.Production.ToString().Contains(filterText, StringComparison.CurrentCultureIgnoreCase) ||
                    tetii.TotalPrice.ToString().Contains(filterText, StringComparison.CurrentCultureIgnoreCase)
                )];
            }
            else
            {
                psti_filter = psti;
            }
            psti = psti_filter;
            ProductSales_Table.Items.Clear();
            foreach (ProductSalesDto productSalesDto in psti)
            {
                ProductSales_Table.Items.Add(productSalesDto);
            }
        }

        private void Source_TB_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (Partners_LB.SelectedItem != null)
            {
                ReWrite(partnerService.GetPartnerById(partnerDtos[Partners_LB.SelectedIndex].Id));
            }
        }

        private void Partners_LB_Selected(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Partners_LB.SelectedItem != null)
            {
                ReWrite(partnerService.GetPartnerById(partnerDtos[Partners_LB.SelectedIndex].Id));
            }
        }

        private void AddPartner(object sender, RoutedEventArgs e)
        {
            PartnerRedactWindow Windows = new();
            if (Windows.ShowDialog() == true)
            {
                Partner? p = Windows.GetPartner();
                if (p != null)
                {
                    partnerService.CreatePartner(p);
                    ReWrite();
                }
            }
        }

        private void AddProduction(object sender, RoutedEventArgs e)
        {
            ProductionRedactWindow Windows = new();
            if (Windows.ShowDialog() == true)
            {
                Production? p = Windows.GetProduction();
                if (p != null)
                {
                    productionService.CreateProduction(p);
                    ReWrite();
                }
            }
        }

        private void AddProductSaleSelected(object sender, RoutedEventArgs e)
        {
            int selectedPartner = partners.FindIndex(x => x.Id == partnerDtos[Partners_LB.SelectedIndex].Id);
            if (selectedPartner != -1)
            {
                ProductSaleRedactWindow Windows = new(selectedPartner, partners, productions, productSales);
                if (Windows.ShowDialog() == true)
                {
                    ProductSale? p = Windows.GetProductSale();
                    if (p != null)
                    {
                        productSaleService.CreateProductSale(p);
                        ReWrite();
                    }
                }
            }
        }

        private void AddProductSale(object sender, RoutedEventArgs e)
        {
            ProductSaleRedactWindow Windows = new(partners, productions, productSales);
            if (Windows.ShowDialog() == true)
            {
                ProductSale? p = Windows.GetProductSale();
                if (p != null)
                {
                    productSaleService.CreateProductSale(p);
                    ReWrite();
                }
            }
        }

        private void RedactPartner(object sender, RoutedEventArgs e)
        {
            Partner selectedPartner = partners.Find(x => x.Id == partnerDtos[Partners_LB.SelectedIndex].Id);
            if (selectedPartner != null)
            {
                PartnerRedactWindow Windows = new(selectedPartner);
                if (Windows.ShowDialog() == true)
                {
                    Partner p = Windows.GetPartner();
                    if (p != null)
                    {
                        partnerService.UpdatePartner(p.Id, p);
                        ReWrite();
                    }
                }
            }
        }

        private void RedactProduction(object sender, RoutedEventArgs e)
        {
            ChoiceWindow IndexWindows = new(productions);
            if (IndexWindows.ShowDialog() == true)
            {
                int i = IndexWindows.GetIndex();
                if (i != -1)
                {
                    Production selectedProduction = productions[i];
                    if (selectedProduction != null)
                    {
                        ProductionRedactWindow Windows = new(selectedProduction);
                        if (Windows.ShowDialog() == true)
                        {
                            Production p = Windows.GetProduction();
                            if (p != null)
                            {
                                productionService.UpdateProduction(p.Id, p);
                                ReWrite();
                            }
                        }
                    }
                }
            }
        }

        private void RedactProductSale(object sender, RoutedEventArgs e)
        {
            ProductSale selectedProductSale = productSales.Find(x => x.Id == psti[ProductSales_Table.SelectedIndex].Id);
            if (selectedProductSale != null)
            {
                ProductSaleRedactWindow Windows = new(selectedProductSale, partners, productions, productSales);
                if (Windows.ShowDialog() == true)
                {
                    ProductSale p = Windows.GetProductSale();
                    if (p != null)
                    {
                        productSaleService.UpdateProductSale(p.Id, p);
                        ReWrite();
                    }
                }
            }
        }

        private void DeletePartner(object sender, RoutedEventArgs e)
        {
            Partner selectedPartner = partners.Find(x => x.Id == partnerDtos[Partners_LB.SelectedIndex].Id);
            if (selectedPartner != null)
            {
                if (MessageBox.Show($"Вы действительно хотите удалить партнера {selectedPartner.Name}?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    partnerService.DeletePartner(selectedPartner.Id);
                    ReWrite();
                }
            }
        }

        private void DeleteProduction(object sender, RoutedEventArgs e)
        {
            ChoiceWindow IndexWindows = new(productions);
            if (IndexWindows.ShowDialog() == true)
            {
                int i = (int)IndexWindows.GetIndex();
                if (i != -1)
                {
                    Production selectedProduction = productions[i];
                    if (selectedProduction != null)
                    {
                        if (MessageBox.Show($"Вы действительно хотите удалить продукт {selectedProduction.Name}?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            productionService.DeleteProduction(selectedProduction.Id);
                            ReWrite();
                        }
                    }
                }
            }
        }

        private void DeleteProductSale(object sender, RoutedEventArgs e)
        {
            ProductSale selectedProductSale = productSales.Find(x => x.Id == psti[ProductSales_Table.SelectedIndex].Id);
            Partner selectedPartner = partners.Find(x => x.Id == selectedProductSale.PartnerId);
            Production selectedProduction = productions.Find(x => x.Id == selectedProductSale.ProductionId);
            if (selectedProductSale != null)
            {
                if (MessageBox.Show($"Вы действительно хотите удалить заказ {selectedPartner.Name} на {selectedProduction.Name}?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    productSaleService.DeleteProductSale(selectedProductSale.Id);
                    ReWrite();
                }
            }
        }

        private void ToExcel(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && int.TryParse(menuItem.CommandParameter.ToString(), out int param))
            {
                if (param == 2)
                {
                    if (Partners_LB.SelectedIndex == -1)
                    {
                        MessageBox.Show("Рассматриваемый партнер отстутствует");
                        return;
                    }
                    if (ProductSales_Table.Items.Count == 0)
                    {
                        MessageBox.Show("Рассматриваемый партнер не имеет истории покупок");
                        return;
                    }
                }
                if (param == 0)
                {
                    if (ProductSales_Table.Items.Count == 0)
                    {
                        if (Partners_LB.SelectedIndex == -1)
                            MessageBox.Show("Рассматриваемый партнер отстутствует, и следовательно не имеет истории покупок", "Предупреждение");
                        else
                            MessageBox.Show("Рассматриваемый партнер не имеет истории покупок", "Предупреждение");
                    }
                }
                List<PartnerDto> partnerDtos = [];
                foreach (PartnerDto partnerDto in Partners_LB.Items)
                    partnerDtos.Add(partnerDto);
                List<ProductSalesDto> salesDtos = [];
                foreach (ProductSalesDto salesDto in ProductSales_Table.Items)
                    salesDtos.Add(salesDto);
                TableReport ExcelReport = new()
                {
                    partnerDtos = partnerDtos,
                    salesDtos = salesDtos
                };
                var reportExcel = MarketExcelGenerator.Generate(ExcelReport, param);

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel файлы (*.xlsx)|*.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        using var fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
                        using var writer = new BinaryWriter(fs);
                        writer.Write(reportExcel);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
            }
            ReWrite();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            // Создаем список партнеров, производств и продаж
            var partners = this.partners; // предполагаем, что эти списки уже заполнены
            var productions = this.productions;
            var productSales = this.productSales;

            // Создаем объект, содержащий все данные
            var dataObject = new
            {
                Partners = partners,
                Productions = productions,
                ProductSales = productSales
            };

            JsonSerializerOptions jsonSerializerOptions = new()
            {
                WriteIndented = true
            };
            var options = jsonSerializerOptions;

            string jsonString = JsonSerializer.Serialize(dataObject, options);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Binary files (*.bin)|*.bin"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using var fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
                    using var writer = new BinaryWriter(fs);
                    writer.Write(jsonBytes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            ReWrite();
        }

        private void ExcelSave(object sender, RoutedEventArgs e)
        {
            var reportExcel = MarketExcelGenerator.GenerateBackUp(new RootObject
            {
                Partners = this.partners,
                Productions = this.productions,
                Products = this.productSales
            });

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel файлы (*.xlsx)|*.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using var fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
                    using var writer = new BinaryWriter(fs);
                    writer.Write(reportExcel);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            ReWrite();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            // Инициализация списков
            List<Partner> newPartners = [];
            List<Production> newProductions = [];
            List<ProductSale> newProductSales = [];

            var openFileDialog = new OpenFileDialog
            {
                Filter = "Binary files (*.bin)|*.bin"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using var fs = new FileStream(openFileDialog.FileName, FileMode.Open);
                    using var reader = new BinaryReader(fs);
                    byte[] jsonBytes = new byte[fs.Length];
                    fs.Read(jsonBytes, 0, jsonBytes.Length);
                    string jsonString = Encoding.UTF8.GetString(jsonBytes);

                    JsonSerializerOptions jsonSerializerOptions1 = new()
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    JsonSerializerOptions jsonSerializerOptions = jsonSerializerOptions1;

                    RootObject dataObject = JsonSerializer.Deserialize<RootObject>(jsonString, jsonSerializerOptions);

                    if (dataObject != null)
                    {
                        Console.WriteLine("Объект успешно восстановлен!");

                        newPartners = dataObject.Partners ?? [];
                        newProductions = dataObject.Productions ?? [];
                        newProductSales = dataObject.Products ?? [];
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            // Обновление и добавление партнеров
            foreach (var partner in newPartners)
            {
                var existingPartner = partners.Find(x => x.Id == partner.Id);
                if (existingPartner != null)
                {
                    // Полное обновление существующего партнёра
                    partnerService.UpdatePartner(partner.Id, partner);
                }
                else
                {
                    // Создаём нового партнёра
                    partnerService.CreatePartner(partner);
                }
            }

            // Обновление и добавление производств
            foreach (var production in newProductions)
            {
                var existingProduction = productions.Find(x => x.Id == production.Id);
                if (existingProduction != null)
                {
                    // Полное обновление производства
                    productionService.UpdateProduction(production.Id, production);
                }
                else
                {
                    // Создаём новое производство
                    productionService.CreateProduction(production);
                }
            }

            // Обновление и добавление продаж
            foreach (var sale in newProductSales)
            {
                var existingSale = productSales.Find(x => x.Id == sale.Id);
                if (existingSale != null)
                {
                    // Полное обновление продажи
                    productSaleService.UpdateProductSale(sale.Id, sale);
                }
                else
                {
                    // Создаём новую продажу
                    productSaleService.CreateProductSale(sale);
                }
            }
        }

        public static RootObject LoadFromExcel(string filePath)
        {
            var result = new RootObject();
            ExcelPackage.License.SetNonCommercialPersonal("My Name");
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var partnerSheet = package.Workbook.Worksheets["Партнеры"];
                result.Partners = [];
                int row = 3;
                while (true)
                {
                    var idCell = partnerSheet.Cells[$"B{row}"].Value;
                    if (idCell == null) break;

                    Partner partner = new()
                    {
                        Id = Convert.ToInt32(idCell),
                        Type = Convert.ToString(partnerSheet.Cells[$"C{row}"].Value),
                        Name = Convert.ToString(partnerSheet.Cells[$"D{row}"].Value),
                        LegalAddress = Convert.ToString(partnerSheet.Cells[$"E{row}"].Value),
                        Inn = Convert.ToString(partnerSheet.Cells[$"F{row}"].Value),
                        DirectorName = Convert.ToString(partnerSheet.Cells[$"G{row}"].Value),
                        Phone = Convert.ToString(partnerSheet.Cells[$"H{row}"].Value),
                        Email = Convert.ToString(partnerSheet.Cells[$"I{row}"].Value),
                        Logo = Convert.ToString(partnerSheet.Cells[$"J{row}"].Value),
                        Rating = Convert.ToInt32(partnerSheet.Cells[$"K{row}"].Value)
                    };
                    result.Partners.Add(partner);
                    row++;
                }
                var productionSheet = package.Workbook.Worksheets["Производство"];
                result.Productions = [];
                row = 3;
                while (true)
                {
                    var idCell = productionSheet.Cells[$"B{row}"].Value;
                    if (idCell == null) break;

                    Production production = new()
                    {
                        Id = Convert.ToInt32(idCell),
                        Article = Convert.ToString(productionSheet.Cells[$"C{row}"].Value),
                        Name = Convert.ToString(productionSheet.Cells[$"D{row}"].Value),
                        Description = Convert.ToString(productionSheet.Cells[$"E{row}"].Value),
                        Photo = Convert.ToString(productionSheet.Cells[$"F{row}"].Value),
                        MinPartnerPrice = Convert.ToInt32(productionSheet.Cells[$"G{row}"].Value),
                        Length = Convert.ToInt32(productionSheet.Cells[$"H{row}"].Value),
                        Width = Convert.ToInt32(productionSheet.Cells[$"I{row}"].Value),
                        Height = Convert.ToInt32(productionSheet.Cells[$"J{row}"].Value),
                        NetWeight = Convert.ToDouble(productionSheet.Cells[$"K{row}"].Value),
                        GrossWeight = Convert.ToDouble(productionSheet.Cells[$"L{row}"].Value),
                        QualityCertificate = Convert.ToString(productionSheet.Cells[$"M{row}"].Value),
                        StandardNumber = Convert.ToString(productionSheet.Cells[$"N{row}"].Value),
                        CostPrice = Convert.ToDouble(productionSheet.Cells[$"O{row}"].Value)
                    };
                    result.Productions.Add(production);
                    row++;
                }

                var productSaleSheet = package.Workbook.Worksheets["Продажи товаров"];
                result.Products = [];
                row = 3;
                while (true)
                {
                    var idCell = productSaleSheet.Cells[$"B{row}"].Value;
                    if (idCell == null) break;
                    var dateCellValue = productSaleSheet.Cells[$"C{row}"].Value;
                    ProductSale productSale = new()
                    {
                        Id = Convert.ToInt32(idCell),
                        PartnerId = Convert.ToInt32(productSaleSheet.Cells[$"C{row}"].Value),
                        ProductionId = Convert.ToInt32(productSaleSheet.Cells[$"D{row}"].Value),
                        Count = Convert.ToInt32(productSaleSheet.Cells[$"E{row}"].Value),
                        SaleDate = DateOnly.FromDateTime(DateTime.FromOADate((double)dateCellValue)),
                        TotalPrice = Convert.ToDouble(productSaleSheet.Cells[$"G{row}"].Value)
                    };
                    result.Products.Add(productSale);
                    row++;
                }
            }

            return result;
        }

        private void ExcelLoad(object sender, RoutedEventArgs e)
        {
            List<Partner> newpartners = [];
            List<Production> newproductions = [];
            List<ProductSale> newproductSales = [];
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Excel файлы (*.xlsx)|*.xlsx"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    RootObject dataObject = LoadFromExcel(openFileDialog.FileName);
                    newpartners = dataObject.Partners ?? [];
                    newproductions = dataObject.Productions ?? [];
                    newproductSales = dataObject.Products ?? [];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            // Обновление или создание партнеров
            foreach (Partner partner in newpartners)
            {
                var existingPartner = partners.FirstOrDefault(x => x.Id == partner.Id);
                if (existingPartner != null)
                {
                    // Обновление существующего
                    partnerService.UpdatePartner(partner.Id, partner);
                }
                else
                {
                    // Создание нового
                    partnerService.CreatePartner(partner);
                }
            }

            // Обновление или создание производств
            foreach (Production production in newproductions)
            {
                var existingProduction = productions.FirstOrDefault(x => x.Id == production.Id);
                if (existingProduction != null)
                {
                    productionService.UpdateProduction(production.Id, production);
                }
                else
                {
                    productionService.CreateProduction(production);
                }
            }

            // Обновление или создание продаж
            foreach (ProductSale productSale in newproductSales)
            {
                var existingProductSale = productSales.FirstOrDefault(x => x.Id == productSale.Id);
                if (existingProductSale != null)
                {
                    productSaleService.UpdateProductSale(productSale.Id, productSale);
                }
                else
                {
                    productSaleService.CreateProductSale(productSale);
                }
            }

            ReWrite();
        }

        private void ListProduction(object sender, RoutedEventArgs e)
        {
            ProductionListWindow Windows = new();
            if (Windows.ShowDialog() == true)
            {
                ReWrite();
            }
        }
    }
}