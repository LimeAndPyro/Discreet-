using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using MelonLoader;
using Discreet.SDK.Config;
using TMPro;

namespace Discreet.QOL
{
    public class CumCursor
    {
        public static GameObject Hud = GameObject.Find("UserInterface/UnscaledUI/HudContent_Old/Hud");
        public static GameObject Cum;
        public static TextMeshProUGUI Cumtexxt;
        public static bool Isready = false;
        public static IEnumerator CumCursorAnimation()
        {
            while (true)
            {
                if (Isready && ConfigSetup.GetConfigParam().Cum)
                {
                    Cumtexxt = Cum.GetComponent<TextMeshProUGUI>();
                    Cumtexxt.text = "";
                    yield return new WaitForSeconds(1f);
                    Cumtexxt.text = "C";
                    yield return new WaitForSeconds(1f);
                    Cumtexxt.text = "CU";
                    yield return new WaitForSeconds(1f);
                    Cumtexxt.text = "CUM";
                    yield return new WaitForSeconds(1f);
                }
                yield return null;
            }
           
            
        }
        public static void CumCursorCreation()
        {

            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Cum Cursor Started", false, true);
            Cum = new GameObject();
            Cum.name = "CumCursor";
            Cum.transform.parent = Hud.transform;
            Cum.AddComponent<TextMeshProUGUI>();
            Cum.GetComponent<RectTransform>().localPosition = new Vector3(56.7129f, 1f, 1f);
            Cum.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            Cum.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 0);
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Cum Cursor Created", false, true);
            Isready = true;
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Cum Cursor Ready", false, true);
            UnityEngine.Object.DontDestroyOnLoad(Cum);


        }
    }
}
