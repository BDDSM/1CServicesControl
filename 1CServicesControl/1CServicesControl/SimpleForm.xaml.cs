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
using System.Windows.Shapes;

namespace _1CServicesControl
{
    
    public partial class SimpleForm : MetroWindow
    {

        public Boolean result = false;

        public SimpleForm()
        {
            InitializeComponent();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            result = true;
            this.Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            result = false;
            this.Close();
        }
    }
}
