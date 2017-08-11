using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CServicesControl.Models
{
    public class Config
    {
        String path { get; }
        String filePath { get; }

        public List<LinuxServer> linuxSrvs { get; set; }
        public List<WindowsServer> windowsSrvs { get; set; }


        public Config()
        {
            this.path = Directory.GetCurrentDirectory() + @"\";
            this.filePath = this.path + "conf.json";

            this.linuxSrvs = new List<LinuxServer>();
            this.windowsSrvs = new List<WindowsServer>();

            if (!File.Exists(filePath))
            {
                File.Create(this.filePath);
            }

            var definition = new { windowsSrvs = new List<WindowsServer>(), linuxSrvs = new List<LinuxServer>()};

            var saveConf = JsonConvert.DeserializeAnonymousType(File.ReadAllText(this.filePath), definition);

            linuxSrvs = saveConf.linuxSrvs;
            windowsSrvs = saveConf.windowsSrvs;
 
        }

        public void SaveConf()
        {
            File.WriteAllText(this.filePath, JsonConvert.SerializeObject(this));
        }

        public void AddNewServer(WindowsServer srv)
        {
            windowsSrvs.Add(srv);
            SaveConf();
        }

        public void AddNewServer(LinuxServer srv)
        {
            linuxSrvs.Add(srv);
            SaveConf();
        }

        public void DeleteServer(WindowsServer srv)
        {
            windowsSrvs.Remove(srv);
            SaveConf();
        }

        public void DeleteServer(LinuxServer srv)
        {
            linuxSrvs.Remove(srv);
            SaveConf();
        }

    }
}
