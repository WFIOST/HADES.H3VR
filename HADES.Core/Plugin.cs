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
        public static ManualLogSource ConsoleLogger { get; private set; }
        private static Plugin _mod;

        public Plugin()
        {
            ConsoleLogger = Logger;
            _mod = this;

            SceneManager.sceneLoaded += (scene, mode) => 
            {
                GM.CurrentPlayerBody.gameObject.AddComponent<EnhancedHealth>();
                GM.CurrentPlayerBody.gameObject.AddComponent<EnhancedMovement>();
                GM.CurrentPlayerBody.gameObject.AddComponent<FallDamage>();
            };
        }

        public static ConfigEntry<T> BindConfig<T>(string section,
                                                   string key,
                                                   T      defaultValue,
                                                   string description) => _mod.Config.Bind
                                                                            (
                                                                                section,
                                                                                key,
                                                                                defaultValue,
                                                                                description
                                                                            );

    }
}