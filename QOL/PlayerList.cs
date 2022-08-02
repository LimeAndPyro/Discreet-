using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
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
    public class PlayerList : MonoBehaviour
    {

        public PlayerList(IntPtr ptr) : base(ptr) { }
        public static bool IsReady;
        public static GameObject Window = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window");
        public static GameObject PlayerlistPrefab;
        public static GameObject VideoBg;
        public static GameObject TEXT;
        public static GameObject Tint;
        public static GameObject Backboard;
        public static Image Tinter;
        public static VideoPlayer videoPlayer;
        public static RawImage RawImage;
        public static RenderTexture render;
        public static Sprite TinterImage;
        public static TextMeshProUGUI text;
        public static ScrollRect scrollRect;
        public static GameObject Scrollbox;
        public static string Url = "https://cdn.discordapp.com/attachments/997794269769244774/999953006814560296/DiscreetBg.mp4";
        public static bool isenabled = false;

        //Animation
        public static Vector3 StartPos = new Vector3(-1.6629f, 279.4678f, 1f);
        public static Vector3 EndPos = new Vector3(-1.6629f, 713.9772f, 1);
        public static float duration = 100f;
        public static float elapsed;
        public static float complete;

        public static Vector3 endposb = new Vector3(-415.3952f, 942.6801f, 1.0987f);
        public static Vector3 startposb = new Vector3(-430.9416f, 517.2505f, 1.0987f);
        public static GameObject hidelistgo;
        public static bool buttonready = false;
        public static GameObject Icon1;
        public static GameObject Icon2;
        public static float rot = 220;



        public static Player[] GetAllPlayers() => PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray();

        public void Start()
        { 
            while (!IsReady) return;
            TinterImage =  LimesFunctions.LoadSpriteFromFile(DirectoryHandling.ImagesDir + "//DiscreetPlayerListRounded.png");
            CreateButtons();
            VideoBg = new GameObject("VideoPlayerBG");
            VideoBg.transform.parent = Window.transform;
            VideoBg.transform.SetSiblingIndex(1);
            RawImage = VideoBg.AddComponent<RawImage>();
            render = new RenderTexture(512, 512, 16);
            videoPlayer = VideoBg.AddComponent<VideoPlayer>();
            videoPlayer.targetTexture = render;
            videoPlayer.aspectRatio = VideoAspectRatio.Stretch;
            videoPlayer.isLooping = true;
            videoPlayer.url = Url;
            RawImage.texture = render;
            UnityEngine.Object.DontDestroyOnLoad(VideoBg);
            VideoBg.GetComponent<RectTransform>().localScale = new Vector3(9.9643f, 4.4873f, 1f);
            Tint = new GameObject("Tinter");
            Tint.transform.parent = VideoBg.transform; 
            Tinter = Tint.AddComponent<Image>();
            Tinter.color = new Color(0.2549f, 0.2784f, 0.4392f, 0.58f);
            Tint.GetComponent<RectTransform>().localPosition = new Vector3(0.0873f, 1, 1);
            Tint.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            Tinter.sprite = TinterImage;
            Backboard = new GameObject("Viewport");
            Backboard.transform.parent = Tint.transform;
            Backboard.AddComponent<Image>();
            Backboard.GetComponent<Image>().color = new Color(0, 0, 0, 0.0774f);
            Backboard.AddComponent<Mask>();
            Backboard.GetComponent<RectTransform>().localPosition = new Vector3(-25.3835f, 1, 1);
            Backboard.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0f, 0f);
            Backboard.GetComponent<RectTransform>().localScale = new Vector3(-0.4563f, 0.8946f, 10);
            Scrollbox = new GameObject("LeftScroll");
            Scrollbox.transform.parent = Backboard.transform;
            Scrollbox.AddComponent<Image>();
            Scrollbox.GetComponent<RectTransform>().localPosition = new Vector3(0.9456f, 2.1931f, 1f);
            Scrollbox.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0f, 0f);
            Scrollbox.GetComponent<RectTransform>().localScale = new Vector3(-1.0381f, 0.9983f, 0.5f);
            scrollRect = Scrollbox.AddComponent<ScrollRect>();
            Scrollbox.GetComponent<Image>().raycastTarget = true;
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            Scrollbox.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            Scrollbox.AddComponent<VRC_UiShape>();
            TEXT = new GameObject("Text");
            TEXT.AddComponent<TextMeshProUGUI>();
            text = TEXT.GetComponent<TextMeshProUGUI>();
            TEXT.transform.parent = Scrollbox.transform;
            TEXT.GetComponent<RectTransform>().localPosition = new Vector3(49.5617f, - 0.0012f, - 38.5382f);
            TEXT.GetComponent<RectTransform>().localScale = new Vector3(0.8328f, 1, 1);
            scrollRect.content = TEXT.GetComponent<RectTransform>();
            text.fontSize = 4.5f;
            isenabled = false;
           
        }
        public void Update()
        {
            while (!IsReady) return;
            while (!VideoBg) return;
            while (text == null) return;
            string Playerstring = "";
            foreach (Player player in GetAllPlayers())
            {
                Playerstring += PlayerWrapper.IsMaster(player);
                Playerstring += "|" + " <color=#" + ColorUtility.ToHtmlStringRGB(player.GetTrustColor()) + ">" + player.prop_APIUser_0.displayName + "</color>";
                Playerstring += "{" + player.GetFramesColord() + "||" + player.GetPingColord() + "}";
                Playerstring += " => " + PlayerWrapper.GetPlatform(player);
                Playerstring += PlayerWrapper.Hasclient(player) + "\n";
            }
            text.text = Playerstring;

            if (Input.GetKey(KeyCode.P))
            {
                isenabled = !isenabled;
            }

            if (!isenabled)
            {
              VideoBg.SetActive(false);
            }
            else
            {
              VideoBg.SetActive(true);
            }
            Icon1.transform.Rotate(0, 0, rot * Time.deltaTime);
            Icon2.transform.Rotate(0, 0, rot * Time.deltaTime);
            Icon1.SetActive(true);



        }
        public static IEnumerator WaitForAnitmation(bool on)
        {
            elapsed += Time.deltaTime;
            complete = elapsed / duration;
            while (!buttonready) yield return null; 
            if (on)
            {
                hidelistgo.transform.localPosition = Vector3.Lerp(endposb, startposb, complete);
                VideoBg.transform.localPosition = Vector3.Lerp(EndPos, StartPos, complete);
                isenabled = true;
            }
            else
            {
                hidelistgo.transform.localPosition = Vector3.Lerp(startposb, endposb, complete);
                VideoBg.transform.localPosition = Vector3.Lerp(StartPos, EndPos, complete);
                isenabled = false;
            }
            yield return null;
            

        }
        public static void CreatetheMonoGamobject()
        {
            GameObject go = new GameObject();
            go.name = "DiscreetPlayerlist";
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Created Playerlist Components", false, true);
            go.AddComponent<PlayerList>();
            DontDestroyOnLoad(go);
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Playerlist Ready", false, true);
            IsReady = true;

        }
        public static void CreateButtons()
        {
            Sprite book =  LimesFunctions.LoadSpriteFromFile(DirectoryHandling.ImagesDir + "//bookmark.png");
            Sprite i1 =  LimesFunctions.LoadSpriteFromFile(DirectoryHandling.ImagesDir + "//Playerlisttab.png");
            QMSingleButton ChangeUrl = new QMSingleButton(DiscreetMenus.settings, 1, 0, "Change\nPlayerlist\nURL", () =>
            {
                Url = "URL Here"; VRCAPIS.vrcInputPopup("Video URL", "Change BG", value => Url = value, () =>
                videoPlayer.url = Url, "URL Here");
            }, "Change Playerlist Video URL");

            QmMiniToggle HideList = new QmMiniToggle(Window, startposb, new Vector3(0, 0, 0), new Vector3(1.1473f, 2.1315f, 0.7f), "CollapseList", "Closes Playerlist", delegate
            {
                MelonLoader.MelonCoroutines.Start(WaitForAnitmation(true));

            }, delegate
            {
                MelonLoader.MelonCoroutines.Start(WaitForAnitmation(false));

            }, i1, i1, book);
            hidelistgo = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/CollapseList-LimesAPI");
            hidelistgo.transform.SetAsFirstSibling();
            hidelistgo.AddComponent<VRC_UiShape>();
            Icon2 = hidelistgo.transform.Find("Icon_Secondary").gameObject;
            Icon1 = hidelistgo.transform.Find("Icon").gameObject;
            Icon1.transform.GetComponent<RectTransform>().localPosition = new Vector3(-19.8727f, 9.4756f, 1);
            Icon1.transform.GetComponent<RectTransform>().localScale = new Vector3(0.2985f, - 1.4597f, 6.3273f);
            Icon2.transform.GetComponent<RectTransform>().localPosition = new Vector3(-19.8727f, 9.4756f, 1);
            Icon2.transform.GetComponent<RectTransform>().localScale = new Vector3(1.6f, 1.6f, 1.6f);
            Icon2.GetComponent<Image>().color = Color.clear;

            buttonready = true;


        }

    }
}
