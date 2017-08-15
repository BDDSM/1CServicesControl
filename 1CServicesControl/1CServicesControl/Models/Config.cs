using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace _1CServicesControl.Models
{
    public class Config
    {
        String path { get; }
        String filePath { get; }

        public List<LinuxServer> LinuxSrvs { get; set; }
        public List<WindowsServer> WindowsSrvs { get; set; }

        public Config()
        {
            this.path = Directory.GetCurrentDirectory() + @"\";
            this.filePath = this.path + "conf.json";

            this.LinuxSrvs = new List<LinuxServer>();
            this.WindowsSrvs = new List<WindowsServer>();

            if (!File.Exists(filePath))
            {
                using (File.Create(this.filePath)) ;   
            }

            var definition = new { windowsSrvs = new List<WindowsServer>(), linuxSrvs = new List<LinuxServer>()};

            var saveConf = JsonConvert.DeserializeAnonymousType(File.ReadAllText(this.filePath), definition);
            
            if(saveConf == null) { return;}

            this.LinuxSrvs = saveConf.linuxSrvs;
            this.WindowsSrvs = saveConf.windowsSrvs;
 
        }

        public void SaveConf()
        {
            File.WriteAllText(this.filePath, JsonConvert.SerializeObject(this));
        }

        public void AddNewServer(WindowsServer srv)
        {
            WindowsSrvs.Add(srv);
            SaveConf();
        }

        public void AddNewServer(LinuxServer srv)
        {
            LinuxSrvs.Add(srv);
            SaveConf();
        }

        public void DeleteServer(WindowsServer srv)
        {
            WindowsSrvs.Remove(srv);
            SaveConf();
        }

        public void DeleteServer(LinuxServer srv)
        {
            LinuxSrvs.Remove(srv);
            SaveConf();
        }

    }
}
