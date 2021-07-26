using BepInEx.Configuration;
using HADES.Core;

namespace HADES.Configs
{
    public class ConfigEntry
    {
        public string CategoryName { get; protected set; }
        protected ConfigEntry<bool> EnabledEntry;
        public bool Enabled => EnabledEntry.Value;
    }
}