using UnityEngine;
using System.Collections;
using System;
using MelonLoader;
using Discreet.SDK.LogUtillities;
using Discreet.SDK.Functions;
using Discreet.Main;
using VRC;
using Discreet.SDK.Wrappers;
using Discreet.SDK.Patching;

namespace Discreet.SDK.APIS.GUI
{
    public class DiscreetGUI : MonoBehaviour
    {
        public DiscreetGUI(IntPtr ptr) : base(ptr) { }
        public static Sprite background = LimesFunctions.LoadSpriteFromFile(DirectoryHandling.ImagesDir + "\\GUIBackROUND.png");
        internal bool IsLocked = false;
        internal GUIStyle BoxStyle = new GUIStyle();
        internal string Clock;
        internal string Frames;

        public void Start()
        {
            if (Player.prop_Player_0 != null)
            {
                LogUtillities.LogHandler.Log(ConsoleColor.Green, "Player Initialized", false, true);
            }
        }


        public void OnGUI()
        {
            if (Player.prop_Player_0 == null) return;
            if (Input.GetKey(KeyCode.Tab))
            {
                UnityEngine.GUI.Box(new Rect(660, 940, 600, 70), "", BoxStyle);
                BoxStyle.normal.background = LimesFunctions.LoadPNG(DirectoryHandling.ImagesDir + "\\GUIBackROUND.png");
                UnityEngine.GUI.Label(new Rect(1125, 920, 600, 70), Clock);
                if (UnityEngine.GUI.Button(new Rect(670, 947, 55, 55), "", Easystyle("\\magnet.png")))
                {
                    LogUtillities.LogHandler.Log(ConsoleColor.Green, "ButtonWorks", false, true);
                }
                if (UnityEngine.GUI.Button(new Rect(720, 947, 60, 60), "", Easystyle("\\Playerlist.png")))
                {
                    LogUtillities.LogHandler.Log(ConsoleColor.Green, "ButtonWorks", false, true);
                }
            }
            



            if (Player.prop_Player_0 != null)
            {//needed to try and catch bcs of init waiting
                try
                {
                    UnityEngine.GUI.Label(new Rect(25, 20, 600, 100), $"Logged In As: {LimesFunctions.WrapTextInHexColor("fa37ed", $"{Player.prop_Player_0.field_Private_APIUser_0.displayName} => {Frames}")}");
                    UnityEngine.GUI.Label(new Rect(25, 40, 600, 100), PlayerPatches.RecentlyJoined);
                    UnityEngine.GUI.Label(new Rect(25, 60, 600, 100), PlayerPatches.RecentlyLeft);
                }
                catch { }
            }
        }

        public void Update()
        {
            Clock = DateTime.Now.ToString($"{LimesFunctions.WrapTextInHexColor("201A4B", "hh:mm:ss")} | ") + DateTime.Now.ToString("yyyy:MM:dd");

        }
        public static GUIStyle Easystyle(string imgname)
        {
            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.normal.background = LimesFunctions.LoadPNG(DirectoryHandling.ImagesDir + imgname);
            return gUIStyle;
        }
        public static void CreatetheMonoGamobject()
        {
            GameObject go = new GameObject();
            go.name = "DiscreetGUI";
            LogUtillities.LogHandler.Log(ConsoleColor.Green, "Created GUI Menu", false, true);
            go.AddComponent<DiscreetGUI>();
            LogUtillities.LogHandler.Log(ConsoleColor.Green, "Injected GUI MonoBehaviour", false, true);
            DontDestroyOnLoad(go);
        }

    }
}
