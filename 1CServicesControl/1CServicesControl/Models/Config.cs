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
        public List<Server> servers { get; set; }

        public Config()
        {
            this.path = Directory.GetCurrentDirectory() + @"\";
            this.filePath = this.path + "conf.json";

            if (!File.Exists(filePath))
            {
                File.Create(this.filePath);
            }

            servers = JsonConvert.DeserializeObject<List<Server>>(File.ReadAllText(this.filePath));

        }

        public void WriteNewServer(Server srv)
        {
            servers.Add(srv);
            File.WriteAllText(this.filePath, JsonConvert.SerializeObject(servers));
        }

        public void ChangeServerConf(Server srv, Server newSrv)
        {
            
        }

    }
}
