using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using _1CServicesControl.Models;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _1CServicesControl
{
    
    public partial class SimpleForm : MetroWindow
    {

        public Boolean Result = false;

        public SimpleForm()
        {
            InitializeComponent();
        }

        public SimpleForm(Server srv)
        {
            InitializeComponent();

            Title = "Удаление сервера";
            Text.Content = $"Удалить сервер \"{ srv.Name}\" ?";
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            this.Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            this.Close();
        }

    }
}
