using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Management;
using _1CServicesControl.Models;

namespace _1CServicesControl
{

    public partial class MainWindow : MetroWindow
    {
        public Config config;

        public MainWindow()
        {
            InitializeComponent();

            config = new Config();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainTabControl.ItemsSource = config.servers;
            MainTabControl.SelectedIndex = 0;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    SrvForm newSrvForm =  new SrvForm();

        //    newSrvForm.Show();  
        //    newSrvForm.Closed += newSrvForm_Closed;
        //}

        //public void newSrvForm_Closed(object sender, EventArgs e)
        //{
        //    SrvForm srvForm = (SrvForm)sender;
            
        //    if (srvForm.saveData)
        //    {
        //        //var name = srvForm.NameSrv.Text;
        //        //var adress = srvForm.AddressSrv.Text;
        //        //var isDomainAuth = (bool)srvForm.IsDomainAuth.IsChecked;
        //        //var login = srvForm.LoginSrv.Text;
        //        //var pass = srvForm.PassSrv.Password;

        //        //Server srv = new Server(name, adress, isDomainAuth, login, pass);

        //        //listSrv.Add(srv);

        //        //srv.isActiveProgressRing = true;
        //        //srv.servicesVisibility = Visibility.Hidden;
        //        //srv.isEnabaleComandButon = true;

        //        //MainTabControl.Items.Refresh();

        //        //Task.Run(() => getServices(srv));
        //        //int t = 1;
        //    }
            
        //}

    }
}
