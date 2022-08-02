using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Discreet.SDK.Functions;
using UnityEngine.UI;
using Discreet.SDK.Config;
using Discreet.SDK.LogUtillities;
using Discreet.QOL;

namespace Discreet.Main
{
    public class DiscreetDecore
    {
        public static Color BorderColor = new Color(0.2549f, 0.2784f, 0.4392f, 0.1196f);
        public static Color GUICOLOR = new Color(0.2549f, 0.2784f, 0.4392f, 0.7f);
        public static bool Override = true;



        //Sprites
        public static Sprite QMBg = LimesFunctions.LoadSpriteFromFile(DirectoryHandling.ImagesDir + "//DiscreetRoundedBG.png");
        public static void QMdecore()
        {
            if (Override)
            {
                
                GameObject QMgameobject = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer01");
                QMgameobject.GetComponent<Image>().overrideSprite = QMBg;
                //GameObject QmWingR = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Right/Button");
                //QmWingR.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 120);
                //QmWingR.GetComponent<RectTransform>().localPosition = new Vector3(-17.1627f, - 98.2076f, 0f);
                //GameObject QmWingL = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Button");
                //QmWingL.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 120);
                //QmWingL.GetComponent<RectTransform>().localPosition = new Vector3(23.3501f, - 100.2912f, 0f);
                RawImage Bannerimage = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners/Image_MASK/Image/Banners/IPS_Template_Banner(Clone)").GetComponent<RawImage>();
                Bannerimage.texture = LimesFunctions.LoadPNG(DirectoryHandling.ImagesDir + "//Testbanner.png");
                GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMNotificationsArea/Notifications/SocialNotificationsOverlay").GetComponent<RectTransform>().localPosition = new Vector3(2072.818f, -377.6818f, 0f);
                  


            }
        }
        
        

    }
}
