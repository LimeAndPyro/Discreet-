using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Discreet.SDK.Wrappers;
using VRC;

namespace Discreet.QOL
{
    public class HudElements : MonoBehaviour
    {
        public HudElements(IntPtr ptr) : base(ptr) { }
        public static GameObject FPS;
        public static TextMeshProUGUI FpsCounter;
        public static GameObject PING;
        public static TextMeshProUGUI PingCounter;
        public static bool IsReady = false;
        public void Start()
        {
            if (IsReady == false) return;
            FPS = new GameObject("FpsCounter");
            FPS.AddComponent<TextMeshProUGUI>();
            FpsCounter = FPS.GetComponent<TextMeshProUGUI>();
            FPS.transform.parent = GameObject.Find("UserInterface/UnscaledUI/HudContent_Old/Hud/").transform;
            FPS.GetComponent<RectTransform>().localPosition = new Vector3(-239.981f, - 339.1997f, 1f);
            FPS.GetComponent<RectTransform>().localEulerAngles = new Vector3(0f, 0f, 0f);
            FPS.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            FpsCounter.fontSize = 32;
            PING = new GameObject("PingCounter");
            PING.AddComponent<TextMeshProUGUI>();
            PingCounter = PING.GetComponent<TextMeshProUGUI>();
            PING.transform.parent = GameObject.Find("UserInterface/UnscaledUI/HudContent_Old/Hud/").transform;
            PingCounter.GetComponent<RectTransform>().localPosition = new Vector3(-214.1628f, - 311.7187f, 1f);
            PingCounter.GetComponent<RectTransform>().localEulerAngles = new Vector3(0f, 0f, 0f);
            PingCounter.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            PingCounter.fontSize = 22;


        }
        public void Update()
        {
            if (IsReady == false) return;
            while (Player.prop_Player_0 == null) return;
            FpsCounter.text = $"F:{PlayerWrapper.GetFramesColord(Player.prop_Player_0)}";
            PingCounter.text = $"P:{PlayerWrapper.GetPingColord(Player.prop_Player_0)}";
        }
        public static void CreatetheMonoGamobject()
        {
            GameObject go = new GameObject();
            go.name = "Hud Elements";
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Created Hud Components", false, true);
            go.AddComponent<HudElements>();
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Injected Original Hud", false, true);
            DontDestroyOnLoad(go);
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Hud Elements Ready", false, true);
            IsReady = true;
        }
    }
}
