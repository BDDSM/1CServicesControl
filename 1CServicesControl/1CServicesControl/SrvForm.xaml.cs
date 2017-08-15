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
        public bool SaveData = false;
        public bool DeleteThisServer = false;
        public Server Srv;
        public Server OldSrv;
        MainWindow mainWindow = ((MainWindow)Application.Current.MainWindow);

        static String textErrorAuth = "Значение логин и пароль должны быть заполнены";
        static String textErrorName = "Значение наименование и адрес сервера должны быть заполнены";


        public SrvForm()
        {
            InitializeComponent();
            
            if (mainWindow.RootTabControl.SelectedIndex == 1)
            {
                srvType.IsChecked = true;
            }

            Delete.Visibility = Visibility.Hidden;

            Srv = new Server();
            this.DataContext = this.Srv;
            this.Title = "Новый сервер";
        }

        public SrvForm(Server srv)
        {
            InitializeComponent();

            if (mainWindow.RootTabControl.SelectedIndex == 1)
            {
                srvType.IsChecked = true;
            }

            this.Srv = new Server(srv);
            this.DataContext = this.Srv;
            this.OldSrv = srv;

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
            String title = "Ошибка сохранения сервера";
            
            if (String.IsNullOrEmpty(NameSrv.Text) || String.IsNullOrEmpty(AddressSrv.Text))
            {
                await this.ShowMessageAsync(title, textErrorName);
                return;
            }

            if (IsDomainAuth.IsChecked != true)
            {
                if (String.IsNullOrEmpty(LoginSrv.Text) || String.IsNullOrEmpty(PassSrv.Password))
                {
                    await this.ShowMessageAsync(title, textErrorAuth);
                    return;
                }
            }

            SaveData = true;
            this.Close();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            NameSrv.Focus();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            SimpleForm form = new SimpleForm(Srv);
            form.ShowDialog();

            if (!form.Result)
            {
                return;
            }

            if ((Boolean)srvType.IsChecked)
            {
                mainWindow.Config.DeleteServer((LinuxServer)this.OldSrv);
            }
            else
            {
                mainWindow.Config.DeleteServer((WindowsServer)this.OldSrv);
            }

            this.Close();
       
        }

    }
}
