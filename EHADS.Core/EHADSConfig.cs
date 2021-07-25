using BepInEx.Configuration;

namespace EHADS.Core
{
    public struct EHADSConfig
    {

        public struct FallDamage
        {
            public const string CATEGORY_NAME = "Fall Damages";

            public static bool Enabled => EnabledEntry.Value;
            public static ConfigEntry<bool> EnabledEntry;

            public static float FallHeight => FallHeightEntry.Value;
            public static ConfigEntry<float> FallHeightEntry;

            public static float DamageMultiplier => DamageMultiplierEntry.Value;
            public static ConfigEntry<float> DamageMultiplierEntry;
        }

        public struct EnhancedHealth
        {
            public const string CATEGORY_NAME = "Enhanced Health";

            public static bool Enabled => EnabledEntry.Value;
            public static ConfigEntry<bool> EnabledEntry;

            public static float RegenCap => RegenCapEntry.Value;
            public static ConfigEntry<float> RegenCapEntry;

            public static float RegenDelay => RegenDelayEntry.Value;
            public static ConfigEntry<float> RegenDelayEntry;

            public static float RegenSpeed => RegenSpeedEntry.Value;
            public static ConfigEntry<float> RegenSpeedEntry;
        }

        static EHADSConfig()
        {
            FallDamage.EnabledEntry = Plugin.Mod.Config.Bind
            (
                FallDamage.CATEGORY_NAME,
                "Enabled",
                true,
                "If enabled, the player will take Fall Damage based off how far they fall from (configurable)"
            );

            FallDamage.FallHeightEntry = Plugin.Mod.Config.Bind
            (
                FallDamage.CATEGORY_NAME,
                "Height",
                20f,
                "How far you need to fall to take fall damage"
            );

            FallDamage.DamageMultiplierEntry = Plugin.Mod.Config.Bind
            (
                FallDamage.CATEGORY_NAME,
                "Multiplier",
                1.5f,
                "The multiplier is multiplied by your velocity (distance traveled between 2 points in 1 second) and is what damages you"
            );


            EnhancedHealth.EnabledEntry = Plugin.Mod.Config.Bind
            (
                EnhancedHealth.CATEGORY_NAME,
                "Enabled",
                true,
                "If enabled, Enhanced Health will be active."
            );

            EnhancedHealth.RegenCapEntry = Plugin.Mod.Config.Bind
            (
                EnhancedHealth.CATEGORY_NAME,
                "Regeneration Cap",
                0.10f,
                "Limit to how much you may regenerate (Note: this is a percentage so 1 is 100%!)"
            );

            EnhancedHealth.RegenDelayEntry = Plugin.Mod.Config.Bind
            (
            );
        }
    }
}