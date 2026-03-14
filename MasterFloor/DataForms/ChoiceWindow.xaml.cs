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
using ClassLibrary.Classes;

namespace MasterFloor.DataForms
{
    public partial class ChoiceWindow : Window
    {
        public ChoiceWindow(List<Production> productions)
        {
            InitializeComponent();
            foreach (Production p in productions)
            {
                Choice_CB.Items.Add(p.Name);
            }
        }
        public int GetIndex()
        {
            return Choice_CB.SelectedIndex;
        }

        private void accept_btn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
