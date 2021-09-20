using BepInEx.Configuration;

namespace HADES.Configs
{
    public class ConfigEntry
    {
        protected ConfigEntry<bool> EnabledEntry;
        public string CategoryName { get; protected set; }
        public bool   Enabled      => EnabledEntry.Value;
    }
}