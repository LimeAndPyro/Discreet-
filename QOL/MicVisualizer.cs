using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Discreet.SDK.Functions;
using VRC;
using Discreet.SDK.LogUtillities;
using System.Collections;

namespace Discreet.QOL
{   
    public class MicVisualizer
    {
        public static GameObject Template = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_AudioSettings/Content/Mic/InputLevel/Sliders/MicLevelSlider/Slider");
        public static GameObject Voicedotparent = GameObject.Find("UserInterface/UnscaledUI/HudContent_Old/Hud/");
        public static GameObject NewVis;
        public static IEnumerator CreateVis()
        {
            //this is based off of https://github.com/xavion-lux/MicLevelVisualizer Thanks xavi
            if (Player.prop_Player_0 == null) yield return null;
            NewVis = LimesFunctions.EasyInstantiate(Template, Voicedotparent);
            NewVis.name = "Mic Vis";
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Created Visual Mic Components => Thank to Xavi For The Original Idea <3", false, true);
            NewVis.GetComponent<RectTransform>().localPosition = new Vector3(-302.082F, -415.6728f, 1f);
            NewVis.GetComponent<RectTransform>().localEulerAngles = new Vector3(340.9442f, 345.4547f, 94.8493f);
            NewVis.GetComponent<RectTransform>().localScale = new Vector3(0.0932f, 0.0046f, 0f);
            NewVis.transform.Find("Fill Area/Fill/").GetComponent<Image>().color = Color.blue;
            NewVis.transform.Find("Background").GetComponent<Image>().color = new Color(0.566f, 0.3812f, 0.5886f, 0.1684f);
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Set Visual Mic New Position", false, true);
            yield return new WaitForSeconds(4f);
            UnityEngine.Object.DontDestroyOnLoad(NewVis);
            Template.GetComponent<Slider>().onValueChanged.AddListener(new Action<float>((changed) => NewVis.GetComponent<Slider>().value = changed));
            yield return new WaitForSeconds(5f);
            ConsoleArt.ConsoleClear();
            yield break;


        }
    }
}
