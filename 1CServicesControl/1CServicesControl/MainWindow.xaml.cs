using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Management;
using _1CServicesControl.Models;

namespace _1CServicesControl
{

    public partial class MainWindow : MetroWindow
    {
        public Config Config;
        List<Server> serversOnProgress;

        public MainWindow()
        {
            InitializeComponent();

            Config = new Config();
            
            WindowsTabControl.SelectionChanged += SelectionChanged;
            LinuxTabControl.SelectionChanged += SelectionChanged;
            Ring.IsActive = false;

            serversOnProgress = new List<Server>();
        }

        private async void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Server server = (Server)((object[])e.AddedItems)[0];
            string title = $"Ошибка подключения к серверу: \n \"{ server.Name}\"";

            var findingItem = serversOnProgress.Find(x => x == server);

            if (findingItem == null)
            {
                serversOnProgress.Add(server);
            }
            else
            {
                Ring.IsActive = true;
                return;
            }

            Ring.IsActive = true;
            string textError = await server.GetServicesAsync();
            Ring.IsActive = false;

            serversOnProgress.Remove(server);

            if (string.IsNullOrEmpty(textError)) { return; }

            await this.ShowMessageAsync(title, textError);
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowsTabControl.ItemsSource = Config.WindowsSrvs;
            LinuxTabControl.ItemsSource = Config.LinuxSrvs;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SrvForm srvForm = new SrvForm();

            srvForm.ShowDialog();

            if (!srvForm.SaveData)
            {
                return;
            }

            var name = srvForm.NameSrv.Text;
            var adress = srvForm.AddressSrv.Text;
            var isDomainAuth = (bool)srvForm.IsDomainAuth.IsChecked;
            var login = srvForm.LoginSrv.Text;
            var pass = srvForm.PassSrv.Password;

            if ((bool)srvForm.srvType.IsChecked)
            {
                LinuxServer newSrv = new LinuxServer(name, adress, isDomainAuth, login, pass);
                Config.AddNewServer(newSrv);
            }
            else
            {
                WindowsServer newSrv = new WindowsServer(name, adress, isDomainAuth, login, pass);
                Config.AddNewServer(newSrv);
            }

            RefreshTabComtrols();

        }

        private void EditServer_Click(object sender, RoutedEventArgs e)
        {

            var selectedSrv = RootTabControl.SelectedIndex == 0
                        ? WindowsTabControl.SelectedItem : LinuxTabControl.SelectedItem;

            if (selectedSrv == null) { return; }

            SrvForm srvForm = new SrvForm((Server)selectedSrv);
            srvForm.ShowDialog();

            if (!srvForm.SaveData)
            {
                RefreshTabComtrols();
                return;
            }

            srvForm.OldSrv.Name = srvForm.Srv.Name;
            srvForm.OldSrv.Address = srvForm.Srv.Address;
            srvForm.OldSrv.IsDomainAuth = srvForm.Srv.IsDomainAuth;
            srvForm.OldSrv.Login = srvForm.Srv.Login;
            srvForm.OldSrv.Pass = srvForm.Srv.Pass;

            if (srvForm.PassSrv.Password != "..!..")
            {
                srvForm.OldSrv.Pass = srvForm.PassSrv.Password;
            }

            Config.SaveConf();
            RefreshTabComtrols();

        }

        private void RefreshTabComtrols()
        {
            WindowsTabControl.Items.Refresh();
            LinuxTabControl.Items.Refresh();
        }

        private void GitHub_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/WizaXxX/1CServicesControl");
        }

        private void DeleteServer_Click(object sender, RoutedEventArgs e)
        {

            var selectedSrv = RootTabControl.SelectedIndex == 0
                        ? WindowsTabControl.SelectedItem : LinuxTabControl.SelectedItem;


            if (selectedSrv == null) { return; }

            SimpleForm form = new SimpleForm((Server)selectedSrv);
            form.ShowDialog();

            if (!form.Result)
            {
                return;
            }

            if (selectedSrv is WindowsServer)
            {
                Config.DeleteServer((WindowsServer)WindowsTabControl.SelectedItem);
            }
            else
            {
                Config.DeleteServer((LinuxServer)LinuxTabControl.SelectedItem);
            }

            RefreshTabComtrols();

        }

        private void TabControl_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton != System.Windows.Input.MouseButton.Left)
            {
                return;
            }

            EditServer_Click(sender, null);

        }
    }
}
