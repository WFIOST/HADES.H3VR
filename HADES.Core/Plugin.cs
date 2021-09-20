using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using FistVR;
using UnityEngine.SceneManagement;
using static HADES.Utilities.Logging;
using PluginInfo = HADES.Utilities.PluginInfo;

namespace HADES.Core
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.NAME, PluginInfo.VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource ConsoleLogger;

        private static Plugin _mod;

        public Plugin()
        {
            _mod = this;
            ConsoleLogger = Logger;
            Print($"Loading HADES version {PluginInfo.VERSION}");

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Start()
        {
            Print($"Loaded HADES version {PluginInfo.VERSION}!");
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            var trycount = 0;

            load:
            try
            {
                GM.CurrentPlayerBody.gameObject.AddComponent<HADES>();
                Print($"Loaded HADES after {trycount} tries");
            }
            catch (NullReferenceException e)
            {
                ++trycount;
                goto load;
            }
        }

        public static ConfigEntry<T> BindConfig<T>(string section,
                                                   string key,
                                                   T      defaultValue,
                                                   string description) =>
            _mod.Config.Bind
            (
                section,
                key,
                defaultValue,
                description
            );
    }
}