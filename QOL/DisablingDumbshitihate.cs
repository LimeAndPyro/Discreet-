using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Discreet.SDK.APIS;
using Discreet.Main;
using Discreet.SDK.LogUtillities;

namespace Discreet.QOL
{
    public static class DisablingDumbshitihate
    {
        public static void CreateButtons()
        {
            QMSingleButton TurnOffavipicker = new QMSingleButton(DiscreetMenus.OddballShit, 1, 0, "AviHud", delegate
            {
                if (GameObject.Find("_UI") != null) GameObject.Find("_UI/UI_Avatars").SetActive(false);
                DiscreetGUIConsole.MessageConsoleOnClient("ff0000", "AviHud Disabled Rejoin to Reenable");
                LogHandler.Log(ConsoleColor.Red, "AviHud Disabled Rejoin to Reenable", false, false, false, true);

            }, "Gets rid of the shitty avi pedastool in vrchome", true);

        }
    }
}
