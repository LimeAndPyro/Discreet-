using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Discreet.SDK.APIS;
using System.Collections;
using Discreet.SDK.LogUtillities;
using VRC;

namespace Discreet.QOL
{
    public static class Flashlights
    {
        public static GameObject Light;
        public static GameObject Overhead;

        
        public static IEnumerator CreateGlowstickBody(bool enabled)
        {
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Started OverHeads", false, true);
            Overhead = new GameObject();
            Overhead.name = "DiscreetOverhead";
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Created OverHeads", false, true);
            Overhead.AddComponent<Light>();
            Overhead.SetActive(false);
            UnityEngine.Object.DontDestroyOnLoad(Overhead);
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "OverHeads Ready", false, true);
            while (true)
            {
                if (VRC.Player.prop_Player_0 == null) yield return null;
                Overhead.transform.localPosition = Player.prop_Player_0.transform.localPosition;
                if (enabled) Overhead.SetActive(true);
                else Overhead.SetActive(false);
                yield return null;

            }




        }
        
        

    }

}
