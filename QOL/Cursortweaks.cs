using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Discreet.SDK.Config;
using Discreet.SDK.LogUtillities;

namespace Discreet.QOL
{
    public class Cursortweaks : MonoBehaviour
    {
        public Cursortweaks(IntPtr ptr) : base(ptr) { }
        public static GameObject trail;
        public static SpriteRenderer curser = GameObject.Find("_Application/CursorManager/MouseArrow/VRCUICursorIcon").GetComponent<SpriteRenderer>();
        public static TrailRenderer trailRenderer;
        public static float timestart;
        public static Color LerpColor = Color.Lerp(Color.blue, Color.cyan, Mathf.Sin(Time.time - timestart) * 1f);
        public static bool IsReady;

        public void Start()
        {
            //thanks mimic for inspo and code rerwitten 
            timestart = Time.time;
            if (!ConfigSetup.GetConfigParam().isCursor) return;
            trail = new GameObject();
            trail.transform.parent = curser.gameObject.transform;
            trail.name = "CursorBRR";
            trailRenderer = trail.AddComponent<TrailRenderer>();
            trailRenderer.time = -1f;
            trailRenderer.time = 0.5f;
            trailRenderer.startWidth = .02f;
            trailRenderer.endWidth = 0f;
            trailRenderer.numCapVertices = 54;
            
            
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Magenta, $"Cursor Created Thanks Mimic For Original Code <3", false, true);
        }
        public void Update()
        {
            trailRenderer.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            curser.color = LerpColor;
            trailRenderer.sharedMaterial = new Material(Shader.Find("Unlit/Color"))
            {
                color = LerpColor
            };
            
        }
        public static void CreatetheMonoGamobject()
        {
            GameObject go = new GameObject();
            go.name = "Cursor Tweaks";
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Created Cursor Components", false, true);
            go.AddComponent<Cursortweaks>();
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Injected Original Cursor", false, true);
            DontDestroyOnLoad(go);
            SDK.LogUtillities.LogHandler.Log(ConsoleColor.Green, "Cursor Tweaks Ready", false, true);
            IsReady = true;


        }

    }
}
