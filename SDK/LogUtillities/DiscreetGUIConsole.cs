using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Discreet.SDK.Functions;
using Discreet.Main;
using Discreet.SDK.APIS;
using UnityEngine.UI;
using System.Collections;
using VRC;
using Discreet.SDK.Wrappers;
using Discreet.QOL;
using TMPro;

namespace Discreet.SDK.LogUtillities
{//LIMES CONSOLE CODE Lime/Pyro/Creed#9739
    public class DiscreetGUIConsole : MonoBehaviour
    {
        public DiscreetGUIConsole(IntPtr ptr) : base(ptr) { }
        public static GameObject CheckNull = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)");
        public static GameObject StreamerModeHeader = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMNotificationsArea/Header_StreamerMode/Header");
        public static GameObject TitleText;
        public static GameObject Clock;
        public static GameObject LoggedIn;
        public static GameObject Logger;
        private static int duplicateCount = 1;
        public static int ConsoleLimit = 100;
        public static string Lastmessage = "";
        public static List<string> DebugLogs = new List<string>();
        public static GameObject Viewport;
        public static GameObject ScrollRect;
        public static ScrollRect Scroll;

        public void Start()
        {
            TitleText = LimesFunctions.CreateTextobject(LimesFunctions.ReturnGameobjectifActive(DiscreetMenus.GUIConsole), new Vector3(36.933f, 15.0646f, 1f), new Vector3(0, 0, 0), new Vector3(0.8509f, 1.3436f, 1f), LimesFunctions.WrapTextInHexColor("700CB3", "Discreet Console"), $"DiscreetConsole{LoadButton.APINAME}", false, 3.2f, true);
            Clock = LimesFunctions.CreateTextobject(LimesFunctions.ReturnGameobjectifActive(DiscreetMenus.GUIConsole), new Vector3(105.8628f, 15.1082f, 1f), new Vector3(0, 0, 0), new Vector3(0.8509f, 1.3436f, 1f), LimesFunctions.WrapTextInHexColor("700CB3", ""), $"DiscreetCLock{LoadButton.APINAME}", false, 3f, false);
            LoggedIn = LimesFunctions.CreateTextobject(LimesFunctions.ReturnGameobjectifActive(DiscreetMenus.GUIConsole), new Vector3(36.933f, -84.7718f, 1f), new Vector3(0, 0, 0), new Vector3(0.8509f, 1.3436f, 1f), $"Logged In As: {LimesFunctions.WrapTextInHexColor("700CB3", $"{Player.prop_Player_0.field_Private_APIUser_0.displayName}")}", $"DiscreetUserLoggedIn{LoadButton.APINAME}", false, 3f, false);
            Viewport = new GameObject("ViewPort");
            Viewport.transform.parent = DiscreetMenus.GUIConsole.transform;
            Viewport.AddComponent<Image>();
            Viewport.GetComponent<RectTransform>().localPosition = new Vector3(0.0909f, -2.0909f, 1f);
            Viewport.GetComponent<RectTransform>().localEulerAngles = new Vector3(0,0,0);
            Viewport.GetComponent<RectTransform>().localScale = new Vector3(0.9745f, 0.8764f, 1f);
            Viewport.AddComponent<Mask>();
            Viewport.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.0121f);
            ScrollRect = new GameObject("ScrollRect");
            ScrollRect.transform.parent = Viewport.transform;
            ScrollRect.AddComponent<Image>();
            ScrollRect.GetComponent<RectTransform>().localPosition = new Vector3(-1.9455f, 16.5402f, 1f);
            ScrollRect.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 0);
            ScrollRect.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            ScrollRect.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.0121f);
            Scroll = ScrollRect.AddComponent<ScrollRect>();
            Logger = LimesFunctions.CreateTextobject(LimesFunctions.ReturnGameobjectifActive(ScrollRect), new Vector3(37.9352f, 0.021f, - 20.8987f), new Vector3(0, 0, 0), new Vector3(0.8509f, 1.3436f, 1f), "", $"Discreet Logger{LoadButton.APINAME}", false, 3f, false);
            Scroll.content = Logger.GetComponent<RectTransform>();
            Scroll.horizontal = false;
            Scroll.vertical = true;


            LogHandler.Log(ConsoleColor.Cyan, "Injected Console", false, true);
        }
        public void Update()
        {
            LimesFunctions.ChangeTMPText(Clock, DateTime.Now.ToString($"{LimesFunctions.WrapTextInHexColor("700CB3", "hh:mm:ss")} | ") + DateTime.Now.ToString("yyyy:MM:dd"));

        }
        public static void MessageConsoleOnClient(string Hexcolor, string LogMessage)
        {
            if (LogMessage == Lastmessage)
            {
                if (duplicateCount >= ConsoleLimit)
                {
                    Logger.GetComponent<TextMeshProUGUI>().text = "\n";
                }
                DebugLogs.RemoveAt(DebugLogs.Count - 1);
                duplicateCount++;
                DebugLogs.Add($"<b>[<color=#700CB3>{DateTime.Now.ToString("hh:mm tt")}</color>] {LimesFunctions.WrapTextInHexColor(Hexcolor, LogMessage)} <color=#700CB3><i>x{duplicateCount}</i></color></b>");
            }
            else
            {
                Lastmessage = LogMessage;
                duplicateCount = 1;
                DebugLogs.Add($"<b>[<color=#700CB3>{DateTime.Now.ToString("hh:mm tt")}</color>] {LimesFunctions.WrapTextInHexColor(Hexcolor, LogMessage)}</b>");
                
            }
            Logger.GetComponent<TextMeshProUGUI>().text = string.Join("\n", DebugLogs.Take(28));

        }
    }
}
