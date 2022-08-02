using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace Discreet.SDK.Config
{
    
    internal class ConfigSetup
    {
        public static bool CongfigEnabled = true;
        public static GeneralConfig GeneralConfig;
        internal static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };
        internal static void CreateConfig()
        {
            GeneralConfig = new GeneralConfig();

        }
        internal static GeneralConfig GetConfigParam()
        {
            if (GeneralConfig == null)
            {
                CreateConfig();
            }
            return GeneralConfig;
        }
        internal static void SaveConfig()
        {
            File.WriteAllText(GeneralConfig.ConfigLocation, JsonConvert.SerializeObject(GeneralConfig, Formatting.Indented, jsonSerializerSettings));
        }
        internal static void LoadGeneralConfig()
        {
            if (!File.Exists(GeneralConfig.ConfigLocation))
            {
                CreateConfig();
                return;
            }
            GeneralConfig = JsonConvert.DeserializeObject<GeneralConfig>(File.ReadAllText(GeneralConfig.ConfigLocation));

        }
    } 
}
