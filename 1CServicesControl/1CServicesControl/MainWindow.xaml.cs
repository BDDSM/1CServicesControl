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

            WindowsTabControl.SelectionChanged += SelectionChanged;
            LinuxTabControl.SelectionChanged += SelectionChanged;
        }

        private void SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            var server = ((object[])e.AddedItems)[0];
            
            if (server is WindowsServer)
            {
                ((WindowsServer)server).GetServices();
            }
            else
            {
                ((LinuxServer)server).GetServices();
            }
            

            int t = 3;

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowsTabControl.ItemsSource = config.windowsSrvs;
            LinuxTabControl.ItemsSource = config.linuxSrvs;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SrvForm newSrvForm = new SrvForm();

            newSrvForm.Show();
            newSrvForm.Closed += newSrvForm_Closed;
        }

        public void newSrvForm_Closed(object sender, EventArgs e)
        {
            SrvForm srvForm = (SrvForm)sender;

            if (!srvForm.saveData)
            {
                return;
            }

            var name = srvForm.NameSrv.Text;
            var adress = srvForm.AddressSrv.Text;
            var isDomainAuth = (bool)srvForm.IsDomainAuth.IsChecked;
            var login = srvForm.LoginSrv.Text;
            var pass = srvForm.PassSrv.Password;

            if ((Boolean)srvForm.srvType.IsChecked)
            {
                LinuxServer newSrv = new LinuxServer(name, adress, isDomainAuth, login, pass);
                config.AddNewServer(newSrv);
            }
            else
            {
                WindowsServer newSrv = new WindowsServer(name, adress, isDomainAuth, login, pass);
                config.AddNewServer(newSrv);    
            }

            RefreshTabComtrols();

        }

        private void EditServer_Click(object sender, RoutedEventArgs e)
        {

            Server selectedSrv;

            if (RootTabControl.SelectedIndex == 0)
            {
                selectedSrv = (Server)WindowsTabControl.SelectedItem;
            }
            else
            {
                selectedSrv = (Server)LinuxTabControl.SelectedItem;
            }

            if (selectedSrv == null) { return; }

            SrvForm form = new SrvForm(selectedSrv);
            form.Closed += formChange_Closed;
            form.Show();

        }

        public void formChange_Closed(object sender, EventArgs e)
        {
            SrvForm srvForm = (SrvForm)sender;

            if (!srvForm.saveData)
            {
                RefreshTabComtrols();
                return;
            }

            srvForm.oldSrv.name = srvForm.srv.name;
            srvForm.oldSrv.address = srvForm.srv.address;
            srvForm.oldSrv.isDomainAuth = srvForm.srv.isDomainAuth;
            srvForm.oldSrv.login = srvForm.srv.login;
            srvForm.oldSrv.pass = srvForm.srv.pass;

            if (srvForm.PassSrv.Password != "..!..")
            {
                srvForm.oldSrv.pass = srvForm.PassSrv.Password;
            }
            
            config.SaveConf();
            RefreshTabComtrols();

        }

        public void RefreshTabComtrols()
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
            Server selectedSrv;
            Boolean win = true;

            if (RootTabControl.SelectedIndex == 0)
            {
                selectedSrv = (Server)WindowsTabControl.SelectedItem;
                win = true;
            }
            else
            {
                selectedSrv = (Server)LinuxTabControl.SelectedItem;
                win = false;
            }

            if (selectedSrv == null) { return; }

            SimpleForm form = new SimpleForm();
            form.Title = "Удаление сервера";
            form.Text.Content = "Удалить сервер \"" + selectedSrv.name + "\" ?";

            form.ShowDialog();

            if (!form.result)
            {
                return;
            }

            if (win)
            {
                config.DeleteServer((WindowsServer)WindowsTabControl.SelectedItem);
            }
            else
            {
                config.DeleteServer((LinuxServer)LinuxTabControl.SelectedItem);
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
