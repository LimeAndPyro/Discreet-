//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Photon.Realtime;
//using ExitGames.Client.Photon;
//using Photon.Pun;
//using HarmonyLib;
//using Newtonsoft.Json;
//using Discreet.SDK.LogUtillities;
//using Discreet.SDK.Wrappers;

//namespace Discreet.SDK.Patching
//{
//    public class PhotonPatches
//    {
//        public static bool Onoff;
//        public static void Patch()
//        {
//            EasyPatching.EasyPatchMethodPre(typeof(LoadBalancingClient), "OnEvent", typeof(PhotonPatches), nameof(RecieveEvents));

//        }
//        public static bool RecieveEvents(EventData __0)
//        {
//            if (Onoff)
//            {
//                int sender = __0.sender;
//                Player player = PlayerWrapper.GetPlayerByActorID(sender);
//            }
//        }

//    }
//}
