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

namespace _1CServicesControl
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
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
            listSrv.Add(new Server("Server1", "1123/12..123.", false, "", ""));
            listSrv.Add(new Server("Server1", "1123/12..123.", false, "", ""));
            listSrv.Add(new Server("Server1", "1123/12..123.", false, "", ""));
            listSrv.Add(new Server("Server1", "1123/12..123.", false, "", ""));
            listSrv.Add(new Server("Server1", "1123/12..123.", false, "", ""));

            MainTabControl.ItemsSource = listSrv;
            MainTabControl.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            //MainTabControl.ItemsSource = listSrv;
        }
    }


    public class Server
    {
        public String name { get; set; }
        public String address { get; set; }
        public String login { get; set; }
        public String pass { get; set; }
        public Boolean isDomainAuth { get; set; }

        public Server(String newName, String newAddress, Boolean newIsDomainAuth, String newLogin, String newPass)
        {
            name = newName;
            address = newAddress;
            login = newLogin;
            pass = newPass;
            isDomainAuth = newIsDomainAuth;
        }
    }

}
