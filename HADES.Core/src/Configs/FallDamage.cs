using BepInEx.Configuration;
using HADES.Core;

namespace HADES.Configs
{
    public class FallDamageConfig : ConfigEntry
    {
        private const string CATEGORY_NAME = "Fall Damage";
        private readonly ConfigEntry<float> _damageMultiplierEntry;
        private readonly ConfigEntry<float> _fallHeightEntry;

        public FallDamageConfig()
        {
            CategoryName = CATEGORY_NAME;
            EnabledEntry = Plugin.BindConfig
            (
                CATEGORY_NAME,
                "Enabled",
                true,
                "If enabled, the player will take Fall Damage based off how far they fall from (configurable)"
            );

            _fallHeightEntry = Plugin.BindConfig
            (
                CATEGORY_NAME,
                "Height",
                20f,
                "How far you need to fall to take fall damage"
            );

            _damageMultiplierEntry = Plugin.BindConfig
            (
                CATEGORY_NAME,
                "Multiplier",
                1.5f,
                "The multiplier is multiplied by your velocity (distance traveled between 2 points in 1 second) and is what damages you"
            );
        }

        public float FallHeight => _fallHeightEntry.Value;

        public float DamageMultiplier => _damageMultiplierEntry.Value;
    }
}