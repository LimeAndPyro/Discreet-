using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC;
using System.Windows.Forms;
using MelonLoader;

namespace Discreet.SDK.Functions
{
    public static class LimesFunctions
    {
        public static string fulldesc = "";

        public static AudioClip LoadFromWebclient(string location)
        {
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(location);
            unityWebRequest.SendWebRequest();
            if (!unityWebRequest.isDone) return null;
            if (unityWebRequest.error != null) SDK.LogUtillities.LogHandler.Log(ConsoleColor.Red, "Webrequest Error Path Invalid", false, false, false, true);
            AudioClip Clip = WebRequestWWW.InternalCreateAudioClipUsingDH(unityWebRequest.downloadHandler, unityWebRequest.url, false, false, AudioType.UNKNOWN);
            return Clip;
        }

        public static GameObject ReturnVerticalLayoutGroup(GameObject target)
        {
            GameObject VLG = target.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup").gameObject;
            return VLG;
        }

        public static void ChangeLocalPosition(GameObject TargetGameObject, Vector3 TargetPosition)
        {
            TargetGameObject.GetComponent<RectTransform>().localPosition = TargetPosition;
        }
        public static void ChangeLocalScale(GameObject TargetGameObject, Vector3 TargetScale)
        {
            TargetGameObject.GetComponent<RectTransform>().localScale = TargetScale;
        }
        public static void ChangeLocalRotation(GameObject TargetGameObject, Vector3 TargetRotation)
        {
            TargetGameObject.GetComponent<RectTransform>().localEulerAngles = TargetRotation;
        }
        public static void ChangeAnchoredPosition(GameObject TargetGameObject, Vector2 TargetPosition)
        {
            TargetGameObject.GetComponent<RectTransform>().anchoredPosition = TargetPosition;
        }
        public static void DestroyChildren(Transform ParentOfGameObject)
        {
            foreach (Transform Child in ParentOfGameObject.transform)
            {
                UnityEngine.Object.Destroy(Child.gameObject);
            }
        }
        public static void ChangeTMPText(GameObject TargetTextObject, string TargetText)
        {
            TargetTextObject.GetComponent<TextMeshProUGUI>().text = TargetText;
        }
        public static void DestroythisCompbcsimlazy(Component gameObject)
        {
            UnityEngine.Object.DestroyImmediate(gameObject);
        }

        public static void ChangeSpriteIcons(GameObject TargetSprite, Sprite NewSprite)
        {
            TargetSprite.GetComponent<Image>().overrideSprite = NewSprite;
        }
        public static Texture2D LoadImageToTexture(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(256, 256);
            bool image =  ImageConversion.LoadImage(texture, bytes);
            while (image == false)
            {
                Console.WriteLine("Data Wrong");
                return null;
            }
            return texture;
        }
        public static void SetClipboard(string Set)
        {
            if (Clipboard.ContainsText())
            {
                Clipboard.Clear();
                Clipboard.SetText(Set);
            }
            Clipboard.SetText(Set);
        }
        public static Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
        public static Texture2D LoadPNG(string filePath)
        {

            Texture2D tex = null;
            byte[] fileData;

            if (File.Exists(filePath))
            {
                fileData = File.ReadAllBytes(filePath);
                tex = new Texture2D(2, 2);
                ImageConversion.LoadImage(tex, fileData); //..this will auto-resize the texture dimensions.
            }
            return tex;
        }

        internal static Sprite LoadSpriteFromFile(string path)//Not Made By Me
        {
            byte[] array = File.ReadAllBytes(path);
            Texture2D texture2D = new Texture2D(256, 256);
            ImageConversion.LoadImage(texture2D, array);
            Rect rect = new Rect(0.0f, 0.0f, texture2D.width, texture2D.height);
            Vector2 vector = new Vector2(0.5f, 0.5f);
            Vector4 zero = Vector4.zero;
            return Sprite.CreateSprite_Injected(texture2D, ref rect, ref vector, 100f, 0, SpriteMeshType.Tight, ref zero, false);
        }
        //
        public static GameObject LoadBundleFromDisk(Transform parent, Vector3 position, Vector3 rotation, Vector3 scale, string objname, string location, string assetname)
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(location); ;
            if (bundle == null)
            {
                Console.WriteLine("Failed To Load Asset");
                return null;
            }
            GameObject obj = bundle.LoadAsset<GameObject>(assetname);
            GameObject gameObject = UnityEngine.Object.Instantiate(obj);
            gameObject.transform.parent = parent;
            gameObject.name = objname;
            gameObject.GetComponent<Transform>().localPosition = position;
            gameObject.GetComponent<Transform>().localEulerAngles = rotation;
            gameObject.GetComponent<Transform>().localScale = scale;
            return gameObject;
        }
        
        public static void OverrideSprite(Image image, Sprite sprite)
        {
            image.sprite = sprite;
            image.overrideSprite = sprite;
        }
        public static GameObject ReturnGameobjectifActive(GameObject gameObject)
        {
            while (gameObject == null) return null;
            return gameObject;
        }

        public static string WrapTextInHexColor(string HexCode, string Text)
        {
            return $"<color=#{HexCode}>{Text}</color>";
        }

        public static GameObject EasyInstantiate(GameObject target, GameObject parent)
        {
            GameObject Instatiated = UnityEngine.Object.Instantiate(ReturnGameobjectifActive(target), ReturnGameobjectifActive(parent).transform);
            return Instatiated;
            
        }
        public static void ChangeParent(GameObject gameObject, GameObject NewParent)
        {
            gameObject.transform.parent = NewParent.transform;
        }



        //


        public static GameObject Createbox(GameObject parent, Vector3 Position, Vector3 Rotation, Vector3 Scale, string name, Color color, Sprite sprite)
        {
            GameObject boxtemp = new GameObject();
            Image Box = boxtemp.AddComponent<Image>();
            Box.overrideSprite = sprite;
            boxtemp.transform.parent = parent.transform;
            boxtemp.GetComponent<RectTransform>().localEulerAngles = Rotation;
            boxtemp.GetComponent<RectTransform>().localScale = Scale;
            boxtemp.GetComponent<RectTransform>().localPosition = Position;
            boxtemp.name = name;
            Box.color = color;
            return boxtemp;
        }
        //
        public static GameObject Justblank(GameObject parent, Vector3 Position, Vector3 Rotation, Vector3 Scale, string name)
        {
            GameObject gameObject = new GameObject();
            gameObject.transform.parent = parent.transform;
            gameObject.transform.localEulerAngles = Rotation;
            gameObject.transform.localPosition = Position;
            gameObject.transform.localScale = Scale;
            gameObject.name = name;
            return gameObject;
        }

        public static GameObject CreateTextobject(GameObject parent, Vector3 Position, Vector3 Rotation, Vector3 scale, string text, string objname, bool wordwrapping, float fontsize, bool Isbold = false)
        {
            GameObject gameObject = new GameObject();
            TextMeshProUGUI textMeshPro = gameObject.AddComponent<TextMeshProUGUI>();
            textMeshPro.name = objname;
            textMeshPro.text = text;
            gameObject.transform.parent = parent.transform;
            gameObject.GetComponent<RectTransform>().localPosition = Position;
            gameObject.GetComponent<RectTransform>().localEulerAngles = Rotation;
            gameObject.GetComponent<RectTransform>().localScale = scale;
            textMeshPro.enableWordWrapping = wordwrapping;
            if (Isbold) textMeshPro.fontStyle = TMPro.FontStyles.Bold;
            textMeshPro.fontSize = fontsize;
            return gameObject;

        }
        

        public static string Grabwindowname(string processname)
        {
            Process[] processe = Process.GetProcessesByName(processname);
            foreach (Process process in processe)
            {
                if (!string.IsNullOrEmpty(process.MainWindowTitle))
                {

                    fulldesc = process.MainWindowTitle;
                }
            }
            return fulldesc;
        }

        public static void ChangeGameObjectColor(GameObject TargetObject, Color TargetColor, bool ImageorText)
        {
            if (ImageorText)
            {
                TargetObject.GetComponent<Image>().color = TargetColor;
            }
            else
            {
                TargetObject.GetComponent<TextMeshProUGUI>().color = TargetColor;
            }
        }

        public static void DestroyStyleElements(GameObject TargetToDestroy)
        {
            UnityEngine.Object.Destroy(TargetToDestroy.GetComponent<VRC.UI.Core.Styles.StyleElement>());
        }


        public static void SkipUpdate(GameObject SkipUpdateOnThis)
        {
            SkipUpdateOnThis.GetComponent<Image>().m_SkipLayoutUpdate = true;
            SkipUpdateOnThis.GetComponent<Image>().m_SkipMaterialUpdate = true;
        }


        //WORK IN PROGRESS
        public static void ChangeAllTabColors(Color AllTabBackground, Color AllTabIcon)
        {
            //Test Destroy
            DestroyStyleElements(GameObject.Find("Page_Dashboard"));
            DestroyStyleElements(GameObject.Find("Page_Notifications"));
            DestroyStyleElements(GameObject.Find("Page_Here"));
            DestroyStyleElements(GameObject.Find("Page_Camera"));
            DestroyStyleElements(GameObject.Find("Page_AudioSettings"));
            DestroyStyleElements(GameObject.Find("Page_Settings"));
            //Destroy Stupid Icon Style Elements 
            DestroyStyleElements(GameObject.Find("Page_Dashboard").transform.Find("Icon").gameObject);
            DestroyStyleElements(GameObject.Find("Page_Notifications").transform.Find("Icon").gameObject);
            DestroyStyleElements(GameObject.Find("Page_Here").transform.Find("Icon").gameObject);
            DestroyStyleElements(GameObject.Find("Page_Camera").transform.Find("Icon").gameObject);
            DestroyStyleElements(GameObject.Find("Page_AudioSettings").transform.Find("Icon").gameObject);
            DestroyStyleElements(GameObject.Find("Page_Settings").transform.Find("Icon").gameObject);
            //Destroy Stupid Background Style Elements 
            DestroyStyleElements(GameObject.Find("Page_Dashboard").transform.Find("Background").gameObject);
            DestroyStyleElements(GameObject.Find("Page_Notifications").transform.Find("Background").gameObject);
            DestroyStyleElements(GameObject.Find("Page_Here").transform.Find("Background").gameObject);
            DestroyStyleElements(GameObject.Find("Page_Camera").transform.Find("Background").gameObject);
            DestroyStyleElements(GameObject.Find("Page_AudioSettings").transform.Find("Background").gameObject);
            DestroyStyleElements(GameObject.Find("Page_Settings").transform.Find("Background").gameObject);
            //Change All Tab Icon Colors
            ChangeGameObjectColor(GameObject.Find("Page_Dashboard").transform.Find("Icon").gameObject, AllTabIcon, true);
            ChangeGameObjectColor(GameObject.Find("Page_Notifications").transform.Find("Icon").gameObject, AllTabIcon, true);
            ChangeGameObjectColor(GameObject.Find("Page_Here").transform.Find("Icon").gameObject, AllTabIcon, true);
            ChangeGameObjectColor(GameObject.Find("Page_Camera").transform.Find("Icon").gameObject, AllTabIcon, true);
            ChangeGameObjectColor(GameObject.Find("Page_AudioSettings").transform.Find("Icon").gameObject, AllTabIcon, true);
            ChangeGameObjectColor(GameObject.Find("Page_Settings").transform.Find("Icon").gameObject, AllTabIcon, true);
            //Change All Tab Background Colors
            ChangeGameObjectColor(GameObject.Find("Page_Dashboard").transform.Find("Background").gameObject, AllTabBackground, true);
            ChangeGameObjectColor(GameObject.Find("Page_Notifications").transform.Find("Background").gameObject, AllTabBackground, true);
            ChangeGameObjectColor(GameObject.Find("Page_Here").transform.Find("Background").gameObject, AllTabBackground, true);
            ChangeGameObjectColor(GameObject.Find("Page_Camera").transform.Find("Background").gameObject, AllTabBackground, true);
            ChangeGameObjectColor(GameObject.Find("Page_AudioSettings").transform.Find("Background").gameObject, AllTabBackground, true);
            ChangeGameObjectColor(GameObject.Find("Page_Settings").transform.Find("Background").gameObject, AllTabBackground, true);
            //Skip Updates On Icons
            SkipUpdate(GameObject.Find("Page_Dashboard").transform.Find("Icon").gameObject);
            SkipUpdate(GameObject.Find("Page_Notifications").transform.Find("Icon").gameObject);
            SkipUpdate(GameObject.Find("Page_Here").transform.Find("Icon").gameObject);
            SkipUpdate(GameObject.Find("Page_Camera").transform.Find("Icon").gameObject);
            SkipUpdate(GameObject.Find("Page_AudioSettings").transform.Find("Icon").gameObject);
            SkipUpdate(GameObject.Find("Page_Settings").transform.Find("Icon").gameObject);
            //Skip Updates On Icons
            SkipUpdate(GameObject.Find("Page_Dashboard").transform.Find("Background").gameObject);
            SkipUpdate(GameObject.Find("Page_Notifications").transform.Find("Background").gameObject);
            SkipUpdate(GameObject.Find("Page_Here").transform.Find("Background").gameObject);
            SkipUpdate(GameObject.Find("Page_Camera").transform.Find("Background").gameObject);
            SkipUpdate(GameObject.Find("Page_AudioSettings").transform.Find("Background").gameObject);
            SkipUpdate(GameObject.Find("Page_Settings").transform.Find("Background").gameObject);
        }

    }
}
