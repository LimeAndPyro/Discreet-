using System.Collections;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.Management;
using VRC.UI;

namespace Discreet.SDK.NOT_MY_CODE
{
    /// <summary>
    /// Not My Wrappers Given to By Mimic
    /// </summary>
    public static class PlayerUtils
    {
        private static readonly Dictionary<string, int> QuestUserDictionary = new Dictionary<string, int>();
        private static int _questCounter = 1;
        public static MethodInfo LoadAvatarMethod { get; set; }
        public static VRCPlayer GetLocalPlayer() => VRCPlayer.field_Internal_Static_VRCPlayer_0;
        public static APIUser GetLocalAPIUser() => APIUser.CurrentUser;

        public static Player GetPlayer(this PlayerManager instance) =>
            instance == null ? null : instance.field_Private_Player_0;

        public static VRCPlayer GetVrcPlayer() => VRCPlayer.field_Internal_Static_VRCPlayer_0;

        public static Player[] GetAllPlayers(this PlayerManager instance) =>
            instance.field_Private_List_1_Player_0.ToArray();

        public static Player GetPlayerWithPlayerID(this PlayerManager instance, int playerID) =>
            GetPlayerManager()
                .GetAllPlayers()
                .FirstOrDefault(allPlayer =>
                    allPlayer.field_Private_VRCPlayerApi_0.playerId == playerID);

        public static APIUser GetAPIUser(this PlayerManager instance) =>
            instance?.GetPlayer().GetAPIUser();

        public static APIUser GetAPIUser(this Player instance) =>
            instance == null ? null : instance.field_Private_APIUser_0;

        public static APIUser GetAPIUser(this VRCPlayer instance) =>
            instance == null ? null : instance._player.field_Private_APIUser_0;

        public static APIUser GetAPIUser(this PlayerManager instance, string userID) =>
            (from player in instance.GetAllPlayers()
             where player.GetAPIUser().id == userID
             select player.field_Private_APIUser_0).FirstOrDefault();

        //public static Player SelectedPlayer() =>
        //    GetPlayerByUserID(APIStuff.GetSelectedUserMenuQM.field_Private_IUser_0.prop_String_0);
        public static Player LocalPlayer() => Player.prop_Player_0;
        public static PageUserInfo SelectedSocialPlayer() =>
            GameObject.Find("UserInterface/MenuContent/Screens/UserInfo/")
                .GetComponent<PageUserInfo>();

        public static Player GetPlayerByUserID(string userID) =>
            (from p in PlayerManager.field_Private_Static_PlayerManager_0
                    .field_Private_List_1_Player_0.ToArray()
                    .ToList()
             where p.GetAPIUser().id == userID
             select p).FirstOrDefault();

        public static PlayerManager GetPlayerManager() =>
            PlayerManager.field_Private_Static_PlayerManager_0;

        public static Player GetPlayerWithPlayerID(int playerID) =>
            GetPlayerManager()
                .GetAllPlayers()
                .FirstOrDefault(allPlayer =>
                    allPlayer.field_Private_VRCPlayerApi_0.playerId == playerID);

        public static bool IsMaster(this Player player) => player.prop_VRCPlayerApi_0.isMaster;

        public static int GetPlayerFPS(VRCPlayer player)
        {
            if (player._playerNet.prop_Byte_0 == 0) return 0;
            return (int)(1000f / player._playerNet.prop_Byte_0);
        }

        public static int GetPlayerPing(VRCPlayer player) =>
            player._playerNet.field_Private_Int16_0;

        public static int GetSentPingAmount(VRCPlayer player) =>
            player._playerNet.field_Private_Byte_1;

        public static int GetSentFPSAmount(VRCPlayer player) =>
            player._playerNet.field_Private_Byte_0;

        public static string GetPlayerStability(int updatesSinceEvent)
        {
            if (updatesSinceEvent < 150 && updatesSinceEvent > 45)
                return "<color=#f29500>Lagging</color>";
            if (updatesSinceEvent > 100) return "<color=red>Crashed</color>";
            return "<color=green>Stable</color>";
        }

        //public static string GetFPSColored(VRCPlayer player) =>
        //    $"<color={GeneralUtils.GetFPSColor(GetPlayerFPS(player))}>{GetPlayerFPS(player)}</color>";

        //public static string GetPingColored(VRCPlayer player) =>
        //    $"<color={GeneralUtils.GetPingColor(GetPlayerPing(player))}>{GetPlayerPing(player)}</color>";

        public static string GetPlatform(Player player)
        {
            string result = null;
            if (player == null) return string.Empty;

            if (player.IsPlayerQuest()) result = "<color=green>Q</color>";

            if (player.IsInVR()) result = "<color=blue>VR</color>";

            if (!player.IsInVR() && !player.IsPlayerQuest()) result = "<color=red>PC</color>";

            return result;
        }

        public static string GetPlayerRank(APIUser instance)
        {
            if (instance.hasModerationPowers || instance.tags.Contains("admin_moderator"))
                return "Moderator";
            if (instance.hasSuperPowers || instance.tags.Contains("admin_")) return "Admin";
            if (instance.hasVeteranTrustLevel) return "Trusted";
            if (instance.hasTrustedTrustLevel) return "Known";
            if (instance.hasKnownTrustLevel) return "User";
            if (instance.hasBasicTrustLevel || instance.isNewUser) return "New User";
            if (instance.hasNegativeTrustLevel) return "Visitor";
            return instance.hasVeryNegativeTrustLevel ? "Nuiscance" : "Visitor";
        }


        //public static Color GetRankColor(APIUser player)
        //{
        //    switch (GetPlayerRank(player))
        //    {
        //        case "Friend":
        //            return GeneralUtils.HexToColor("#f2ff00");
        //        case "Moderator":
        //            return Color.blue;
        //        case "Admin":
        //            return Color.blue;
        //        case "Trusted":
        //            return GeneralUtils.HexToColor("#9500ff");
        //        case "Known":
        //            return GeneralUtils.HexToColor("#ff0800");
        //        case "User":
        //            return GeneralUtils.HexToColor("#00ff55");
        //        case "New User":
        //            return GeneralUtils.HexToColor("#00fff2");
        //        case "Visitor":
        //            return Color.gray;
        //        case "Nuiscance":
        //            return Color.black;
        //    }

        //    return Color.gray;
        //}

        public static string GetRankColorHex(APIUser player)
        {
            switch (GetPlayerRank(player))
            {
                case "Friend":
                    return "#f2ff00";
                case "Moderator":
                    return "#0000FF";
                case "Admin":
                    return "#0000FF";
                case "Trusted":
                    return "#9500ff";
                case "Known":
                    return "#FFA500";
                case "User":
                    return "#00ff55";
                case "New User":
                    return "#00fff2";
                case "Visitor":
                    return "#808080";
                case "Nuiscance":
                    return "#000000";
            }

            return "#808080";
        }
        public static Color GetTrustColor(this VRC.Player player) => VRCPlayer.Method_Public_Static_Color_APIUser_0(player.GetAPIUser());


        public static List<Player> GetAllPlayersToList() =>
            PlayerManager.field_Private_Static_PlayerManager_0 == null
                ? null
                : PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0;

        public static void ReloadAvatar(VRCPlayer player)
        {
            LoadAvatarMethod.Invoke(player, new object[] { true });
        }

        public static void ReloadAllAvatars()
        {
            foreach (var player in GetAllPlayersToList())
                if (player != null)
                    LoadAvatarMethod.Invoke(player, new object[] { true });
            RenderSettings.skybox.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        public static float GetFrames(this Player player) =>
            player._playerNet.prop_Byte_0 != 0
                ? Mathf.Floor(1000f / player._playerNet.prop_Byte_0)
                : -1f;
        public static ApiAvatar GetAPIAvatar(this VRCPlayer vrcPlayer) => vrcPlayer.prop_ApiAvatar_0;
        // public static ApiAvatar GetAPIAvatar(this Player player) => player.G().GetAPIAvatar();
        public static short GetPing(this Player player) => player._playerNet.field_Private_Int16_0;

        public static bool IsPlayerQuest(this Player instance) =>
            instance.prop_APIUser_0.IsOnMobile;

        public static bool IsInVR(this Player player) =>
            player.field_Private_VRCPlayerApi_0.IsUserInVR();

        public static bool ClientDetect(this Player player) =>
            player.GetFrames() > 90 && player.GetFrames() < 1 && player.GetPing() > 665 &&
            player.GetPing() < 0 && player.IsPlayerQuest() && player.IsInVR();

        public static bool GetIsVRChatStaff(this Player instance) =>
            instance.prop_APIUser_0.developerType == APIUser.DeveloperType.Moderator ||
            instance.prop_APIUser_0.hasModerationPowers || instance.prop_APIUser_0.hasSuperPowers;

        public static void CheckForQuest(Player player, [Optional] bool state)
        {
            if (state && IsPlayerQuest(player))
            {
                QuestUserDictionary.Add(GetAPIUser(player).id, ++_questCounter);
                player.prop_USpeaker_0.field_Private_SimpleAudioGain_0.field_Public_Single_0 = 0.0f;
            }
            else if (QuestUserDictionary.ContainsKey(GetAPIUser(player).id))
            {
                QuestUserDictionary.Remove(player.GetAPIUser().id);
                player.prop_USpeaker_0.field_Private_SimpleAudioGain_0.field_Public_Single_0 = 1.0f;
            }
        }

        public static void CheckForQuestUserOnLoad()
        {
            foreach (var player in GetAllPlayersToList()) CheckForQuest(player);
        }

        public static bool IsBlockedEitherWay(string userId)
        {
            var moderationManager = ModerationManager.prop_ModerationManager_0;
            if (moderationManager == null) return false;
            if (APIUser.CurrentUser.id == userId) return false;

            var moderationsDict = ModerationManager.prop_ModerationManager_0
                .field_Private_Dictionary_2_String_List_1_ApiPlayerModeration_0;
            if (!moderationsDict.ContainsKey(userId)) return false;

            foreach (var playerModeration in moderationsDict[userId])
                if (playerModeration != null && playerModeration.moderationType ==
                    ApiPlayerModeration.ModerationType.Block)
                    return true;

            return false;
        }

        public static Quaternion GetPlayerRotation() => GetLocalPlayer().transform.rotation;

        public static Vector3 GetPlayerPosition() => GetLocalPlayer().transform.position;

        public static void SendToLocation(Vector3 pos, Quaternion rot)
        {
            GetLocalPlayer().transform.position = pos;
            GetLocalPlayer().transform.rotation = rot;
        }

        //public static void LogInfoOnUser(Player player)
        //{
        //    var apiUser = player != null ? player.prop_APIUser_0 : null;
        //    if (apiUser != null)
        //        ConsoleUtils.OnLogInfo(
        //            $"\n{MiscUtils.MultiCharacterString("=", 50)}\n" +
        //            $"Display Name: [{apiUser.displayName}]\n" +
        //            $"User ID:{apiUser.id}\n" + $"Is Moderator: {apiUser.hasModerationPowers}\n" +
        //            $"Is Troll: {apiUser.hasVeryNegativeTrustLevel || apiUser.hasNegativeTrustLevel}\n" +
        //            $"Has VRC+: {apiUser.hasVIPAccess}\n" +
        //            $"Trust Level: {GetPlayerRank(apiUser)}\n" + $"Bio: {apiUser.bio}\n" +
        //            $"Bio Links: {apiUser.bioLinks}" + $"Current Avatar: {apiUser.avatarId}\n" +
        //            $"Current Avatar Asset URL: {apiUser.currentAvatarAssetUrl}\n" +
        //            $"Current Avatar Image URL: {apiUser.currentAvatarImageUrl}" +
        //            $"Current Avatar Thumbnail Image URL: {apiUser.currentAvatarThumbnailImageUrl}\n" +
        //            $"Date Joined: {apiUser.date_joined}\n" +
        //            $"Developer Type: {apiUser.developerType}\n" +
        //            $"Last Platform: {apiUser._last_platform}\n" +
        //            $"{MiscUtils.MultiCharacterString("=", 50)}");
        //}

        
    }
}

