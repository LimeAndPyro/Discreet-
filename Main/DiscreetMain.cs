using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using Discreet.SDK.Patching;
using Discreet.SDK.LogUtillities;
using Discreet.SDK.Functions;
using UnhollowerRuntimeLib;
using Discreet.SDK.Config;
using Discreet.SDK.APIS.GUI;
using Discreet.QOL;

namespace Discreet.Main
{
    public class DiscreetMain : MelonMod
    {
        public override void OnApplicationStart()
        {
            
            ConsoleArt.ConsoleStart(ConsoleColor.DarkBlue);
            DirectoryHandling.CreateDirectorys();
            LogHandler.Log(ConsoleColor.Green, "Started Discreet", false, true);
            ConfigSetup.LoadGeneralConfig();
            LogHandler.Log(ConsoleColor.Green, "Loaded Configurations", false, true);
            Initpatches.Start();
            ClassInjector.RegisterTypeInIl2Cpp<DiscreetGUIConsole>();
            ClassInjector.RegisterTypeInIl2Cpp<DiscreetGUI>();
            
            MelonCoroutines.Start(OnVRCUI());
            MelonCoroutines.Start(OnGameInit());
            MelonCoroutines.Start(BetterWorldinit());
        }
        //
        public override void OnUpdate()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.I)) HudLogger.Log("9E40E6", "My Cock Hurts", "ALERT");


        }


        //
        public override void OnLevelWasLoaded(int level)
        {
            if (level == -1)
            {
                
                ConfigSetup.SaveConfig();
                LogHandler.Log(ConsoleColor.Magenta, "Saved Configuration", false, true);
                LogHandler.Log(ConsoleColor.Cyan, "Scene Loaded");
                //ConsoleArt.ConsoleClear();
            }
            if (level == 2)
            {
                ConfigSetup.LoadGeneralConfig();
                LogHandler.Log(ConsoleColor.Magenta, "Loaded Configuration", false, true);
                LogHandler.Log(ConsoleColor.Blue, "Scene Unloaded");
            }

        }
        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            try
            {
                DiscreetLoad.CleanLoad();
                DiscreetLoad.LoadButtons();
            }
            catch { }
               
            
        }
        public IEnumerator BetterWorldinit()
        {
            while (RoomManager.field_Internal_Static_ApiWorld_0 == null) yield return null;
            yield return new WaitForSeconds(1.5f);
            WorldInit();
            yield break;
        }
        public void WorldInit()
        {
            MelonCoroutines.Start(MicVisualizer.CreateVis());
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Mic Visualizer Ready", false, true);


        }

        public IEnumerator OnGameInit() 
        { 
            while (GameObject.Find("UserInterface") == null) yield return null;
            GameInit();
            yield break;
        }

        public void GameInit()
        {
            ClassInjector.RegisterTypeInIl2Cpp<DeafanMePlease>();
            ClassInjector.RegisterTypeInIl2Cpp<Cursortweaks>();
            DiscreetGUI.CreatetheMonoGamobject();
            DeafanMePlease.CreatetheMonoGamobject();
            Cursortweaks.CreatetheMonoGamobject();

            
        }
        //
        public IEnumerator OnVRCUI()
        {
            while (GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)") == null) yield return null; //If Qm Exists
            new WaitForSeconds(0.7f);//Waits to Prevent Breakage
            UIIsInit();//Calls UI Changes
            yield break;//Breaks Statement
        }
        //
        public void UIIsInit()
        {
            DiscreetMenus.StartButtons();
            DiscreetDecore.QMdecore();
            HudLogger.CreateLoggerElement();
            ClassInjector.RegisterTypeInIl2Cpp<PlayerList>();
            PlayerList.CreatetheMonoGamobject();
            ClassInjector.RegisterTypeInIl2Cpp<MediaControlls>();
            MediaControlls.CreatetheMonoGamobject();

        }
        //
        public override void OnApplicationQuit()
        {
            ConfigSetup.SaveConfig();
            Process.GetCurrentProcess().Kill();
        }
        
    }
}
    