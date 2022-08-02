using System.Collections.Generic;
using UnityEngine;
using VRC.Core;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;

namespace Discreet.SDK.Wrappers
{
    class WorldWrapper
    {
        public static string GetInstance() => CurrentWorldInstance().instanceId;
        public static string GetID() => CurrentWorld().id;
        public static string GetLocation() => PlayerWrapper.LocalPlayer().GetAPIUser().location;
        public static ApiWorld CurrentWorld() => RoomManager.field_Internal_Static_ApiWorld_0;
        internal static ApiWorldInstance GetCurrentInstance() => RoomManager.field_Internal_Static_ApiWorldInstance_0;

        public static ApiWorldInstance CurrentWorldInstance() => RoomManager.field_Internal_Static_ApiWorldInstance_0;
        public static List<string> NonSendable = new List<string>();
        public static List<string> Sendable = new List<string>();
        public static VRCMirrorReflection[] WorldMirrors = UnityEngine.Object.FindObjectsOfType<VRCMirrorReflection>();
        public static VRCPickup[] AllPickups = UnityEngine.Object.FindObjectsOfType<VRCPickup>();
    }
}
