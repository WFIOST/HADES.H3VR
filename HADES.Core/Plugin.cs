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
            Print($"Loading EHADS version {PluginInfo.VERSION}");
            
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            load:
            try
            {
                GM.CurrentPlayerBody.gameObject.AddComponent<HADES>();
            }
            catch (NullReferenceException e)
            {
                goto load;
            }
            
        }

        private void Start()
        {
            
            Print($"Loaded EHADS version {PluginInfo.VERSION}!");
        }
 
        public static ConfigEntry<T> BindConfig<T>( string  section,
                                                    string  key,
                                                    T       defaultValue,
                                                    string  description)
        {
            return _mod.Config.Bind
            (
                section,
                key,
                defaultValue,
                description
            );
        }
    }
}