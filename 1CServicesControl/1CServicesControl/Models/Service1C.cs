using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CServicesControl.Models
{
    public class Service1C
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

        public Service1C(System.Management.ManagementObject sc)
        {
            name = (String)sc["name"];
            path = (String)sc["PathName"];
            state = (String)sc["State"];
            status = (String)sc["Status"];
            startMode = (String)sc["StartMode"];
        }

    }
}
