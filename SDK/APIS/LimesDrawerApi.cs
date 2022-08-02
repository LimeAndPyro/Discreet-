using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Discreet.SDK.APIS
{
    public class QMdrawer
    {
        public static string ApiId = "Limes_API";
        //Gameobjects 
        public static GameObject Drawer;
        public static GameObject DrawerButton;
        public static List<GameObject> allDrawerGameObjects = new List<GameObject>();
        public static GameObject InnerContainer = Drawer.transform.Find("Container/InnerContainer").gameObject; 

        //Start of function
        public QMdrawer(Vector3 position, Vector3 rotation, Vector3 scale, string GamobjName,Sprite Icon = null)
        {
            Drawer = UnityEngine.Object.Instantiate(DrawerApiExtensions.ReturnifNotnull(), DrawerApiExtensions.ReturnifNotnull().transform.parent);
            Drawer.transform.GetComponent<RectTransform>().localPosition = position;
            Drawer.transform.GetComponent<RectTransform>().localScale = scale;
            Drawer.transform.GetComponent<RectTransform>().localEulerAngles = rotation;
            Drawer.name = GamobjName + ApiId;
            Drawer.transform.Find("Button/Icon").GetComponent<Image>().sprite = Icon;

        }
    }
    //References
    public class DrawerApiExtensions
    {
        public static GameObject Canvas = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)");
        public static GameObject DrawerTemplate;
        public static GameObject ReturnifNotnull()
        {
            while (Canvas != null && DrawerTemplate != null) return null;
            DrawerTemplate = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Right");
            return DrawerTemplate;
        }
       
        
    }

}
