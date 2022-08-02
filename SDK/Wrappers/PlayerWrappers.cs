using System;
using System.Collections.Generic;
using System.Linq;
using UnhollowerBaseLib;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.DataModel;
using VRC.SDKBase;
using VRC.UI;
using VRC.UI.Elements.Menus;
using Discreet.SDK.LogUtillities;

namespace Discreet.SDK.Wrappers
{
    public static class PlayerWrapper
    {
        public static Dictionary<int, VRC.Player> PlayersActorID = new Dictionary<int, VRC.Player>();
        public static Player[] GetAllPlayers() => PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray();
        public static Player GetByUsrID(string usrID) => GetAllPlayers().First(x => x.prop_APIUser_0.id == usrID);
        public static void Teleport(this Player player) => LocalVRCPlayer().transform.position = player.prop_VRCPlayer_0.transform.position;
        public static Player LocalPlayer() => Player.prop_Player_0;

        public static List<string> PlayerFriendIDs = APIUser.CurrentUser.friendIDs.ToArray().ToList();

        public static Vector3 LocalPlayerpos = LocalPlayer().transform.localPosition;

        public static VRCPlayer LocalVRCPlayer() => VRCPlayer.field_Internal_Static_VRCPlayer_0;
        public static APIUser GetAPIUser(this VRC.Player player) => player.prop_APIUser_0;
        public static float GetFrames(this Player player) => (player._playerNet.prop_Byte_0 != 0) ? Mathf.Floor(1000f / (float)player._playerNet.prop_Byte_0) : -1f;
        public static short GetPing(this Player player) => player._playerNet.field_Private_Int16_0;
        public static Player GetPlayer(this VRCPlayer player) => player.prop_Player_0;
        public static Player GetPlayerData(int ActorNumber)
        {
            return GetAllPlayers().Where(p => p.GetActorNumber() == ActorNumber).FirstOrDefault();
        }
        public static VRCPlayer GetVRCPlayer(this Player player) => player._vrcplayer;
        public static Color GetTrustColor(this VRC.Player player) => VRCPlayer.Method_Public_Static_Color_APIUser_0(player.GetAPIUser());
        public static APIUser GetAPIUser(this VRCPlayer Instance) => Instance.GetPlayer().GetAPIUser();
        public static VRCPlayerApi GetVRCPlayerApi(this Player Instance) => Instance?.prop_VRCPlayerApi_0;
        public static bool GetIsMaster(this Player Instance) => Instance.GetVRCPlayerApi().isMaster;
        public static int GetActorNumber(this Player player) => player.GetVRCPlayerApi() != null ? player.GetVRCPlayerApi().playerId : -1;
        public static void SetHide(this VRCPlayer Instance, bool State) => Instance.GetPlayer().SetHide(State);
        public static void SetHide(this Player Instance, bool State) => Instance.transform.Find("ForwardDirection").gameObject.active = !State;
        public static USpeaker GetUspeaker(this Player player) => player.prop_USpeaker_0;
        public static ulong GetSteamID(this Player player) => (player.GetVRCPlayer().field_Private_UInt64_0 > 10000000000000000UL) ? player.GetVRCPlayer().field_Private_UInt64_0 : ulong.Parse(player.GetPhotonPlayer().prop_Hashtable_0["steamUserID"].ToString());
        public static Photon.Realtime.Player GetPhotonPlayer(this Player player) => player.prop_Player_1;
        public static ApiAvatar GetAPIAvatar(this VRCPlayer vrcPlayer) => vrcPlayer.prop_ApiAvatar_0;
        public static ApiAvatar GetAPIAvatar(this Player player) => player.GetVRCPlayer().GetAPIAvatar();
        public static PlayerManager PManager => PlayerManager.field_Private_Static_PlayerManager_0;
        public static string GetDisplayName() => LocalPlayer().field_Private_APIUser_0._displayName_k__BackingField;
        public static string GetWorldID => LocalPlayer().GetAPIUser().location;
        public static string GetinstanceID => LocalPlayer().GetAPIUser().instanceId;
        public static VRCUiPopupManager GetVRCUiPopupManager() { return VRCUiPopupManager.prop_VRCUiPopupManager_0; }
        public static void AlertPopup(this VRCUiPopupManager manager, string title, string text) => manager.Method_Public_Void_String_String_Single_0(title, text, 10f);
        public static Player ReturnUserID(this string User)
        {
            foreach (Player player in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0)
            {
                if (player.field_Private_APIUser_0.id == User)
                    return player;
            }
            return null;
        }

        public static Transform camera()
        {
            return GameObject.Find("Camera (eye)").transform;
        }

        public static void ChangeAvi(string ID)
        {
            PageAvatar component = GameObject.Find("Screens").transform.Find("Avatar").GetComponent<PageAvatar>();
            component.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0 = new ApiAvatar { id = ID, };
            component.ChangeToSelectedAvatar();
        }

        internal static List<Player> GetAllPlayerslol()
        {
            return (PlayerManager.field_Private_Static_PlayerManager_0 == null) ? null : PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0.ToArray().ToList<Player>();
        }

        public static void Tele2MousePos()
        {
            Ray posF = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //pos, directon 
            RaycastHit[] PosData = Physics.RaycastAll(posF);
            if (PosData.Length > 0) { RaycastHit pos = PosData[0]; VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position = pos.point; }
        }
        public static string Hasclient(Player player)
        {
            string uhoh = "";
            if (player.GetFrames() > 170 || player.GetFrames() < 11 || player.GetPing() > 300 || player.GetPing() < 1 || player.transform.localPosition.y > 300 || player.transform.localPosition.y < -10)
            {
                uhoh = $">>><color=red>Client User</color><<<";
            }
            return uhoh;

        }

        public static string GetFramesColord(this Player player)
        {
            float fps = player.GetFrames();
            if (fps > 80)
                return "<color=green>" + fps + "</color>";
            else if (fps > 30)
                return "<color=yellow>" + fps + "</color>";
            else
                return "<color=red>" + fps + "</color>";
        }

        public static string GetUserID { get { return PlayerWrapper.LocalPlayer().GetAPIUser().id; } }

        public static APIUser SpoofDisplayname(string name)
        {
            PlayerWrapper.LocalPlayer().field_Private_APIUser_0._displayName_k__BackingField = name;
            return (APIUser)name;
        }

        public static bool IsInVR(this Player player)
        {
            return player.GetVRCPlayerApi().IsUserInVR();
        }

        public static string GetPingColord(this Player player)
        {
            short ping = player.GetPing();
            if (ping > 150)
                return "<color=red>" + ping + "</color>";
            else if (ping > 75)
                return "<color=yellow>" + ping + "</color>";
            else
                return "<color=green>" + ping + "</color>";
        }

        public static string GetPlatform(this Player player)
        {
            if (player.GetAPIUser().IsOnMobile) { return "<color=red>Q</color>"; }
            else if (player.GetVRCPlayerApi().IsUserInVR()) { return "<color=red>VR</color>"; }
            else { return "<color=red>PC</color>"; }
        }

        public static void ChangeAvatar(string AvatarID)
        {
            PageAvatar component = GameObject.Find("Screens").transform.Find("Avatar").GetComponent<PageAvatar>();
            component.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0 = new ApiAvatar
            {
                id = AvatarID
            };
            component.ChangeToSelectedAvatar();
        }

        public static Player GetPlayerByActorID(int actorId)
        {
            Player player = null;
            PlayersActorID.TryGetValue(actorId, out player);
            return player;
        }

        public static string UserID(this Player Instance)
        {
            return Instance.GetAPIUser().id;
        }

        public static bool IsInWorld() { return RoomManager.field_Internal_Static_ApiWorld_0 != null; }

        public static string LogRPC(Player sender, VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType vrcBroadcastType)
        {
            string text = "[RPC] ";
            try
            {
                text = ((!(sender != null)) ? (text + " INVISABLE sended ") : (text + sender.prop_APIUser_0.displayName + " sended "));
                text = text + vrcBroadcastType.ToString() + " ";
                text = text + vrcEvent.Name + " ";
                text = text + vrcEvent.EventType.ToString() + " ";
                if (vrcEvent.ParameterObject != null)
                {
                    text = text + vrcEvent.ParameterObject.name + " ";
                    text = text + vrcEvent.ParameterBool + " ";
                    text = text + vrcEvent.ParameterBoolOp.ToString() + " ";
                    text = text + vrcEvent.ParameterFloat + " ";
                    text = text + vrcEvent.ParameterInt + " ";
                    text = text + vrcEvent.ParameterString + " ";
                }
                if (vrcEvent.ParameterObjects != null)
                {
                    for (int i = 0; i < vrcEvent.ParameterObjects.Length; i++)
                    {
                        text = text + vrcEvent.ParameterObjects[i].name + " ";
                    }
                }
                Il2CppReferenceArray<Il2CppSystem.Object> il2CppReferenceArray = Networking.DecodeParameters(vrcEvent.ParameterBytes);
                for (int j = 0; j < il2CppReferenceArray.Length; j++)
                {
                    text = text + Il2CppSystem.Convert.ToString(il2CppReferenceArray[j]) + " ";
                }
                return text;
            }
            catch (Exception ex)
            {
                /*for (int k = 0; k < vrcEvent.ParameterBytes.Length; k++)
                {
                    text = text + vrcEvent.ParameterBytes[k] + " ";
                }*/
                return text;
            }
        }
        public static string backupID = "";

        public static VRCUiPopupInput keyboardPopup
        {
            get
            {
                return VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.field_Public_VRCUiPopupInput_0;
            }
        }

        public static string IsMaster(this Player player)
        {
            string Master = "";
            if (player.prop_VRCPlayerApi_0.isMaster)
            {
                Master = "<color=blue>M</color>";

            }
            return Master;

        }
        public static string IsMOD(this Player player)
        {
            string Master = "";
            if (player.prop_VRCPlayerApi_0.isModerator)
            {
                Master = "<color=red>MODDERATOR</color>";

            }
            return Master;

        }
        public static void MovePlayerLocPos(Vector3 pos)
        {
            PlayerWrapper.LocalPlayer().GetComponent<Transform>().localPosition = pos;
        }

    }
}
