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
            bool answer = true;
            if (!IsArticleValid(Production_Article_TB.Text, out string Article_error))
            {
                MessageBox.Show(Article_error);
                return false;
            }
            if (!int.TryParse(Production_Length_TB.Text, out _))
            {
                MessageBox.Show("Длинна должна быть целым числом или нулем!");
                return false;
            }
            if (!int.TryParse(Production_Width_TB.Text, out _))
            {
                MessageBox.Show("Ширина должна быть целым числом или нулем!");
                return false;
            }
            if (!int.TryParse(Production_Height_TB.Text, out _))
            {
                MessageBox.Show("Высота должна быть целым числом или нулем!");
                return false;
            }
            if (!double.TryParse(Production_Net_Weight_TB.Text, out _))
            {
                MessageBox.Show("Масса нетто должна быть в формате числа через запятую!");
                return false;
            }
            if (!double.TryParse(Production_Gross_Weight_TB.Text, out _))
            {
                MessageBox.Show("Масса брутто должна быть в формате числа через запятую!");
                return false;
            }
            if (!double.TryParse(Production_Min_Partner_Price_TB.Text, out double min))
            {
                MessageBox.Show("Минимальная цена должна быть в формате числа через запятую!");
                return false;
            }
            if (!double.TryParse(Production_Cost_Price_TB.Text, out double cost))
            {
                MessageBox.Show("Цена должна быть в формате числа через запятую!");
                return false;
            }
            if (min > cost)
            {
                MessageBox.Show("Цена должна быть не меньше минимальной!");
                return false;
            }
            if (Production_Standard_Number_TB.Text == "")
            {
                MessageBox.Show("Введите номер ГОСТа");
                return false;
            }
            if (Production_Quality_Certificate_TB.Text == "")
            {
                MessageBox.Show("Введите номер сертификата качества");
                return false;
            }
            if (!Production_Quality_Certificate_TB.Text.All(char.IsDigit))
            {
                MessageBox.Show("Сертификат должен состоять только из цифр");
                return false;
            }
            return answer;
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

        public static bool IsArticleValid(string article, out string error)
        {
            error = "";
            bool answer = true;
            if (article.Any(char.IsWhiteSpace))
            {
                error = "Артикул не должен содержать пробелы";
                answer = false;
            }
            if (article.Length < 6)
            {
                if (error.Length == 0)
                    error = "Артикул должен быть не короче 6 символов";
                else
                    error += " и должен быть не короче 6 символов";
                answer = false;
            }
            if (article.StartsWith("0"))
            {
                if (error.Length == 0)
                    error = "Артикул не должен начинаться с цифры '0'";
                else
                    error += " и не должен начинаться с цифры '0'";
                answer = false;
            }
            if (!Regex.IsMatch(article, @"^[A-Z0-9\-]+$"))
            {
                if (error.Length == 0)
                    error = "Артикул содержит недопустимые символы. Используйте только заглавные английские буквы, цифры и дефисы";
                else
                    error += " и содержит недопустимые символы. Используйте только заглавные английские буквы, цифры и дефисы";
                answer = false;
            }
            return answer;
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
