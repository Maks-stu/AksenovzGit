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
    /// Логика взаимодействия для PartnerRedactWindow.xaml
    /// </summary>
    public partial class PartnerRedactWindow : Window
    {
        private readonly int Id = 0;
        public PartnerRedactWindow()
        {
            InitializeComponent();
            this.Title = "Добавление партнера";
        }
        public PartnerRedactWindow(Partner partner)
        {
            InitializeComponent();
            this.Title = "Редактирование партнера";
            this.accept_btn.Content = "Редактировать";
            Id = partner.Id;
            Partner_Type_CB.Text = partner.Type;
            Partner_Name_TB.Text = partner.Name;
            Partner_Legal_Address_TB.Text = partner.LegalAddress;
            Partner_INN_TB.Text = partner.Inn;
            Partner_Director_Name_TB.Text = partner.DirectorName;
            Partner_Phone_TB.Text = partner.Phone;
            Partner_Email_TB.Text = partner.Email;
            Partner_Logo_B.Content = partner.Logo;
            Partner_Rating_TB.Text = partner.Rating.ToString();
        }
        public Partner? GetPartner()
        {
            if (!TryGetRating(out int rating))
                return null;

            Partner p = new()
            {
                Type = Partner_Type_CB.Text,
                Name = Partner_Name_TB.Text,
                LegalAddress = Partner_Legal_Address_TB.Text,
                Inn = Partner_INN_TB.Text,
                DirectorName = Partner_Director_Name_TB.Text,
                Phone = Partner_Phone_TB.Text,
                Email = Partner_Email_TB.Text,
                Logo = Partner_Logo_B.Content.ToString(),
                Rating = rating
            };

            if (Id != 0)
            {
                p.Id = Id;
            }

            return p;
        }

        private bool TryGetRating(out int rating)
        {
            if (int.TryParse(Partner_Rating_TB.Text, out rating))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Неверное значение рейтинга!");
                return false;
            }
        }

        private void accept_btn_Click(object sender, RoutedEventArgs e)
        {
            if (GetPartner() != null)
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void Partner_Logo_Choosing(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Title = "Select a File", Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*" };


            if (openFileDialog.ShowDialog() == true)
            {
                Partner_Logo_B.Content = openFileDialog.FileName;
            }
        }

        private void reject_btn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
