using Newtonsoft.Json;
using Discreet.Main;

namespace Discreet.SDK.Config
{
    public class GeneralConfig
    {
        [JsonIgnore]
        public static readonly string ConfigLocation = $"{DirectoryHandling.Configdir}//GeneralConfig.json";
        public bool IsDecore = true;
        public bool isCursor = true;
        public bool Cum = true;
        public bool LoadClean = true;
        public string Url = "";
    }
}
