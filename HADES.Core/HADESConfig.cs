using System.Collections.Generic;
using BepInEx.Configuration;

namespace HADES.Core
{
    public struct HADESConfig
    {
        public static FallDamageConfig        FallDamage        { get; }
        public static EnhancedHealthConfig    EnhancedHealth    { get; }
        public static EnhancedMovementConfig  EnhancedMovement  { get; }

        public class ConfigEntry
        {
            public bool Enabled => EnabledEntry.Value;
            protected ConfigEntry<bool> EnabledEntry;
        }
        
        public class FallDamageConfig : ConfigEntry
        {
            private const string CATEGORY_NAME = "Fall Damages";

            public float FallHeight => _fallHeightEntry.Value;
            private readonly ConfigEntry<float> _fallHeightEntry;

            public float DamageMultiplier => _damageMultiplierEntry.Value;
            private readonly ConfigEntry<float> _damageMultiplierEntry;

            public FallDamageConfig()
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

        public class EnhancedHealthConfig : ConfigEntry
        {
            private const string CATEGORY_NAME = "Enhanced Health";

            public float RegenCap => _regenCapEntry.Value;
            private readonly ConfigEntry<float> _regenCapEntry;

            public float RegenDelay => _regenDelayEntry.Value;
            private readonly ConfigEntry<float> _regenDelayEntry;

            public float RegenSpeed => _regenSpeedEntry.Value;
            private readonly ConfigEntry<float> _regenSpeedEntry;

            public EnhancedHealthConfig()
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

        public class EnhancedMovementConfig : ConfigEntry
        {
            public const string CATEGORY_NAME = "Enhanced Movement";
            
            public float MaxStamina => _maxStaminaEntry.Value;
            private readonly ConfigEntry<float> _maxStaminaEntry;

            public float StaminaGain => _staminaGainEntry.Value;
            private readonly ConfigEntry<float> _staminaGainEntry;

            public float StaminaLoss => _staminaLossEntry.Value;
            private readonly ConfigEntry<float> _staminaLossEntry;

            public float WeightModifier => _weightModifierEntry.Value;
            private readonly ConfigEntry<float> _weightModifierEntry;

            public float BackpackWeightModifier => _backpackWeightModifierEntry.Value;
            private readonly ConfigEntry<float> _backpackWeightModifierEntry;

            public float SmallObjectWeightModifier => _smallObjWeightModifierEntry.Value;
            private readonly ConfigEntry<float> _smallObjWeightModifierEntry;

            public float MediumObjectWeightModifier => _mediumObjWeightModifierEntry.Value;
            private readonly ConfigEntry<float> _mediumObjWeightModifierEntry;

            public float LargeObjectWeightModifier => _largeObjWeightModifierEntry.Value;
            private readonly ConfigEntry<float> _largeObjWeightModifierEntry;

            public float MassiveObjectWeightModifier => _massiveObjWeightModifierEntry.Value;
            private readonly ConfigEntry<float> _massiveObjWeightModifierEntry;

            public float CCBWeightModifer => _ccbObjWeightModifierEntry.Value;
            private readonly ConfigEntry<float> _ccbObjWeightModifierEntry;

            public EnhancedMovementConfig()
            {
                EnabledEntry = Plugin.Mod.Config.Bind
                (
                    CATEGORY_NAME,
                    "Enabled",
                    true,
                    "If enabled, Enhanced Movement will be active"
                );

                _maxStaminaEntry = Plugin.Mod.Config.Bind
                (
                    CATEGORY_NAME,
                    "Max Stamina",
                    100f,
                    "Max stamina for the player, more stamina means you are able to move more"
                );

                _staminaGainEntry = Plugin.Mod.Config.Bind
                (
                    CATEGORY_NAME,
                    "Stamina Gain",
                    5f,
                    "The amount of stamina gained whilst inactive"

                );

                _staminaLossEntry = Plugin.Mod.Config.Bind
                (
                    CATEGORY_NAME,
                    "Stamina Loss",
                    10f,
                    "The amount of stamina lost whilst active"
                );

                const string WEIGHT_CAT_NAME = CATEGORY_NAME + " - Weight Configuration";
                
                _weightModifierEntry = Plugin.Mod.Config.Bind
                (
                    WEIGHT_CAT_NAME,
                    "Weight Modifer",
                    1f,
                    "How much what you are carrying modifies the stamina loss"
                );

                _backpackWeightModifierEntry = Plugin.Mod.Config.Bind
                (
                    WEIGHT_CAT_NAME,
                    "Backpack Weight Modifier",
                    10f,
                    "How much weight wearing a backpack will add"
                );

                _smallObjWeightModifierEntry = Plugin.Mod.Config.Bind
                (
                    WEIGHT_CAT_NAME,
                    "Small Object Weight Modifier",
                    1f,
                    "How much weight a small object will add if it is in a Quickbelt slot"
                );

                _mediumObjWeightModifierEntry = Plugin.Mod.Config.Bind
                (
                    WEIGHT_CAT_NAME,
                    "Medium Object Weight Modifier",
                    2.5f,
                    "How much weight a medium object will add if it is in a Quickbelt slot"
                );
                
                _largeObjWeightModifierEntry = Plugin.Mod.Config.Bind
                (
                    WEIGHT_CAT_NAME,
                    "Large Object Weight Modifier",
                    5f,
                    "How much weight a large object will add if it is in a Quickbelt slot"
                );
                
                _massiveObjWeightModifierEntry = Plugin.Mod.Config.Bind
                (
                    WEIGHT_CAT_NAME,
                    "Massive Object Weight Modifier",
                    10f,
                    "How much weight a massive object will add if it is in a Quickbelt slot"
                );
                
                _ccbObjWeightModifierEntry = Plugin.Mod.Config.Bind
                (
                    WEIGHT_CAT_NAME,
                    "Can't Carry Big Object Weight Modifier",
                    15f,
                    "How much weight a Can't Carry Big object will add if it is in a Quickbelt slot"
                );
            }
        }
        
        static HADESConfig()
        {
            FallDamage = new FallDamageConfig();
            EnhancedHealth = new EnhancedHealthConfig();
            EnhancedMovement = new EnhancedMovementConfig();
        }
    }
}