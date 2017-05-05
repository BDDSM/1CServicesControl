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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management;

namespace _1CServicesControl
{

    public partial class MainWindow : MetroWindow
    {

        public List<Server> listSrv;

        public MainWindow()
        {
            InitializeComponent();

            listSrv = new List<Server>();
        }

        public class Customer
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int NumberOfContracts { get; set; }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainTabControl.ItemsSource = listSrv;
            MainTabControl.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SrvForm newSrvForm =  new SrvForm();
            newSrvForm.Show();
            
            newSrvForm.Closed += newSrvForm_Closed;
        }

        private void newSrvForm_Closed(object sender, EventArgs e)
        {
            SrvForm srvForm = (SrvForm)sender;
            
            if (srvForm.saveData)
            {
                var name = srvForm.NameSrv.Text;
                var adress = srvForm.AddressSrv.Text;
                var isDomainAuth = (bool)srvForm.IsDomainAuth.IsChecked;
                var login = srvForm.LoginSrv.Text;
                var pass = srvForm.PassSrv.Password;

                Server srv = new Server(name, adress, isDomainAuth, login, pass);

                listSrv.Add(srv);

                MainTabControl.Items.Refresh();

                getServices(srv);
            }
            
        }

        private void getServices(Server srv)
        {
            ConnectionOptions options = new ConnectionOptions();
            options.Impersonation = ImpersonationLevel.Impersonate;

            if (!srv.isDomainAuth)
            {
                options.Username = srv.login;
                options.Password = srv.pass;
            }

            ManagementScope scope = new ManagementScope("\\\\" + srv.address + "\\root\\cimv2", options);
            scope.Connect();

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Service WHERE PathName LIKE \"%ragent.exe%\"");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

            ManagementObjectCollection queryCollection = searcher.Get();

            foreach (ManagementObject sc in queryCollection)
            {
                srv.services.Add(new service1C(sc));
            }

        }
    }


    public class Server
    {
        public String name { get; set; }
        public String address { get; set; }
        public String domain { get; set; }
        public String login { get; set; }
        public String pass { get; set; }
        public Boolean isDomainAuth { get; set; }
        public List<service1C> services { get; set; }

        public Server(String newName, String newAddress, Boolean newIsDomainAuth, String newLogin, String newPass)
        {
            name = newName;
            address = newAddress;
            login = newLogin;
            pass = newPass;
            isDomainAuth = newIsDomainAuth;

            services = new List<service1C>();
        }
    }

    public class service1C
    {
        public String name { get; set; }
        public String ver { get; set; }
        public Boolean debug { get; set; }
        public int clasterPort { get; set; }
        public int agentPort { get; set; }
        public int rangePortStart { get; set; }
        public int rangePortEnd { get; set; }
        public String srvInfoCatalog { get; set; }
        public String path { get; set; }
        public String state { get; set; }
        public String status { get; set; }
        public String startMode { get; set; }

        public service1C(ManagementObject sc)
        {
            name = (String)sc["name"];
            path = (String)sc["PathName"];
            state = (String)sc["State"];
            status = (String)sc["Status"];
            startMode = (String)sc["StartMode"];
        }
    }
}
