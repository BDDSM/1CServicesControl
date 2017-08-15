using System;

namespace _1CServicesControl.Models
{
    public class Service1C
    {
        public String Name { get; set; }
        public String Ver { get; set; }
        public Boolean Debug { get; set; }
        public int ClasterPort { get; set; }
        public int AgentPort { get; set; }
        public int RangePortStart { get; set; }
        public int RangePortEnd { get; set; }
        public String SrvInfoCatalog { get; set; }
        public String Path { get; set; }
        public String State { get; set; }
        public String Status { get; set; }
        public String StartMode { get; set; }

        public Service1C(System.Management.ManagementObject sc)
        {
            this.Name = (String)sc["name"];
            this.Path = (String)sc["PathName"];
            this.State = (String)sc["State"];
            this.Status = (String)sc["Status"];
            this.StartMode = (String)sc["StartMode"];
        }

    }
}
