using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _1CServicesControl.Models
{
    public class Server
    {
        public String Name { get; set; }
        public String Address { get; set; }
        public String Domain { get; set; }
        public String Login { get; set; }
        public String Pass { get; set; }
        public Boolean IsDomainAuth { get; set; }
        public virtual List<Service1C> Services { get; set; }
        public virtual bool IsActiveRing {get; set;}
        

        public Server(String name, String address, Boolean isDomainAuth, String login, String pass)
        {
            this.Name = name;
            this.Address = address;
            this.Login = login;
            this.Pass = pass;
            this.IsDomainAuth = isDomainAuth;
        }

        public Server(Server srv)
        {
            this.Name = srv.Name;
            this.Address = srv.Address;
            this.Login = srv.Login;
            this.Pass = srv.Pass;
            this.IsDomainAuth = srv.IsDomainAuth;
        }

        public Server()
        {
            this.Name = "";
            this.Address = "";
            this.Login = "";
            this.Pass = "";
            this.IsDomainAuth = false;
        }

        public virtual String GetServices()
        {
            return "";
        }

        public Task<String> GetServicesAsync()
        {
            return Task.Run(() => 
            {
                return GetServices();
            });
        }

    }
}
