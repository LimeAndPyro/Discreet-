using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC;
using ExitGames.Client.Photon;
using Discreet.SDK.LogUtillities;
using Discreet.SDK.Functions;
using Discreet.QOL;

namespace Discreet.SDK.Patching
{
    public class PlayerPatches
    {
        public static string RecentlyJoined;
        public static string RecentlyLeft;
        public static List<Player> allplayersjoinandleave = new List<Player>();
        
        public static void Patch()
        {
            EasyPatching.EasyPatchMethodPost(typeof(NetworkManager), "Method_Public_Void_Player_1", typeof(PlayerPatches), "OnArrival");
            EasyPatching.EasyPatchMethodPost(typeof(NetworkManager), "Method_Public_Void_Player_0", typeof(PlayerPatches), "OnDeparture");
        }

        public static void OnArrival(VRC.Player __0)
        {

            LogUtillities.LogHandler.Log(ConsoleColor.DarkMagenta, $"{__0.prop_APIUser_0.displayName} Has Joined The Lobby", false, false, true);
            DiscreetGUIConsole.MessageConsoleOnClient("66b4de", $"{__0.prop_APIUser_0.displayName} Joined The Lobby");
            RecentlyJoined = LimesFunctions.WrapTextInHexColor("6b2213", $"Recently Joined: {__0.field_Private_APIUser_0.displayName}");
            HudLogger.Log("6b2213", $"Recently Joined: {__0.field_Private_APIUser_0.displayName}");

            if (__0.field_Private_APIUser_0.isFriend)
            {
                HudLogger.Log("e82372", $"Your Friend => {__0.prop_APIUser_0.displayName} Joined The Lobby", "Friend");
                LogUtillities.LogHandler.Log(ConsoleColor.Yellow, $"Your Friend => {__0.prop_APIUser_0.displayName} Joined The Lobby", false, false, true);
                DiscreetGUIConsole.MessageConsoleOnClient("e82372", $"Your Friend => {__0.prop_APIUser_0.displayName} Joined The Lobby");
            }
            if (DeafanMePlease.IsDeafened) DeafanMePlease.togglePlayerExistingAudiosources(false);
            else DeafanMePlease.togglePlayerExistingAudiosources(true);
            
                
        }

        public static void OnDeparture(VRC.Player __0)
        {
            LogUtillities.LogHandler.Log(ConsoleColor.Cyan, $"{__0.prop_APIUser_0.displayName} Left The Lobby", false, false, true);
            DiscreetGUIConsole.MessageConsoleOnClient("914dbf", $"{__0.prop_APIUser_0.displayName} Left The Lobby");
            RecentlyLeft = LimesFunctions.WrapTextInHexColor("5900ed", $"Recently Left: {__0.field_Private_APIUser_0.displayName}");
            HudLogger.Log("5900ed", $"Recently Left: {__0.field_Private_APIUser_0.displayName}");

            if (__0.field_Private_APIUser_0.isFriend)
            {
                HudLogger.Log("e82372", $"Your Friend => {__0.prop_APIUser_0.displayName} Left The Lobby");
                LogUtillities.LogHandler.Log(ConsoleColor.Yellow, $"Your Friend => {__0.prop_APIUser_0.displayName} Left The Lobby", false, false, true);
                DiscreetGUIConsole.MessageConsoleOnClient("e82372", $"Your Friend => {__0.prop_APIUser_0.displayName} Left The Lobby");


            }

        }

    }
}
