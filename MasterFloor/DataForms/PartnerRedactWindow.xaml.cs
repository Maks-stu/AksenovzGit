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
        readonly List<string> types = [ "ИП", "КФЛ", "ХТ", "ХО", "ООО", "АО", "НАО", "ПАО", "ХП", "УП" ];
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
            bool answer = true;
            if (!int.TryParse(Partner_Rating_TB.Text, out _))
            {
                MessageBox.Show("Рейтинг должен быть целым числом или нулем");
                return false;
            }
            if (!IsEmailValid(Partner_Email_TB.Text, out string mail_error))
            {
                MessageBox.Show(mail_error);
                return false;
            }
            if (!IsPhoneNumberValid(Partner_Phone_TB.Text, out string phone_error))
            {
                MessageBox.Show(phone_error);
                return false;
            }
            if (!IsFIOValid(Partner_Director_Name_TB.Text, out string fio_error))
            {
                MessageBox.Show(fio_error);
                return false;
            }
            if (!IsValidINN(Partner_INN_TB.Text, out string INN_error))
            {
                MessageBox.Show(INN_error);
                return false;
            }
            if (!(Partner_Type_CB.SelectedIndex != -1))
            {
                MessageBox.Show("Выберите тип партнера");
                return false;
            }
            return answer;
        }

        private void Partner_Logo_Choosing(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Title = "Select a File", Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*" };


            if (openFileDialog.ShowDialog() == true)
            {
                Partner_Logo_B.Content = openFileDialog.FileName;
            }
        }

        public static bool IsValidINN(string inn, out string error)
        {
            error = "";
            bool answer = true;
            if (string.IsNullOrWhiteSpace(inn))
            {
                error = "Введите ИНН";
                answer = false;
            }

            inn = inn.Trim();

            // Проверка, чтобы ИНН состоял только из цифр
            if (!inn.All(char.IsDigit))
            {
                if(error.Length == 0)
                    error = "ИНН должен содержать только цифры";
                else
                    error += " и должен содержать только цифры";
                answer = false;
            }

            if (!(inn.Length == 10 || inn.Length == 12))
            {
                if (error.Length == 0)
                    error = "ИНН должен состоять из 10 или 12 цифр";
                else
                    error += " и должен состоять из 10 или 12 цифр";
                answer = false;
            }
            return answer;
        }

        public static bool IsPhoneNumberValid(string phoneNumber, out string error)
        {
            //// Регулярное выражение для проверки номера (например, для России)
            //string pattern = @"^(\+7|7|8)?\d{10}$";
            //error = "Номер телефона должен быть в одном из этих форматов: \"+7 10 цифр\", \"7 10 цифр\" или \"8 10 цифр\"";
            //return Regex.IsMatch(phoneNumber, pattern);
            error = "Номер телефона должен состоять только из цифр";
            return phoneNumber.All(char.IsDigit);
        }
        public static bool IsFIOValid(string fio, out string error)
        {
            error = "";
            bool answer = true;
            if (fio.Any(char.IsDigit))
            {
                error = "ФИО директора не должно включать в себя цифры";
                answer = false;
            }
            string FIO_Shablon = "Фамилия И.О.";
            string pattern = @"^[A-ZА-ЯЁ][a-zа-яё]+ [A-ZА-ЯЁ]\.[A-ZА-ЯЁ]\.$";
            if (!Regex.IsMatch(fio, pattern))
            {
                if (error.Length == 0)
                    error = "ФИО директора должно быть формата " + FIO_Shablon;
                else
                    error += " и должно быть формата " + FIO_Shablon;
                answer = false;
            }
            return answer;
        }

        public static bool IsEmailValid(string email, out string error)
        {
            error = "";
            bool answer = true;
            //Записан ли email в нужную строку
            if (string.IsNullOrWhiteSpace(email))
            {
                error = "Введите Электронную почту";
                answer = false;
            }

            string E_mail_Shablon = " имя@домен.расширение";
            // Простое регулярное выражение для проверки формата email в виде имя@домен.расширение
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if(!Regex.IsMatch(email, pattern))
            {
                if (error.Length == 0)
                    error = "Электронная почта должна быть формата " + E_mail_Shablon;
                else
                    error += " и должна быть формата " + E_mail_Shablon;
                answer = false;
            }
            return answer;
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
