using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Discreet.SDK.Functions;
using Discreet.Main;
using UnityEngine.Video;
using Discreet.SDK.APIS;
using Discreet.SDK.Config;
using TMPro;
using Discreet.SDK.Wrappers;
using VRC;
using VRCSDK2;


namespace Discreet.QOL
{
    public class MediaControlls : MonoBehaviour
    {
        //I was lazier on this
        //lol still works
        public MediaControlls(IntPtr ptr) : base(ptr) { }
        public static bool IsReady = false;
        public static GameObject Window = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window");
        public static bool isenabled;
        public static bool buttonready = false;
        public static GameObject frame;
        public static Vector3 OpenPos = new Vector3(-116.7941f, 584.2069f, -23.9904f);
        public static Vector3 ClosedPos = new Vector3(-116.7941f, 421.0077f, -23.9904f);
        public static Vector3 OpenPosb = new Vector3(-346.8828f, 685.2837f, 1);
        public static Vector3 ClosedPosb = new Vector3(-346.8828f, 517.7391f, 1f);
        public static bool cheaptoggle;
        public static float duration = 1f;
        public static float elapsed;
        public static float complete;
        public static GameObject Button;
        public static GameObject cd;
        public static float rot = 50;
        public static GameObject MenuCd;


        public void Start()
        {
            Sprite frameimg = LimesFunctions.LoadSpriteFromFile(DirectoryHandling.ImagesDir + "//MediaControlbg.png");
            Sprite cdimg = LimesFunctions.LoadSpriteFromFile(DirectoryHandling.ImagesDir + "//RetroCd.png");

            if (!IsReady) return;
            CreateButtons();
            frame = new GameObject("DiscreetMedia_Frame/Holder");
            frame.transform.parent = Window.transform;
            frame.AddComponent<Image>();
            frame.GetComponent<Image>().sprite = frameimg;
            frame.GetComponent<Image>().color = new Color(0.2549f, 0.2784f, 0.4392f, 1);
            frame.GetComponent<RectTransform>().localScale = new Vector3(5.8091f, 1.6691f, 1);
            frame.GetComponent<RectTransform>().localEulerAngles = new Vector3(0f, 0f, 0);
            frame.transform.SetSiblingIndex(3);
            MenuCd = new GameObject("CdIcon");
            MenuCd.transform.parent = frame.transform;
            MenuCd.AddComponent<Image>();
            MenuCd.GetComponent<Image>().sprite = cdimg;
            MenuCd.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
            MenuCd.GetComponent<RectTransform>().localEulerAngles = new Vector3(356.0847f, 287.0219f, 292.5593f);
            MenuCd.GetComponent<RectTransform>().localPosition = new Vector3(-34.5419f, 1f, 1f);
            isenabled = true;

        }
        public void Update()
        {
            if (!IsReady) return;
            if (!isenabled)
            {
                frame.SetActive(false);
            }
            else
            {
                frame.SetActive(true);
            }
            if (cheaptoggle)
            {
                MelonLoader.MelonCoroutines.Start(WaitForAnitmation(true));
            }
            else
            {
                MelonLoader.MelonCoroutines.Start(WaitForAnitmation(false));

            }
            cd.transform.Rotate(0, 0, rot * Time.deltaTime);
            MenuCd.transform.Rotate(0, 0, rot * Time.deltaTime);


        }
        public static void CreatetheMonoGamobject()
        {
            GameObject go = new GameObject();
            go.name = "DiscreetMedia";
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Created DiscreetMedia Components", false, true);
            go.AddComponent<MediaControlls>();
            DontDestroyOnLoad(go);
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "DiscreetMedia Ready", false, true);
            IsReady = true;

        }

        public static IEnumerator WaitForAnitmation(bool on)
        {
            elapsed += Time.deltaTime;
            complete = elapsed / duration;
            while (!buttonready) yield return null;
            if (on)
            {
                frame.transform.localPosition = Vector3.Lerp(ClosedPos, OpenPos, complete);
                Button.transform.localPosition = Vector3.Lerp(ClosedPosb, OpenPosb, complete);
                isenabled = true;
            }
            else
            {
                frame.transform.localPosition = Vector3.Lerp(OpenPos, ClosedPos, complete);
                Button.transform.localPosition = Vector3.Lerp(OpenPosb, ClosedPosb, complete);
                isenabled = false;
            }
            yield return null;


        }
        public static void CreateButtons()
        {
            Sprite book = LimesFunctions.LoadSpriteFromFile(DirectoryHandling.ImagesDir + "//bookmark.png");
            Sprite Cd = LimesFunctions.LoadSpriteFromFile(DirectoryHandling.ImagesDir + "//CD.png");
            QMMiniButton Hidecontrolls = new QMMiniButton(Window, new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector3(1.1473f, 2.1315f, 0.7f), "CollapseMedia", "Closes MediaControlls", delegate
            {
                cheaptoggle = !cheaptoggle;

            }, Cd, book);
            Button = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/CollapseMedia-LimesAPI");
            Button.transform.SetSiblingIndex(2);
            Button.AddComponent<VRC_UiShape>();
            cd = Button.transform.Find("Icon").gameObject;
            cd.transform.GetComponent<RectTransform>().localScale = new Vector3(0.6419f, -0.6382f, 0.5f);
            cd.transform.GetComponent<RectTransform>().localPosition = new Vector3(-16.5182f, 6.5575f, 0);


            buttonready = true;


        }
    }
}
