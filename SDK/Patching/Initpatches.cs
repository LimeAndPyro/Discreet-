using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discreet.SDK.LogUtillities;

namespace Discreet.SDK.Patching
{
    class Initpatches
    {
        public static void Start()
        {

            //PhotonPatches.Patch();
            //LogHandler.Log(ConsoleColor.Green, "Patched Photon", false, true);
            PlayerPatches.Patch();
            LogHandler.Log(ConsoleColor.Green, "Patched Players", false, true);

        }
    }
}
