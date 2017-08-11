using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using _1CServicesControl.Models;


namespace _1CServicesControl
{

    public partial class SrvForm : MetroWindow
    {

        public bool saveData = false;
        public bool deleteThisServer = false;
        public Server srv;
        public Server oldSrv;

        public SrvForm()
        {
            InitializeComponent();
            
            if (((MainWindow)Application.Current.MainWindow).RootTabControl.SelectedIndex == 1)
            {
                srvType.IsChecked = true;
            }

            Delete.Visibility = Visibility.Hidden;

            srv = new Server();
            this.DataContext = this.srv;
            this.Title = "Новый сервер";
        }

        public SrvForm(Server srv)
        {
            InitializeComponent();

            if (((MainWindow)Application.Current.MainWindow).RootTabControl.SelectedIndex == 1)
            {
                srvType.IsChecked = true;
            }

            this.srv = new Server(srv);
            this.DataContext = this.srv;
            this.oldSrv = srv;

            PassSrv.Password = "..!..";
            srvType.IsEnabled = false;
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
            if (NameSrv.Text == "" || AddressSrv.Text == "")
            {
                await this.ShowMessageAsync("Ошибка сохранения сервера", "Значение наименование и адрес сервера должны быть заполнены");
                return;
            }

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

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            NameSrv.Focus();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            SimpleForm form = new SimpleForm();
            form.Title = "Удаление сервера";
            form.Text.Content = "Удалить сервер \"" + srv.name + "\" ?";

            form.ShowDialog();

            if (!form.result)
            {
                return;
            }

            if ((Boolean)srvType.IsChecked)
            {
                ((_1CServicesControl.MainWindow)Application.Current.MainWindow).config.DeleteServer((LinuxServer)this.oldSrv);
            }
            else
            {
                ((_1CServicesControl.MainWindow)Application.Current.MainWindow).config.DeleteServer((WindowsServer)this.oldSrv);
            }

            this.Close();
       
        }

    }
}
