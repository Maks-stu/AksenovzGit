using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Application.Services.Interfaces;
using Application.Services.Services;
using ClassLibrary.Classes;
using ClassLibrary.DTOs;

namespace MasterFloor.DataForms
{
    /// <summary>
    /// Логика взаимодействия для ProductionListWindow.xaml
    /// </summary>
    public partial class ProductionListWindow : Window
    {
        List<ProductionDTO> pti = [];
        public readonly ProductionService productionService = new();
        public ProductionListWindow()
        {
            InitializeComponent();
            ReWrite();
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
                }
            }
        }

        private void RedactProduction(object sender, RoutedEventArgs e)
        {
            Production selectedProduction = productionService.GetProductionsAll().Find(x => x.Id == pti[Production_Table.SelectedIndex].Id);
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

        private void DeleteProduction(object sender, RoutedEventArgs e)
        {
            Production selectedProduction = productionService.GetProductionsAll().Find(x => x.Id == pti[Production_Table.SelectedIndex].Id);
            if (selectedProduction != null)
            {
                if (MessageBox.Show($"Вы действительно хотите удалить партнера {selectedProduction.Name}?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    productionService.DeleteProduction(selectedProduction.Id);
                    ReWrite();
                }
            }
        }

        private void Source_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReWrite();
        }

        private void ReWrite()
        {
            Production_Table.Items.Clear();
            pti.Clear();

            foreach (Production production in productionService.GetProductionsAll())
            {
                pti.Add(new ProductionDTO
                {
                    Id = production.Id,
                    Article = production.Article,
                    Name = production.Name,
                    Description = production.Description,
                    CostPrice = production.CostPrice.ToString(),
                    Dimensions = $"{production.Length}^{production.Width}^{production.Height}",
                    Weight = $"{production.NetWeight} || {production.GrossWeight}"
                });
            }

            string filterText = Source_TB.Text.ToLower();
            List<ProductionDTO> pti_filter = [];
            if (!string.IsNullOrEmpty(filterText))
            {
                pti_filter = [.. pti.Where(ptii =>
                    ptii.Id.ToString().Contains(filterText, StringComparison.CurrentCultureIgnoreCase) ||
                    ptii.Article.ToString().Contains(filterText, StringComparison.CurrentCultureIgnoreCase) ||
                    ptii.Name.ToString().Contains(filterText, StringComparison.CurrentCultureIgnoreCase) ||
                    ptii.Description.ToString().Contains(filterText, StringComparison.CurrentCultureIgnoreCase) ||
                    ptii.CostPrice.ToString().Contains(filterText, StringComparison.CurrentCultureIgnoreCase) ||
                    ptii.Dimensions.ToString().Contains(filterText, StringComparison.CurrentCultureIgnoreCase) ||
                    ptii.Weight.ToString().Contains(filterText, StringComparison.CurrentCultureIgnoreCase)
                )];
            }
            else
            {
                pti_filter = pti;
            }
            pti = pti_filter;
            Production_Table.Items.Clear();
            foreach (ProductionDTO productionDTO in pti)
            {
                Production_Table.Items.Add(productionDTO);
            }
        }
    }
}
