using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CServicesControl.Models
{
    public class LinuxServer : Server
    {
        public LinuxServer(string name, string address, bool isDomainAuth, string login, string pass) 
            : base(name, address, isDomainAuth, login, pass)
        {
        }
    }
}
