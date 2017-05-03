using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _1CServicesControl
{
    /// <summary>
    /// Логика взаимодействия для SrvForm.xaml
    /// </summary>
    public partial class SrvForm : MetroWindow
    {

        bool saveData = false;
        bool modify = false;

        public SrvForm()
        {
            InitializeComponent();

            textNameSrv.TextChanged += TextChanged;
            textAddressSrv.TextChanged += TextChanged;
            textLoginSrv.TextChanged += TextChanged;
            textLoginSrv.TextChanged += TextChanged;

        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            Modify();
        }

        private void boolIsDomainAuth_Click(object sender, RoutedEventArgs e)
        {
            if (boolIsDomainAuth.IsChecked == true)
            {
                AuthSettingsGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                AuthSettingsGrid.Visibility = Visibility.Visible;
            }

            Modify();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            saveData = true;
            this.Close();
        }

        private void Modify ()
        {
            modify = true;
            this.Title = "Новый сервер *";
        }

    }
}
