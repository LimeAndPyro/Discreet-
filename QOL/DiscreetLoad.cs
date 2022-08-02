using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Discreet.SDK.Config;
using Discreet.SDK.Functions;
using Discreet.Main;
using UnityEngine.Networking;
using Discreet.SDK.APIS;
using System.Diagnostics;

namespace Discreet.QOL
{
    public class DiscreetLoad
    {
        public static int loadcheck;
        public static GameObject Badload = GameObject.Find("UserInterface/LoadingBackground_TealGradient_Music");
        public static AudioSource load;
        public static void CleanLoad()
        {
            if (ConfigSetup.GetConfigParam().LoadClean)
            {
                if (Badload != null) return;
                SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Cleaning LoadScreen", false, true);
                GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingInfoPanel/InfoPanel_Template_ANIM").gameObject.SetActive(false);
                GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked").gameObject.SetActive(false);
                GameObject.Find("UserInterface/LoadingBackground_TealGradient_Music/SkyCube_Baked").gameObject.SetActive(false);
                GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_FX_ParticleBubbles/FX_snow").GetComponent<ParticleSystem>().startColor = Color.blue;
                GameObject.Find("UserInterface/LoadingBackground_TealGradient_Music/_FX_ParticleBubbles/FX_snow").GetComponent<ParticleSystem>().startColor = Color.blue;
                GameObject LoadPanel = GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress");
                GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/ButtonMiddle").GetComponent<Image>().color = Color.blue;
                foreach (Image image in LoadPanel.GetComponentsInChildren<Image>())
                {
                    image.color = Color.blue;
                }
                SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "LoadScreen Cleaned", false, true);
                AudioClip Loadclip = LimesFunctions.LoadFromWebclient(DirectoryHandling.AudioDir + "\\Load.ogg");
                load = GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/LoadingSound").GetComponent<AudioSource>();
                MelonLoader.MelonCoroutines.Start(LoadAudio());
                load.Play();    
                
            }
            
        }
        public static IEnumerator LoadAudio()
        {
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Attempting To Change Audio", false, true);
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(DirectoryHandling.AudioDir + "\\Load.ogg");
            unityWebRequest.SendWebRequest();
            while (!unityWebRequest.isDone) yield return null;
            while (load == null) yield return null;
            AudioClip song = WebRequestWWW.InternalCreateAudioClipUsingDH(unityWebRequest.downloadHandler, unityWebRequest.url, false, false, AudioType.OGGVORBIS);
            load.clip = song;
            load.volume = .6f;
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Audio Changed", false, true);
        }
        public static void LoadButtons()
        {
            LoadButton Restart = new LoadButton(new Vector3(1331.77f, -459.6257f, 0f), new Vector3(1, 1, 1), "Restart", "RestartLoadButton", delegate
            {
                Process.Start("vrchat.exe", Environment.CommandLine.ToString()); Process.GetCurrentProcess().Kill();
            }, Color.white, Color.blue);

            LoadButton CloseGame = new LoadButton(new Vector3(-1322.886f, -461.9034f, 0f), new Vector3(1, 1, 1), "Close Game", "CloseGameLoadButton", delegate
            {
                Process.GetCurrentProcess().Kill();
            }, Color.white, Color.blue);
        }
    }
}
