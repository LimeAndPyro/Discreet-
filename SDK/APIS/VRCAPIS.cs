using Il2CppSystem.Collections.Generic;
using System;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;
using VRC;


namespace Discreet.SDK.APIS
{
    internal class VRCAPIS
    {
        
        internal static string vrcInputPopup(string name, string EnterText,  Action<string> outputresult, Action action, string Textboxtext)
        {
            string inputedstring = string.Empty;
            VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.
                Method_Public_Void_String_String_InputType_Boolean_String_Action_3_String_List_1_KeyCode_Text_Action_String_Boolean_Action_1_VRCUiPopup_Boolean_PDM_0(
                name, string.Empty, InputField.InputType.Standard, false, EnterText, DelegateSupport.ConvertDelegate<Il2CppSystem.Action<string, List<KeyCode>, Text>>(
                    new Action<string, List<KeyCode>, Text>(delegate (string Output, List<KeyCode> k, Text t)
            {
                outputresult(Output); action.Invoke();
            })), null, Textboxtext, true, null);
            return inputedstring;


        }
           
    }
}
