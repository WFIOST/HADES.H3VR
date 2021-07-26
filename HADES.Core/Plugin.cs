using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using FistVR;
using static HADES.Utilities.Logging;
using PluginInfo = HADES.Utilities.PluginInfo;

namespace HADES.Core
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.NAME, PluginInfo.VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Mod;
        public static ManualLogSource ConsoleLogger;

        public Plugin()
        {
            Mod = this;
            Print($"Loading EHADS version {PluginInfo.VERSION}");
        }

        private void Start()
        {
            GM.CurrentPlayerBody.gameObject.AddComponent<HADES>();
            Print($"Loaded EHADS version {PluginInfo.VERSION}!");
        }

        public static ConfigEntry<T> BindConfig<T>(string section,
            string key,
            T defaultValue,
            string description)
        {
            return Mod.Config.Bind(section, key, defaultValue, description);
        }
    }
}