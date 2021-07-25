using System.Collections.Generic;
using BepInEx.Configuration;

namespace EHADS.Core
{
    public struct EHADSConfig
    {
        public class ConfigEntry
        {
            public static bool Enabled => EnabledEntry.Value;
            protected static ConfigEntry<bool> EnabledEntry;
        }
        
        public class FallDamage : ConfigEntry
        {
            private const string CATEGORY_NAME = "Fall Damages";

            public static float FallHeight => _fallHeightEntry.Value;
            private static ConfigEntry<float> _fallHeightEntry;

            public static float DamageMultiplier => _damageMultiplierEntry.Value;
            private static ConfigEntry<float> _damageMultiplierEntry;

            public static void BindConfigEntries()
            {
                EnabledEntry = Plugin.Mod.Config.Bind
                (
                    CATEGORY_NAME,
                    "Enabled",
                    true,
                    "If enabled, the player will take Fall Damage based off how far they fall from (configurable)"
                );

                _fallHeightEntry = Plugin.Mod.Config.Bind
                (
                    CATEGORY_NAME,
                    "Height",
                    20f,
                    "How far you need to fall to take fall damage"
                );

                _damageMultiplierEntry = Plugin.Mod.Config.Bind
                (
                    CATEGORY_NAME,
                    "Multiplier",
                    1.5f,
                    "The multiplier is multiplied by your velocity (distance traveled between 2 points in 1 second) and is what damages you"
                );
            }
        }

        public class EnhancedHealth : ConfigEntry
        {
            private const string CATEGORY_NAME = "Enhanced Health";

            public static float RegenCap => _regenCapEntry.Value;
            private static ConfigEntry<float> _regenCapEntry;

            public static float RegenDelay => _regenDelayEntry.Value;
            private static ConfigEntry<float> _regenDelayEntry;

            public static float RegenSpeed => _regenSpeedEntry.Value;
            private static ConfigEntry<float> _regenSpeedEntry;

            public static void BindConfigEntries()
            {
                EnabledEntry = Plugin.Mod.Config.Bind
                (
                    CATEGORY_NAME,
                    "Enabled",
                    true,
                    "If enabled, Enhanced Health will be active."
                );

                _regenCapEntry = Plugin.Mod.Config.Bind
                (
                    CATEGORY_NAME,
                    "Regeneration Cap",
                    0.10f,
                    "Limit to how much you may regenerate (Note: this is a percentage so 1 is 100%!)"
                );

                _regenDelayEntry = Plugin.Mod.Config.Bind
                (
                    CATEGORY_NAME,
                    "Regeneration Delay",
                    10f,
                    "Number of seconds before the regeneration will start"
                );

                _regenSpeedEntry = Plugin.Mod.Config.Bind
                (
                    CATEGORY_NAME,
                    "Regeneration Speed",
                    5f,
                    "How long does it take to regenerate to the regeneration cap"
                );
            }
        }

        static EHADSConfig()
        {
            FallDamage.BindConfigEntries();
            EnhancedHealth.BindConfigEntries();
        }
    }
}