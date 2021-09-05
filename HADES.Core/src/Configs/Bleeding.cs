using BepInEx.Configuration;
using HADES.Core;

namespace HADES.Configs
{
    public class BleedingConfig : ConfigEntry
    {
        private const string CATEGORY_NAME = "Bleeding";
        private readonly ConfigEntry<float> _bleedoutRateEntry;
        private readonly ConfigEntry<float> _bleedoutLengthEntry;

        public BleedingConfig()
        {
            CategoryName = CATEGORY_NAME;
            
            EnabledEntry = Plugin.BindConfig
            (
                CATEGORY_NAME,
                "Enabled",
                true,
                "If enabled, the player will bleed out once damaged"
            );
            
            _bleedoutRateEntry = Plugin.BindConfig
            (
                CATEGORY_NAME, 
                "Bleedout Rate",
                10f,
                "How much health is lost per second whilst bleeding out"
            );

            _bleedoutLengthEntry = Plugin.BindConfig
            (
                CATEGORY_NAME,
                "Bleedout Length",
                10f,
                "How long will the player bleed out for"
            );
        }

        public float BleedoutRate => _bleedoutRateEntry.Value;
        public float BleedoutLength => _bleedoutLengthEntry.Value;
    }
}