using System.Text.RegularExpressions;
using System.Windows;
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
        readonly List<string> types = [ "ИП", "КФЛ", "ХТ", "ХО", "ООО", "АО/НАО", "ПАО", "ХП", "УП" ];
        public PartnerRedactWindow()
        {
            InitializeComponent();
            this.Title = "Добавление партнера";
            Partner_Type_CB.ItemsSource = types;
        }
        public PartnerRedactWindow(Partner partner)
        {
            InitializeComponent();
            this.Title = "Редактирование партнера";
            this.accept_btn.Content = "Редактировать";
            Partner_Type_CB.ItemsSource = types;
            Id = partner.Id;
            if (types.Exists(t => t == partner.Type))
            {
                Partner_Type_CB.SelectedIndex = types.FindIndex(t => t == partner.Type);
            }
            else
            {
                Partner_Type_CB.SelectedIndex = 0;
            }
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
            if (!TryGetRating())
                return null;

            Partner p = new()
            {
                Type = Partner_Type_CB.SelectedValue.ToString(),
                Name = Partner_Name_TB.Text,
                LegalAddress = Partner_Legal_Address_TB.Text,
                Inn = Partner_INN_TB.Text,
                DirectorName = Partner_Director_Name_TB.Text,
                Phone = Partner_Phone_TB.Text,
                Email = Partner_Email_TB.Text,
                Logo = Partner_Logo_B.Content.ToString(),
                Rating = Convert.ToInt32(Partner_Rating_TB.Text)
            };

            if (Id != 0)
            {
                p.Id = Id;
            }

            return p;
        }

        private bool TryGetRating()
        {
            if (!int.TryParse(Partner_Rating_TB.Text, out _))
            {
                MessageBox.Show("Неверное значение рейтинга!");
                return false;
            }
            if (!IsEmailValid(Partner_Phone_TB.Text))
            {
                MessageBox.Show("Неправильная Электронная почта!");
                return false;
            }
            if (!IsPhoneNumberValid(Partner_Phone_TB.Text))
            {
                MessageBox.Show("Неправильный номер телефона!");
                return false;
            }
            if (IsValidINN(Partner_Email_TB.Text))
            {
                MessageBox.Show("Неправильный ИНН!");
                return false;
            }
            if (Partner_Type_CB.SelectedIndex != -1)
            {
                MessageBox.Show("Выберите тип партнера!");
                return false;
            }
            return true;
        }

        private void Partner_Logo_Choosing(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Title = "Select a File", Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*" };


            if (openFileDialog.ShowDialog() == true)
            {
                Partner_Logo_B.Content = openFileDialog.FileName;
            }
        }

        public static bool IsValidINN(string inn)
        {
            if (string.IsNullOrWhiteSpace(inn))
                return false;

            inn = inn.Trim();

            if (!(inn.Length == 10 || inn.Length == 12))
                return false;

            // Проверка, чтобы ИНН состоял только из цифр
            if (!inn.All(char.IsDigit))
                return false;

            if (inn.Length == 10)
                return CheckINN10(inn);
            else
                return CheckINN12(inn);
        }

        private static bool CheckINN10(string inn)
        {
            int[] coefficients = { 2, 4, 10, 3, 5, 9, 4, 6, 8 };
            int sum = 0;

            for (int i = 0; i < 9; i++)
                sum += (inn[i] - '0') * coefficients[i];

            int checkDigit = (sum % 11) % 10;

            return checkDigit == (inn[9] - '0');
        }

        private static bool CheckINN12(string inn)
        {
            int[] coefficients1 = { 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };
            int[] coefficients2 = { 3, 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };

            int sum1 = 0;
            for (int i = 0; i < 10; i++)
                sum1 += (inn[i] - '0') * coefficients1[i];

            int checkDigit1 = (sum1 % 11) % 10;

            int sum2 = 0;
            for (int i = 0; i < 11; i++)
                sum2 += (inn[i] - '0') * coefficients2[i];

            int checkDigit2 = (sum2 % 11) % 10;

            return checkDigit1 == (inn[10] - '0') && checkDigit2 == (inn[11] - '0');
        }

        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            // Регулярное выражение для проверки номера (например, для России)
            string pattern = @"^(\+7|7|8)?\d{10}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        public static bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Простое регулярное выражение для проверки формата email
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private void accept_btn_Click(object sender, RoutedEventArgs e)
        {
            if (GetPartner() != null)
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

        private void FIO_TB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Partner_Director_Name_TB.Text == "(Фамилия + инициалы)")
            {
                Partner_Director_Name_TB.Text = "";
            }
        }

        private void FIO_TB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Partner_Director_Name_TB.Text))
            {
                Partner_Director_Name_TB.Text = "(Фамилия + инициалы)";
            }
        }
    }
}
