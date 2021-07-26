using BepInEx.Configuration;
using HADES.Core;

namespace HADES.Configs
{
    public class EnhancedHealthConfig : ConfigEntry
    {
        private const string CATEGORY_NAME = "Enhanced Health";
        private readonly ConfigEntry<float> _regenCapEntry;
        private readonly ConfigEntry<float> _regenDelayEntry;
        private readonly ConfigEntry<float> _regenSpeedEntry;

        public EnhancedHealthConfig()
        {
            EnabledEntry = Plugin.BindConfig
            (
                CATEGORY_NAME,
                "Enabled",
                true,
                "If enabled, Enhanced Health will be active."
            );

            _regenCapEntry = Plugin.BindConfig
            (
                CATEGORY_NAME,
                "Regeneration Cap",
                0.10f,
                "Limit to how much you may regenerate (Note: this is a percentage so 1 is 100%!)"
            );

            _regenDelayEntry = Plugin.BindConfig
            (
                CATEGORY_NAME,
                "Regeneration Delay",
                10f,
                "Number of seconds before the regeneration will start"
            );

            _regenSpeedEntry = Plugin.BindConfig
            (
                CATEGORY_NAME,
                "Regeneration Speed",
                5f,
                "How long does it take to regenerate to the regeneration cap"
            );
        }

        public float RegenCap => _regenCapEntry.Value;

        public float RegenDelay => _regenDelayEntry.Value;

        public float RegenSpeed => _regenSpeedEntry.Value;
    }
}