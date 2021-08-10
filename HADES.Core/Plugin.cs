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
        public static ManualLogSource ConsoleLogger;

        private static Plugin _mod;

        public Plugin()
        {
            _mod = this;
            ConsoleLogger = Logger;
            Print($"Loading EHADS version {PluginInfo.VERSION}");
        }

        private void Start()
        {
            GM.CurrentPlayerBody.gameObject.AddComponent<HADES>();
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