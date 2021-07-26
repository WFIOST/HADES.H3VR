using BepInEx.Configuration;
using HADES.Core;

namespace HADES.Configs
{
    public class ConfigEntry
    {
        protected ConfigEntry<bool> EnabledEntry;
        public bool Enabled => EnabledEntry.Value;
    }
}