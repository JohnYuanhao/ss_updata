using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Shadowsocks
{
    [Serializable]
    public class Configuration
    {
        public List<Server> configs;

        // when strategy is set, index is ignored
        public string strategy;
        public int index;
        public bool global;
        public bool enabled;
        public bool shareOverLan;
        public bool isDefault;
        public int localPort;
        public string pacUrl;
        public bool useOnlinePac;
        public bool secureLocalPac = true;
        public bool availabilityStatistics;
        public bool autoCheckUpdate;
        public bool checkPreRelease;
        public bool isVerboseLogging;


        private static string CONFIG_FILE = "gui-config.json";

        public Server GetCurrentServer()
        {
            if (index >= 0 && index < configs.Count)
                return configs[index];
            else
                return GetDefaultServer();
        }


        public static Configuration Load()
        {
            try
            {
                string configContent = File.ReadAllText(CONFIG_FILE);
                Configuration config = JsonConvert.DeserializeObject<Configuration>(configContent);
                config.isDefault = false;

                if (config.configs == null)
                    config.configs = new List<Server>();
                if (config.configs.Count == 0)
                    config.configs.Add(GetDefaultServer());
                if (config.localPort == 0)
                    config.localPort = 1080;
                if (config.index == -1 && config.strategy == null)
                    config.index = 0;

           
                return config;
            }
            catch (Exception e)
            {
               
                return new Configuration
                {
                    index = 0,
                    isDefault = true,
                    localPort = 1080,
                    autoCheckUpdate = true,
                    configs = new List<Server>()
                    {
                        GetDefaultServer()
                    }
                };
            }
        }

        public static void Save(Configuration config)
        {
            if (config.index >= config.configs.Count)
                config.index = config.configs.Count - 1;
            if (config.index < -1)
                config.index = -1;
            if (config.index == -1 && config.strategy == null)
                config.index = 0;
            config.isDefault = false;
            try
            {
                using (StreamWriter sw = new StreamWriter(File.Open(CONFIG_FILE, FileMode.Create)))
                {
                    string jsonString = JsonConvert.SerializeObject(config, Formatting.Indented);
                    sw.Write(jsonString);
                    sw.Flush();
                }
            }
            catch (IOException e)
            {
                
            }
        }

        public static Server GetDefaultServer()
        {
            return new Server();
        }

    }
}
