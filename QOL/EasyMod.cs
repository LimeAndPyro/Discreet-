using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Discreet.SDK.APIS;
using Discreet.Main;
using Discreet.SDK.LogUtillities;

namespace Discreet.QOL
{
    public static class EasyMod
    {
        public static void CreateButtons()
        {
            //Singles
            QMSingleButton EasyDestroy = new QMSingleButton(DiscreetMenus.EasyMod, 1, 0, "Destroy\nGameObject", delegate
            {
                string Gameobject = "Path here"; VRCAPIS.vrcInputPopup("Easy Destroy", "Destroy", value => Gameobject = value, () =>
                UnityEngine.Object.Destroy(GameObject.Find(Gameobject)), "GameObject Path");
            }, "Easily Destroy Gameobject By Its Path");
        }
       
        
    }
}
