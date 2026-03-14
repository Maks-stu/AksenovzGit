using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Application.Services.Services;
using ClassLibrary.Classes;
using ClassLibrary.DTOs;

namespace MasterFloor.DataForms
{
    public partial class ProductSaleRedactWindow : Window
    {
        private readonly List<Partner> partners;
        private readonly List<Production> productions;
        private readonly List<ProductSale> productSales;
        private readonly int Id = 0;
        public ProductSaleRedactWindow(List<Partner> partners, List<Production> productions, List<ProductSale> productSales)
        {
            InitializeComponent();
            this.Title = "Добавление заказа";
            this.partners = partners;
            this.productions = productions;
            this.productSales = productSales;
            foreach (Partner p in partners)
            {
                ProductSale_Partner_CB.Items.Add(p.Name);
            }
            foreach (Production p in productions)
            {
                ProductSale_Production_CB.Items.Add(p.Name);
            }
        }
        public ProductSaleRedactWindow(int partnerindex, List<Partner> partners, List<Production> productions, List<ProductSale> productSales)
        {
            InitializeComponent();
            this.Title = "Добавление заказа";
            this.partners = partners;
            this.productions = productions;
            this.productSales = productSales;
            foreach (Partner p in partners)
            {
                ProductSale_Partner_CB.Items.Add(p.Name);
            }
            foreach (Production p in productions)
            {
                ProductSale_Production_CB.Items.Add(p.Name);
            }
            ProductSale_Partner_CB.SelectedIndex = partnerindex;
        }
        public ProductSaleRedactWindow(ProductSale productSale, List<Partner> partners, List<Production> productions, List<ProductSale> productSales)
        {
            InitializeComponent();
            this.Title = "Редактирование заказа";
            this.accept_btn.Content = "Редактировать";
            this.partners = partners;
            this.productions = productions;
            this.productSales = productSales;
            foreach (Partner p in partners)
            {
                ProductSale_Partner_CB.Items.Add(p.Name);
            }
            foreach (Production p in productions)
            {
                ProductSale_Production_CB.Items.Add(p.Name);
            }
            Id = productSale.Id;
            ProductSale_Count_TB.Text = productSale.Count.ToString();
            ProductSale_Date_DP.SelectedDate = productSale.SaleDate.ToDateTime(TimeOnly.Parse("10:00 PM"));
            ProductSale_Partner_CB.SelectedIndex = partners.FindIndex(x => x.Id == productSale.PartnerId);
            ProductSale_Production_CB.SelectedIndex = productions.FindIndex(x => x.Id == productSale.ProductionId);
        }
        private bool ValidateInputs()
        {
            Partner partner = partners[ProductSale_Partner_CB.SelectedIndex];
            Production production = productions[ProductSale_Production_CB.SelectedIndex];

            if (partner == null)
            {
                MessageBox.Show("Данный учитель(id: " + partner?.Id + ") не найден!");
                return false;
            }

            if (production == null)
            {
                MessageBox.Show("Данная работа(id: " + production?.Id + ") не найдена!");
                return false;
            }

            if (!int.TryParse(ProductSale_Count_TB.Text, out _))
            {
                MessageBox.Show("Неправильное количество продукции!");
                return false;
            }

            if (ProductSale_Date_DP.SelectedDate == null)
            {
                MessageBox.Show("Неправильная дата!");
                return false;
            }

            return true;
        }

        public ProductSale? GetProductSale()
        {
            if (!ValidateInputs())
                return null;

            Partner partner = partners[ProductSale_Partner_CB.SelectedIndex];
            Production production = productions[ProductSale_Production_CB.SelectedIndex];

            int count = int.Parse(ProductSale_Count_TB.Text);

            double TotalPrice;
            if (production.CostPrice * partner.GetDiscount(productSales) < production.MinPartnerPrice)
                TotalPrice = count * production.MinPartnerPrice;
            else
                TotalPrice = count * production.CostPrice.GetValueOrDefault() * partner.GetDiscount(productSales);
            
            #pragma warning disable CS8629 // Тип значения, допускающего NULL, может быть NULL.
            DateTime selectedDate = (DateTime)ProductSale_Date_DP.SelectedDate;
            #pragma warning restore CS8629 // Тип значения, допускающего NULL, может быть NULL.

            ProductSale p = new()
            {
                PartnerId = partner.Id,
                ProductionId = production.Id,
                Count = count,
                SaleDate = DateOnly.FromDateTime(selectedDate),
                TotalPrice = TotalPrice
            };
            if (Id != 0)
                p.Id = Id;
            return p;
        }

        private void accept_btn_Click(object sender, RoutedEventArgs e)
        {
            if (GetProductSale() != null)
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void reject_btn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}