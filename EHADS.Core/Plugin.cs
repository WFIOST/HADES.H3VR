using System.Collections;
using BepInEx;
using BepInEx.Logging;
using EHADS.Common;
using FistVR;
using UnityEngine;
using static EHADS.Common.Logging;
using PluginInfo = EHADS.Common.PluginInfo;

namespace EHADS.Core
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.NAME, PluginInfo.VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource ConsoleLogger;
        public Plugin()
        {
            ConsoleLogger = Logger;
            Print($"Loading EHADS version {PluginInfo.VERSION}");
        }


        private void Start()
        {
            GM.CurrentPlayerBody.gameObject.AddComponent<EHADS>();
            Print($"Loaded EHADS version {PluginInfo.VERSION}!");
        }

        private void Update()
        {

        }
    }
}