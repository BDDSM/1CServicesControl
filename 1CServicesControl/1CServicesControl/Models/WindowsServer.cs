using System;
using System.Collections.Generic;
using System.Management;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CServicesControl.Models
{
    public class WindowsServer : Server
    {
        public WindowsServer(string name, string address, bool isDomainAuth, string login, string pass) : base(name, address, isDomainAuth, login, pass)
        {

        }

        public override void GetServices()
        {
            services = new List<Service1C>();

            ManagementScope scope = GetScope(this);

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Service WHERE PathName LIKE \"%ragent.exe%\"");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

            ManagementObjectCollection queryCollection = searcher.Get();

            foreach (ManagementObject sc in queryCollection)
            {
                services.Add(new Service1C(sc));
            }

        }

        public ManagementScope GetScope(WindowsServer srv)
        {
            ConnectionOptions options = new ConnectionOptions();
            options.Impersonation = ImpersonationLevel.Impersonate;

            if (!srv.isDomainAuth)
            {
                options.Username = srv.login;
                options.Password = srv.pass;
            }

            ManagementScope scope = new ManagementScope("\\\\" + srv.address + "\\root\\cimv2", options);

            try
            {
                scope.Connect();
            }
            catch (Exception ex)
            {
                return null;
            }

            return scope;
        }

    }
}
