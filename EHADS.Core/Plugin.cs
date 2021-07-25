using BepInEx;
using BepInEx.Logging;
using FistVR;
using static EHADS.Common.Logging;
using PluginInfo = EHADS.Common.PluginInfo;

namespace EHADS.Core
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
            GM.CurrentPlayerBody.gameObject.AddComponent<EHADS>();
            Print($"Loaded EHADS version {PluginInfo.VERSION}!");
        }
    }
}