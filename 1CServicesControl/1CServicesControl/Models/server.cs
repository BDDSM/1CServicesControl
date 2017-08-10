using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CServicesControl.Models
{
    public class Server
    {
        public String name { get; set; }
        public String address { get; set; }
        public String domain { get; set; }
        public String login { get; set; }
        public String pass { get; set; }
        public Boolean isDomainAuth { get; set; }

        public Server(String name, String address, Boolean isDomainAuth, String login, String pass)
        {
            this.name = name;
            this.address = address;
            this.login = login;
            this.pass = pass;
            this.isDomainAuth = isDomainAuth;
        }

        public virtual List<Service1C> GetServices()
        {
            return new List<Service1C>();
        }

    }
}
