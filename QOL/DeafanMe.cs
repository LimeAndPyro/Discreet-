using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Discreet.SDK.Functions;
using Discreet.Main;
using Discreet.SDK.LogUtillities;
using UnityEngine.Audio;
using VRC;
using Discreet.SDK.Wrappers;
using Discreet.SDK.Patching;
//Created By Lime/Pyro/Creed#9739 
namespace Discreet.QOL
{
    public class DeafanMePlease : MonoBehaviour
    {
        public DeafanMePlease(IntPtr ptr) : base(ptr) { }
        public static GameObject DeafanOn;
        public static GameObject DeafanOff;
        public static GameObject Template = GameObject.Find("UserInterface/UnscaledUI/HudContent_Old/Hud/VoiceDotParent/VoiceDotDisabled");
        public static Image DeafanOnSprite;
        public static Image DeafanOffSprite;
        public static UnityEngine.AudioListener Ears = GameObject.Find("_Application/TrackingVolume/TrackingSteam(Clone)/SteamCamera/[CameraRig]/Neck/Camera (head)/Camera (ears)").GetComponent<AudioListener>();
        public static bool IsDeafened = false;
        public static GameObject load = GameObject.Find("UserInterface/LoadingBackground_TealGradient_Music");
        public static bool IsReady = false;

        public static Player[] GetAllPlayers() => PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0.ToArray();

        public static void togglePlayerExistingAudiosources(bool enabled)
        {
            foreach (Player player in GetAllPlayers())
            {
                if (player.prop_APIUser_0.IsSelf) return;
                AudioSource allplayeraudio = player.gameObject.transform.Find("AnimationController/HeadAndHandIK/HeadEffector/USpeak").GetComponent<AudioSource>();
                if (!enabled) allplayeraudio.enabled = false;
                else allplayeraudio.enabled = true;
                    
            }
            
        }
        public static void togglePlayerJoinedAudiosources(bool enabled)
        {
            foreach (Player player in PlayerPatches.allplayersjoinandleave)
            {
                if (player.prop_APIUser_0.IsSelf) return;
                AudioSource allplayeraudio = player.gameObject.transform.Find("AnimationController/HeadAndHandIK/HeadEffector/USpeak").GetComponent<AudioSource>();
                if (!enabled) allplayeraudio.enabled = false;
                else allplayeraudio.enabled = true;

            }

        }

        public void Start()
        {
            Sprite DeafenedSprite = LimesFunctions.LoadSpriteFromFile(DirectoryHandling.ImagesDir + "//defeaned.png");
            Sprite UnDeafenedSprite = LimesFunctions.LoadSpriteFromFile(DirectoryHandling.ImagesDir + "//Headphone.png");
            DeafanOn = LimesFunctions.EasyInstantiate(Template, Template.transform.parent.gameObject);
            DeafanOn.name = "DeafanOn";
            DeafanOn.GetComponent<RectTransform>().localPosition = new Vector3(-49.2364f, 56.056f, 0f);
            DeafanOn.GetComponent<RectTransform>().localScale = new Vector3(0.3074f, 0.3236f, 1f);
            DeafanOn.GetComponent<RectTransform>().eulerAngles = new Vector3(352.2182f, 9.1059f, 0f);
            DeafanOnSprite = DeafanOn.GetComponent<Image>();
            DeafanOnSprite.overrideSprite = DeafenedSprite;
            DeafanOnSprite.enabled = true;
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Loaded DeafanOn", false, true);
            DeafanOff = LimesFunctions.EasyInstantiate(Template, Template.transform.parent.gameObject);
            DeafanOff.name = "DeafanOff";
            DeafanOff.GetComponent<RectTransform>().localPosition = new Vector3(-49.2364f, 56.056f, 0f);
            DeafanOff.GetComponent<RectTransform>().localScale = new Vector3(0.3074f, 0.3236f, 1f);
            DeafanOff.GetComponent<RectTransform>().eulerAngles = new Vector3(352.2182f, 9.1059f, 0f);
            DeafanOffSprite = DeafanOff.GetComponent<Image>();
            DeafanOffSprite.overrideSprite = UnDeafenedSprite;
            DeafanOffSprite.enabled = true;
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Loaded DeafanOff", false, true);

            

        }
        public void Update()
        {
            if (!IsReady && load) return;
            try
            {
                if (load)
                {
                     DeafanOn.SetActive(false);
                    DeafanOff.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.M))
                {
                    IsDeafened = !IsDeafened;
                }
                if (IsDeafened)
                {
                    DeafanOn.SetActive(true);
                    DeafanOff.SetActive(false);
                    DeafanOnSprite.enabled = true;
                    DeafanOffSprite.enabled = false;
                    togglePlayerExistingAudiosources(false);
                    

                }
                else
                {
                    DeafanOn.SetActive(false);
                    DeafanOff.SetActive(true);
                    DeafanOnSprite.enabled = false;
                    DeafanOffSprite.enabled = true;
                    togglePlayerExistingAudiosources(true);
                    



                }
            }
            catch { }
            

        }
        public static void CreatetheMonoGamobject()
        {
            GameObject go = new GameObject();
            go.name = "DeafanMe";
                SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Created DeafanMe Components", false, true);
            go.AddComponent<DeafanMePlease>();
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Injected Audio Listeners", false, true);
            DontDestroyOnLoad(go);
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "DeafanMe Ready", false, true);
            IsReady = true;


        }

    }
}
