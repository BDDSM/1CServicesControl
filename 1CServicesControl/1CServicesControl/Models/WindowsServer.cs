using System;
using System.Collections.Generic;
using System.Management;
using System.Windows;
using MahApps.Metro.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CServicesControl.Models
{
    public class WindowsServer : Server
    {
        public WindowsServer(string name, string address, bool isDomainAuth, string login, string pass) 
            : base(name, address, isDomainAuth, login, pass)
        {

        }

        public override string GetServices()
        {
            Services = new List<Service1C>();
            ManagementScope scope = null;

            try
            {
                scope = GetScope(this);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            

            if (scope == null) { return ""; }

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Service WHERE PathName LIKE \"%ragent.exe%\"");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

            ManagementObjectCollection queryCollection = searcher.Get();

            foreach (ManagementObject sc in queryCollection)
            {
                Services.Add(new Service1C(sc));
            }

            return "";

        }

        public ManagementScope GetScope(WindowsServer srv)
        {
            ConnectionOptions options = new ConnectionOptions();
            options.Impersonation = ImpersonationLevel.Impersonate;

            if (!srv.IsDomainAuth)
            {
                options.Username = srv.Login;
                options.Password = srv.Pass;
            }

            ManagementScope scope = new ManagementScope("\\\\" + srv.Address + "\\root\\cimv2", options);
            scope.Connect();


            return scope;
        }

    }
}
