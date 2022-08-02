using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Discreet.SDK.Functions;
using UnityEngine.EventSystems;
using VRC.UI.Elements.Controls;
using UnityEngine.Events;
using VRC.UI.Elements;
using Discreet.SDK.LogUtillities;
using Discreet.Main;

namespace Discreet.SDK.APIS
{
    class QmMiniToggle
    {
        public static GameObject MiniTemplate = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Right/Container/InnerContainer/Friends/Cell_Wing_Toolbar/Button_PageDown");

        public static Image MiniIcon1on;
        public static Image MiniIconoff;
        public static GameObject MiniToggle;
        public static List<GameObject> AllMiniToggles = new List<GameObject>();
        public static string APINAME = "-LimesAPI";
        public static bool Ison = false;
        public static Action OnActionConnect;
        public static Action OffActionConnect;

        public QmMiniToggle(GameObject parent, Vector3 pos, Vector3 rot, Vector3 scale, string name, string Tooltip, Action btnactionOn, Action btnactionOff, Sprite IconOn = null, Sprite IconOff = null, Sprite Background = null)
        {

            MiniToggle = QMMiniButton.LimesAPIFunctions.EasyInstantiate(MiniTemplate, parent);
            MiniToggle.name = name + APINAME;
            MiniToggle.GetComponent<RectTransform>().localPosition = pos;
            MiniToggle.GetComponent<RectTransform>().localEulerAngles = rot;
            MiniToggle.GetComponent<RectTransform>().localScale = scale;
            LimesAPIFunctions.SetToolTip(Tooltip, MiniToggle);
            MiniIcon1on = MiniToggle.transform.Find("Icon").GetComponent<Image>();
            GameObject secondimage = MiniToggle.transform.Find("Icon_Secondary").gameObject;
            secondimage.transform.parent = MiniToggle.transform;
            secondimage.GetComponent<RectTransform>().localPosition = new Vector3(0.9236f, 18.9419f, 0f);
            secondimage.GetComponent<RectTransform>().localEulerAngles = new Vector3(0f, 0f, 0f);
            secondimage.GetComponent<RectTransform>().localScale = new Vector3(0.6309f, 0.5547f, 1f);
            MiniIconoff = secondimage.GetComponent<Image>();
            MiniIcon1on.overrideSprite = IconOn;
            MiniIcon1on.sprite = IconOn;
            MiniIconoff.overrideSprite = IconOff;
            MiniIconoff.sprite = IconOff;
            LimesAPIFunctions.OverrideSprite(MiniToggle.transform.Find("Background").GetComponent<Image>(), Background);
            AllMiniToggles.Add(LimesAPIFunctions.ReturnButtonObj());
            LimesAPIFunctions.DestroythisCompbcsimlazy(MiniToggle.GetComponent<LayoutElement>());
            LimesAPIFunctions.DestroythisCompbcsimlazy(MiniToggle.GetComponent<CanvasGroup>());
            LimesAPIFunctions.DestroythisCompbcsimlazy(MiniToggle.GetComponent<UiTooltip>());
            LimesAPIFunctions.DestroythisCompbcsimlazy(MiniToggle.GetComponent<ScrollRectPageButton>());
            LimesAPIFunctions.DestroythisCompbcsimlazy(MiniToggle.GetComponent<UiTooltip>());
            MiniToggle.AddComponent<Button>();
            MiniToggle.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            MiniToggle.GetComponent<Button>().onClick.AddListener(new Action(HandleFunction));
            OnActionConnect = btnactionOn;
            OffActionConnect = btnactionOff;

        }
        private void HandleFunction()
        {
            Ison = !Ison;
            if (Ison)
            {
                MiniIcon1on.gameObject.SetActive(true);
                MiniIconoff.gameObject.SetActive(false);
                OnActionConnect.Invoke();

            }
            else
            {
                MiniIcon1on.gameObject.SetActive(false);
                MiniIconoff.gameObject.SetActive(true);
                OffActionConnect.Invoke();
            }

        }

        public static class LimesAPIFunctions
        {
            public static GameObject ReturnButtonObj()
            {
                return MiniToggle;
            }
            public static void SetToolTip(string buttonToolTip, GameObject gameObject)//EDITED BLAZES API FUNCTION!!!  
            {
                gameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = buttonToolTip;
                gameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_1 = buttonToolTip;
            }
            public static void OverrideSprite(Image image, Sprite sprite)
            {
                image.sprite = sprite;
                image.overrideSprite = sprite;
            }
            public static void ChangeColor(Image image, Color color)
            {
                image.color = color;
            }
            public static GameObject ReturnGameobjectifActive(GameObject gameObject)
            {
                while (gameObject == null) return null;
                return gameObject;
            }
            public static GameObject EasyInstantiate(GameObject target, GameObject parent)
            {
                GameObject Instantiated = UnityEngine.Object.Instantiate(ReturnGameobjectifActive(target), ReturnGameobjectifActive(parent).transform);
                return Instantiated;

            }
            public static void DestroythisCompbcsimlazy(Component gameObject)
            {
                UnityEngine.Object.DestroyImmediate(gameObject);
            }
        }
    }
    //teeny weeny button api created by Lime/Pyro/Creed#1212
    class QMMiniButton
    {
        public static GameObject MiniTemplate = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Right/Container/InnerContainer/Friends/Cell_Wing_Toolbar/Button_PageDown");
        public static Image MiniIcon;
        public static GameObject MiniButton;
        public static List<GameObject> AllMinis = new List<GameObject>();
        public static string APINAME = "-LimesAPI";
        public QMMiniButton(GameObject parent, Vector3 pos, Vector3 rot, Vector3 scale, string name, string Tooltip, Action btnaction, Sprite Icon = null, Sprite Background = null)
        {
            MiniButton = LimesFunctions.EasyInstantiate(MiniTemplate, parent);
            MiniButton.name = name + APINAME;
            MiniButton.GetComponent<RectTransform>().localPosition = pos;
            MiniButton.GetComponent<RectTransform>().localEulerAngles = rot;
            MiniButton.GetComponent<RectTransform>().localScale = scale;
            LimesAPIFunctions.SetToolTip(Tooltip, MiniButton);
            MiniIcon = MiniButton.transform.Find("Icon").GetComponent<Image>();
            LimesAPIFunctions.OverrideSprite(MiniIcon, Icon);
            LimesAPIFunctions.OverrideSprite(MiniButton.transform.Find("Background").GetComponent<Image>(), Background);
            LimesAPIFunctions.DestroythisCompbcsimlazy(MiniButton.GetComponent<LayoutElement>());
            LimesAPIFunctions.DestroythisCompbcsimlazy(MiniButton.GetComponent<CanvasGroup>());
            LimesAPIFunctions.DestroythisCompbcsimlazy(MiniButton.GetComponent<UiTooltip>());
            LimesAPIFunctions.DestroythisCompbcsimlazy(MiniButton.GetComponent<ScrollRectPageButton>());
            MiniButton.AddComponent<Button>();
            MiniButton.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            if (btnaction != null)
            {
               MiniButton.GetComponent<Button>().onClick.AddListener(UnhollowerRuntimeLib.DelegateSupport.ConvertDelegate<UnityAction>(btnaction));
            }
        }
        public static class LimesAPIFunctions
        {
            public static GameObject ReturnButtonObj()
            {
                return MiniButton;
            }
            public static void SetToolTip(string buttonToolTip, GameObject gameObject)//EDITED BLAZES API FUNCTION!!!  
            {
                gameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = buttonToolTip;
                gameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_1 = buttonToolTip;
            }
            public static void OverrideSprite(Image image, Sprite sprite)
            {
                image.sprite = sprite;
                image.overrideSprite = sprite;
            }
            public static void ChangeColor(Image image, Color color)
            {
                image.color = color;
            }
            public static GameObject ReturnGameobjectifActive(GameObject gameObject)
            {
                while (gameObject == null) return null;
                return gameObject;
            }
            public static GameObject EasyInstantiate(GameObject target, GameObject parent)
            {
                GameObject Instantiated = UnityEngine.Object.Instantiate(ReturnGameobjectifActive(target), ReturnGameobjectifActive(parent).transform);
                return Instantiated;

            }
            public static void DestroythisCompbcsimlazy(Component gameObject)
            {
                UnityEngine.Object.DestroyImmediate(gameObject);
            }
        }
        
    }
    
    
}
