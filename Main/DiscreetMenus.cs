using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discreet.SDK.APIS;
using Discreet.SDK.Functions;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Discreet.SDK.LogUtillities;
using MelonLoader;
using Discreet.QOL;
using VRC.SDKBase;
using Discreet.SDK.Wrappers;
using UnhollowerRuntimeLib;

namespace Discreet.Main
{
    public class DiscreetMenus
    {
        public static GameObject Window = GameObject.Find("UserInterface/UnscaledUI/HudContent_Old/");
        public static GameObject GUIConsole;
        public static QMNestedButton World;
        public static QMNestedButton Player;
        public static QMNestedButton settings;
        public static QMNestedButton FunStuff;
        public static QMNestedButton Bots;
        public static QMNestedButton Safetys;
        public static QMNestedButton ColorSettings;
        public static QMNestedButton OddballShit;
        public static QMNestedButton EasyMod;
        public static void StartButtons()
        {
            //tab
            QMTabMenu Discreettab = new QMTabMenu("Opens Discreet Mod", "Discreet");

            //Nested Buttons
            World = new QMNestedButton(Discreettab, 1f, 0f, "World Tools", "Opens World Tools Menu", "World Tools", true);
            Player = new QMNestedButton(Discreettab, 3f, 0f, "Player Tools", "Opens Player Tools", "Player Tools", true);
            settings = new QMNestedButton(Discreettab, 4f, 0.5f, "Settings", "Opens Settigns", "Settings", true);
            FunStuff = new QMNestedButton(Discreettab, 3f, 0.5f, "Fun", "Opens Fun Test Features", "Fun", true);
            Bots = new QMNestedButton(Discreettab, 2f, 0.5f, "Bots", "Opens Bots Menu", "Bots", true);
            Safetys = new QMNestedButton(Discreettab, 1f, 0.5f, "Safety", "Opens Safety Tools", "Safety", true);
            ColorSettings = new QMNestedButton(settings, 2f, 3f, "ColorSettings", "Opens Color Settings", "ColorSettings", true);
            OddballShit = new QMNestedButton(settings, 1f, 3f, "Oddball   Shit", "Opens OddballShit", "OddballShit", true);
            EasyMod = new QMNestedButton(FunStuff, 2f, 3f, "EasyMod", "Opens EasyMod", "EasyMod", true);




            QmCollapse Collapse = new QmCollapse(LimesFunctions.ReturnVerticalLayoutGroup(FunStuff.GetMenuObject()), "Deez Nuts In Your Mouth");

            //QMSingleButton qMSingleButton = new QMSingleButton(LimesFunctions.ReturnGameobjectifActive(Collapse.ReturnGroup()), 1f, 0f, "LOL", delegate
            //{
            //    LogHandler.Log(ConsoleColor.DarkGreen, "WOW WOW wow", true);
            //}, "LOL");





            //Here we play with Nested button stuff
            World.GetMainButton().GetGameObject().transform.Find("Background").transform.localScale = new Vector3(2.1972f, 1f, 1f);
            World.GetMainButton().GetGameObject().transform.Find("Background").transform.localPosition = new Vector3(114.5636f, 0f, 0f);
            TextMeshProUGUI WorldText = World.GetMainButton().GetGameObject().transform.Find("Text_H4").GetComponent<TextMeshProUGUI>();
            WorldText.enableWordWrapping = false;
            WorldText.fontSize = 32;
            WorldText.autoSizeTextContainer = true;
            WorldText.gameObject.transform.localPosition = new Vector3(112.8736f, - 22f, 0f);
            Player.GetMainButton().GetGameObject().transform.Find("Background").transform.localScale = new Vector3(2.1972f, 1f, 1f);
            Player.GetMainButton().GetGameObject().transform.Find("Background").transform.localPosition = new Vector3(114.5636f, 0f, 0f);
            TextMeshProUGUI PlayerText = Player.GetMainButton().GetGameObject().transform.Find("Text_H4").GetComponent<TextMeshProUGUI>();
            PlayerText.enableWordWrapping = false;
            PlayerText.fontSize = 32;
            PlayerText.autoSizeTextContainer = true;
            PlayerText.gameObject.transform.localPosition = new Vector3(112.8736f, -22f, 0f);

            //Custom Gameobjects
            Sprite QMConsoleSprite = LimesFunctions.LoadSpriteFromFile(DirectoryHandling.ImagesDir + "\\ConsoleImage.png");
            GUIConsole = LimesFunctions.Createbox(Discreettab.GetMenuObject(), new Vector3(1f, - 162.3245f, 1f), new Vector3(0, 0, 0), new Vector3(8.9864f, 5.7471f, 1f), "Console_DiscreetAPI", new Color(0.2549f, 0.2784f, 0.4392f, 0.3f), QMConsoleSprite);
            GUIConsole.AddComponent<DiscreetGUIConsole>();

            //Minis
            QmMiniToggle MoveConsole = new QmMiniToggle(Discreettab.GetMenuObject(), new Vector3(473.7256f, -413.1389f, -7.4222f), new Vector3(0, 0, 90), new Vector3(0.6f, 0.6f, 0f), "MoveTocorner", "Moves Console To Top Right Corner", delegate
            {
                LimesFunctions.ChangeParent(GUIConsole, Window);
                LimesFunctions.ChangeLocalPosition(GUIConsole, new Vector3(310.1091f, -339.1273f, 0f));
                LimesFunctions.ChangeLocalScale(GUIConsole, new Vector3(3.7f, 2.5f, 2.5f));

            }, delegate
            {
                LimesFunctions.ChangeParent(GUIConsole, Discreettab.GetMenuObject());
                LimesFunctions.ChangeLocalPosition(GUIConsole, new Vector3(1f, -162.3245f, 1f));
                LimesFunctions.ChangeLocalScale(GUIConsole, new Vector3(8.9864f, 5.7471f, 1f));
                LimesFunctions.ChangeLocalRotation(GUIConsole, new Vector3(0, 0, 0));
            });

            //QMMiniButton MoveConsoleNormal = new QMMiniButton(Discreettab.GetMenuObject(), new Vector3(473.7256f, - 340.0173f, - 7.4222f), new Vector3(0, 0, 90), new Vector3(0.6f, 0.6f, 0f), "MoveToTab", "Moves Console To Top Right Corner", delegate
            //{
            //    LimesFunctions.ChangeParent(GUIConsole, Discreettab.GetMenuObject());
            //    LimesFunctions.ChangeLocalPosition(GUIConsole, new Vector3(1f, -162.3245f, 1f));
            //    LimesFunctions.ChangeLocalScale(GUIConsole, new Vector3(8.9864f, 5.7471f, 1f));
            //    LimesFunctions.ChangeLocalRotation(GUIConsole, new Vector3(0, 0, 0));

            //});



            //Drawers



            //Toggles


            //Singles
            QMSingleButton JoinWorldByID = new QMSingleButton(World, 1, 0, "JoinWorld\nByID", delegate
            {
                string roomid = "RoomID Here"; VRCAPIS.vrcInputPopup("Room Instance Id", "Join World", value => roomid = value, () =>
                Networking.GoToRoom(roomid), "WorldID Here");
            }, "Click me to JoinWorldByID!");
            QMSingleButton GetWorldID = new QMSingleButton(World, 2, 0, "GetWorld\nByID", delegate
            {
                if (WorldWrapper.GetLocation() != "")
                    LimesFunctions.SetClipboard(WorldWrapper.GetLocation());
                SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, $"Copied World ID => {WorldWrapper.GetLocation()}", false, true);

            }, "Click me to GetWorldID!");


            //Sliders


            //Starts And Voids
           
            CumCursor.CumCursorCreation();
            MelonCoroutines.Start(CumCursor.CumCursorAnimation());
            MelonCoroutines.Start(Flashlights.CreateGlowstickBody(true));
            ClassInjector.RegisterTypeInIl2Cpp<HudElements>();
            HudElements.CreatetheMonoGamobject();
            DisablingDumbshitihate.CreateButtons();
            QOL.EasyMod.CreateButtons();







        }
    }
}
