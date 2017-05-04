using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;


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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            if(modify)
            {

            }

            this.Close();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (boolIsDomainAuth.IsChecked != true)
            {
                if (textLoginSrv.Text == "" || passBoxSrv.Password == "")
                {
                    await this.ShowMessageAsync("Ошибка сохранения сервера", "Значение логин и пароль должны быть заполнены");
                    return;
                }
            }

            saveData = true;
            this.Close();
        }

        private void Modify ()
        {
            modify = true;
            this.Title = "Новый сервер *";
        }

        private void passBoxSrv_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Modify();
        }
    }
}
