using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Discreet.SDK.LogUtillities;

namespace Discreet.Main
{
    public class DirectoryHandling
    {
        public static string VRCDir = Environment.CurrentDirectory;
        public static string DiscreetDir = VRCDir + "\\Discreet";
        public static string Resourcesdir = DiscreetDir + "\\Resources";
        public static string AudioDir = Resourcesdir + "\\Audio";
        public static string ImagesDir = Resourcesdir + "\\Images";
        public static string SkyboxDir = Resourcesdir + "\\Skyboxes";
        public static string Assetbundledir = Resourcesdir + "\\AssetBundles";
        public static string Configdir = DiscreetDir + "\\Configs";
        public static string OtherDir = DiscreetDir + "\\OtherStuff";
        public static void CreateDirectorys()
        {
            if (!Directory.Exists(DiscreetDir))
            {
                LogHandler.Log(ConsoleColor.Red, "No Discreet Directory Found!", true);
                LogHandler.Log(ConsoleColor.Yellow, "Creating Directorys.....");
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Discreet");
                Directory.CreateDirectory(DiscreetDir + "\\Resources");
                Directory.CreateDirectory(Resourcesdir + "\\Audio");
                Directory.CreateDirectory(Resourcesdir + "\\Images");
                Directory.CreateDirectory(Resourcesdir + "\\Skyboxes");
                Directory.CreateDirectory(Resourcesdir + "\\AssetBundles");
                Directory.CreateDirectory(DiscreetDir + "\\Configs");
                Directory.CreateDirectory(DiscreetDir + "\\OtherStuff");
                LogHandler.Log(ConsoleColor.Green, "Directorys Created!", false, true);
            }

        }
    }
}
