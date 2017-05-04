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

    public partial class SrvForm : MetroWindow
    {

        public bool saveData = false;

        public SrvForm()
        {
            InitializeComponent();
        }

        private void boolIsDomainAuth_Click(object sender, RoutedEventArgs e)
        {
            if (IsDomainAuth.IsChecked == true)
            {
                AuthSettingsGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                AuthSettingsGrid.Visibility = Visibility.Visible;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (IsDomainAuth.IsChecked != true)
            {
                if (LoginSrv.Text == "" || PassSrv.Password == "")
                {
                    await this.ShowMessageAsync("Ошибка сохранения сервера", "Значение логин и пароль должны быть заполнены");
                    return;
                }
            }

            saveData = true;
            this.Close();
        }
    }
}
