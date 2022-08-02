using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Discreet.SDK.Functions;
using UnityEngine.EventSystems;
using VRC.UI.Elements.Controls;
using UnityEngine.Events;
using TMPro;
using VRC.UI.Elements;

namespace Discreet.SDK.APIS
{
    public static class APIInfo
    {
        public static string ID = "_LimesApi";
    }
    class QmCollapse
    {
        public static GameObject CollapseTemplate = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/QM_Foldout_PerUseInteractions");
        public static GameObject collapsable;
        public static Toggle GroupToggle;
        public static Action<bool> toggleaction;
        public static int NumberOfGroup;
        public static HorizontalLayoutGroup HorizontalLayoutGroup;
        public static GameObject Group;

        
        public QmCollapse(GameObject parent, string Name)
        {
            collapsable = LimesFunctions.EasyInstantiate(CollapseTemplate, parent);
            collapsable.name = Name + APIInfo.ID;
            Group = new GameObject();
            collapsable.transform.Find("Label").GetComponent<TextMeshProUGUI>().text = Name;
            GroupToggle = collapsable.transform.Find("Background_Button").GetComponent<Toggle>();
            GroupToggle.onValueChanged = new Toggle.ToggleEvent();
            GroupToggle.onValueChanged.AddListener(toggleaction);
            toggleaction = delegate (bool state)
            {
                if (state)
                {
                    Group.gameObject.SetActive(true);
                }
                else
                {
                    Group.gameObject.SetActive(false);
                }
            };
        }
        public GameObject ReturnGroup()
        {
            return ReturnGameobjectifActive(Group);
        }

        public GameObject ReturnCollapseGameObject()
        {
            return ReturnGameobjectifActive(collapsable);
        }
        public GameObject ReturnGameobjectifActive(GameObject gameObject)
        {
            while (gameObject == null) return null;
            return gameObject;
        }

    }
    
}
