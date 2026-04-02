using System;
using System.Collections.Generic;
using System.IO;
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
using ClassLibrary.Classes;
using Microsoft.Win32;

namespace MasterFloor.DataForms
{
    /// <summary>
    /// Логика взаимодействия для ProductionRedactWindow.xaml
    /// </summary>
    public partial class ProductionRedactWindow : Window
    {
        private readonly int Id = 0;
        public ProductionRedactWindow()
        {
            InitializeComponent();
            this.Title = "Добавление продукции";
        }
        public ProductionRedactWindow(Production production)
        {
            InitializeComponent();
            this.Title = "Редактирование продукции";
            this.accept_btn.Content = "Редактировать";
            Id = production.Id;
            Production_Article_TB.Text = production.Article;
            Production_Name_TB.Text = production.Name;
            Production_Description_TB.Text = production.Description;
            Production_Photo_B.Content = production.Photo;
            Production_Min_Partner_Price_TB.Text = production.MinPartnerPrice.ToString();
            Production_Length_TB.Text = production.Length.ToString();
            Production_Width_TB.Text = production.Width.ToString();
            Production_Height_TB.Text = production.Height.ToString();
            Production_Net_Weight_TB.Text = production.NetWeight.ToString();
            Production_Gross_Weight_TB.Text = production.GrossWeight.ToString();
            Production_Quality_Certificate_TB.Text = production.QualityCertificate.ToString();
            Production_Standard_Number_TB.Text = production.StandardNumber.ToString();
            Production_Cost_Price_TB.Text= production.CostPrice.ToString();
        }

        private bool ValidateInputs()
        {
            if (!IsArticleValid(Production_Article_TB.Text))
            {
                MessageBox.Show("Неверный артикль!");
                return false;
            }
            if (!int.TryParse(Production_Length_TB.Text, out _))
            {
                MessageBox.Show("Неверное значение длинны!");
                return false;
            }
            if (!int.TryParse(Production_Width_TB.Text, out _))
            {
                MessageBox.Show("Неверное значение ширины!");
                return false;
            }
            if (!int.TryParse(Production_Height_TB.Text, out _))
            {
                MessageBox.Show("Неверное значение высоты!");
                return false;
            }
            if (!double.TryParse(Production_Net_Weight_TB.Text, out _))
            {
                MessageBox.Show("Неверное значение массы нетто!");
                return false;
            }
            if (!double.TryParse(Production_Gross_Weight_TB.Text, out _))
            {
                MessageBox.Show("Неверное значение массы брутто!");
                return false;
            }
            if (!double.TryParse(Production_Min_Partner_Price_TB.Text, out _))
            {
                MessageBox.Show("Неверное значение мин. цены!");
                return false;
            }
            if (!double.TryParse(Production_Cost_Price_TB.Text, out _))
            {
                MessageBox.Show("Неверное значение цены!");
                return false;
            }
            return true;
        }
        public Production? GetProduction()
        {
            if (ValidateInputs())
            {
                Production p = new()
                {
                    Article = Production_Article_TB.Text,
                    Name = Production_Name_TB.Text,
                    Description = Production_Description_TB.Text,
                    Photo = Production_Photo_B.Content.ToString(),
                    MinPartnerPrice = Convert.ToDouble(Production_Min_Partner_Price_TB.Text),
                    Length = Convert.ToInt32(Production_Length_TB.Text),
                    Width = Convert.ToInt32(Production_Width_TB.Text),
                    Height = Convert.ToInt32(Production_Height_TB.Text),
                    NetWeight = Convert.ToDouble(Production_Net_Weight_TB.Text),
                    GrossWeight = Convert.ToDouble(Production_Gross_Weight_TB.Text),
                    QualityCertificate = Production_Quality_Certificate_TB.Text,
                    StandardNumber = Production_Standard_Number_TB.Text,
                    CostPrice = Convert.ToDouble(Production_Cost_Price_TB.Text)
                };
                if (Id != 0)
                {
                    p.Id = Id;
                }
                return p;
            }
            return null;
        }

        public static bool IsArticleValid(string article)
        {
            string trimmed = article.Replace(" ", "");

            if (trimmed.Length < 6)
            {
                MessageBox.Show("Артикул должен быть не короче 6 символов.");
                return false;
            }
            if (trimmed.StartsWith("0"))
            {
                MessageBox.Show("Артикул не должен начинаться с цифры '0'.");
                return false;
            }
            if (!Regex.IsMatch(trimmed, @"^[A-Z0-9\-]+$"))
            {
                MessageBox.Show("Артикул содержит недопустимые символы. Используйте только заглавные буквы, цифры и дефисы.");
                return false;
            }

            return true;
        }

        private void accept_btn_Click(object sender, RoutedEventArgs e)
        {
            if (GetProduction() != null)
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void Production_Logo_Choosing(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Title = "Select a File", Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*" };


            if (openFileDialog.ShowDialog() == true)
            {
                Production_Photo_B.Content = openFileDialog.FileName;
            }
        }

        private void reject_btn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
