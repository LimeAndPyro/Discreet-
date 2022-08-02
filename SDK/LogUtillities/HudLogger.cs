using UnityEngine;
using UnityEngine.UI;
using MelonLoader;
using System.Collections;
using TMPro;
using Discreet.SDK.Functions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Discreet.SDK.LogUtillities
{
    public class HudLogger
    {
        public static GameObject Template = GameObject.Find("/UserInterface").transform.Find("UnscaledUI/HudContent_Old/Hud/AlertTextParent/Capsule").gameObject;
        public static GameObject Capsule;
        public static GameObject LOG;
        public static TextMeshProUGUI LogText;
        public static string ClientLogTemplate = $"[{LimesFunctions.WrapTextInHexColor("468499", DateTime.Now.ToString("hh:mm tt"))}] {LimesFunctions.WrapTextInHexColor("700CB3", "{Discreet}")}";
        public static ContentSizeFitter fitter;
        public static VerticalLayoutGroup _VerticalLayoutGroup;
        public static Image LogSprite;
        private static int duplicateCount = 1;
        public static int ConsoleLimit = 100;
        public static string Lastmessage = "";
        public static List<string> DebugLogs = new List<string>();
        public static bool IsDisplayed = false;
        public static int Played5times;

        public static void CreateLoggerElement()
        {
            Capsule = LimesFunctions.EasyInstantiate(Template, Template.transform.parent.transform.parent.gameObject);
            Capsule.name = "DiscreetHudLogger";
            Capsule.SetActive(true);
            LOG = Capsule.transform.Find("Text").gameObject;
            LogText = LOG.GetComponent<TextMeshProUGUI>();
            LimesFunctions.DestroythisCompbcsimlazy(Capsule.GetComponent<ImageThreeSlice>());
            LimesFunctions.DestroythisCompbcsimlazy(Capsule.GetComponent<ContentSizeFitter>());
            LimesFunctions.DestroythisCompbcsimlazy(Capsule.GetComponent<HorizontalLayoutGroup>());
            fitter = Capsule.GetComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            _VerticalLayoutGroup = Capsule.AddComponent<VerticalLayoutGroup>();
            LogText.alignment = TextAlignmentOptions.BottomLeft;
            _VerticalLayoutGroup.spacing = 22f;



        }
        public static void Log(string Hexcolor, string text, string Subject = null, bool alert = false, float timebeforefade = 1.5f)
        {
            if (text == Lastmessage)
            {

                DebugLogs.RemoveAt(DebugLogs.Count - 1);
                duplicateCount++;
                DebugLogs.Add($"{ClientLogTemplate} {LimesFunctions.WrapTextInHexColor(Hexcolor, text)} <color=#700CB3><i>x{duplicateCount}</i></color></b>");
                if (Subject != null) DebugLogs.Add($"{ClientLogTemplate}[{Subject}]=>{LimesFunctions.WrapTextInHexColor(Hexcolor, text)} <color=#700CB3><i>x{duplicateCount}</i></color></b>");
            }
            else
            {
                Lastmessage = text;
                duplicateCount = 1;
                DebugLogs.Add($"{ClientLogTemplate}=>{LimesFunctions.WrapTextInHexColor(Hexcolor, text)}</b>");
                if (Subject != null) DebugLogs.Add($"{ClientLogTemplate}[{Subject}]=>{LimesFunctions.WrapTextInHexColor(Hexcolor, text)}</b>");
                if (DebugLogs.Count > 16)
                {
                    DebugLogs.RemoveAt(0);
                }
            }
            LogText.text = string.Join("\n", DebugLogs.Take(28));
            IsDisplayed = true;
            MelonCoroutines.Start(Wait(timebeforefade));
            
        }
        public static IEnumerator Wait(float time, float timebeforefade = 5f, bool alert = false)   
        {
            yield return new WaitForSeconds(time);
            MelonCoroutines.Start(Fade(LogText, timebeforefade, alert));
            yield return null;
        }

        public static IEnumerator Fade(TextMeshProUGUI target, float Timebeforefade, bool Alert = false)
        {

            if (!Alert || Played5times == 5)
            {
                for (float i = 0; i <= 1; i -= Time.deltaTime)
                {
                    target.color = new Color(target.color.r, target.color.g, target.color.b, i);
                    yield return null;
                }
            }

        }
    }
}
