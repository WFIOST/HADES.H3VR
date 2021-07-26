using BepInEx.Configuration;
using HADES.Core;

namespace HADES.Config
{
            public class ConfigEntry
        {
            protected ConfigEntry<bool> EnabledEntry;
            public bool Enabled => EnabledEntry.Value;
        }

        public class FallDamageConfig : ConfigEntry
        {
            private const string CATEGORY_NAME = "Fall Damages";
            private readonly ConfigEntry<float> _damageMultiplierEntry;
            private readonly ConfigEntry<float> _fallHeightEntry;

            public FallDamageConfig()
            {
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

        public class EnhancedMovementConfig : ConfigEntry
        {
            private const string CATEGORY_NAME = "Enhanced Movement";

            #region Stamina

            private readonly ConfigEntry<float> _maxStaminaEntry;
            private readonly ConfigEntry<float> _staminaLossStartSpeedEntry;
            private readonly ConfigEntry<float> _staminaGainEntry;
            private readonly ConfigEntry<float> _staminaLossEntry;

            #endregion

            #region Weight

            private readonly ConfigEntry<float> _backpackWeightModifierEntry;
            private readonly ConfigEntry<float> _ccbObjWeightModifierEntry;
            private readonly ConfigEntry<float> _largeObjWeightModifierEntry;
            private readonly ConfigEntry<float> _massiveObjWeightModifierEntry;
            private readonly ConfigEntry<float> _mediumObjWeightModifierEntry;
            private readonly ConfigEntry<float> _smallObjWeightModifierEntry;
            private readonly ConfigEntry<float> _weightModifierEntry;

            #endregion

            #region Jumping

            private readonly ConfigEntry<float> _jumpStaminaModifierField;
            private readonly ConfigEntry<float> _realGravModeJumpForceField;
            private readonly ConfigEntry<float> _playGravModeJumpForceField;
            private readonly ConfigEntry<float> _moonGravModeJumpForceField;
            private readonly ConfigEntry<float> _noneGravModeJumpForceField;
            
            #endregion

            public EnhancedMovementConfig()
            {
                EnabledEntry = Plugin.BindConfig
                (
                    CATEGORY_NAME,
                    "Enabled",
                    true,
                    "If enabled, Enhanced Movement will be active"
                );

                #region Stamina

                _maxStaminaEntry = Plugin.BindConfig
                (
                    CATEGORY_NAME,
                    "Max Stamina",
                    100f,
                    "Max stamina for the player, more stamina means you are able to move more"
                );

                _staminaGainEntry = Plugin.BindConfig
                (
                    CATEGORY_NAME,
                    "Stamina Gain",
                    15f,
                    "The speed of which stamina regenerates"
                );

                _staminaLossEntry = Plugin.BindConfig
                (
                    CATEGORY_NAME,
                    "Stamina Loss",
                    30f,
                    "The speed of which stamina drains"
                );

                _staminaLossStartSpeedEntry = Plugin.BindConfig
                (
                    CATEGORY_NAME,
                    "Stamina Loss Speed",
                    10f,
                    "The speed that must be reached for stamina to drain"
                );

                #endregion


                #region Weight

                const string WEIGHT_CAT_NAME = CATEGORY_NAME + " - Weight Configuration";

                _weightModifierEntry = Plugin.BindConfig
                (
                    WEIGHT_CAT_NAME,
                    "Weight Modifer",
                    1f,
                    "How much what you are carrying modifies the stamina loss"
                );

                _backpackWeightModifierEntry = Plugin.BindConfig
                (
                    WEIGHT_CAT_NAME,
                    "Backpack Weight Modifier",
                    10f,
                    "How much weight wearing a backpack will add"
                );

                _smallObjWeightModifierEntry = Plugin.BindConfig
                (
                    WEIGHT_CAT_NAME,
                    "Small Object Weight Modifier",
                    1f,
                    "How much weight a small object will add if it is in a Quickbelt slot"
                );

                _mediumObjWeightModifierEntry = Plugin.BindConfig
                (
                    WEIGHT_CAT_NAME,
                    "Medium Object Weight Modifier",
                    2.5f,
                    "How much weight a medium object will add if it is in a Quickbelt slot"
                );

                _largeObjWeightModifierEntry = Plugin.BindConfig
                (
                    WEIGHT_CAT_NAME,
                    "Large Object Weight Modifier",
                    5f,
                    "How much weight a large object will add if it is in a Quickbelt slot"
                );

                _massiveObjWeightModifierEntry = Plugin.BindConfig
                (
                    WEIGHT_CAT_NAME,
                    "Massive Object Weight Modifier",
                    10f,
                    "How much weight a massive object will add if it is in a Quickbelt slot"
                );

                _ccbObjWeightModifierEntry = Plugin.BindConfig
                (
                    WEIGHT_CAT_NAME,
                    "Can't Carry Big Object Weight Modifier",
                    15f,
                    "How much weight a Can't Carry Big object will add if it is in a Quickbelt slot"
                );

                #endregion


                #region Jumping

                const string JUMP_CAT_NAME = CATEGORY_NAME + " - Jump Configuration";

                _jumpStaminaModifierField = Plugin.BindConfig
                (
                    JUMP_CAT_NAME,
                    "Jumping Stamina Modifier",
                    5f,
                    "How much stamina gets used when jumping"
                );
                
                _realGravModeJumpForceField = Plugin.BindConfig
                (
                    JUMP_CAT_NAME,
                    "Realistic Gravity Jump Force",
                    3.1f,
                    "The force of which you jump on realistic gravity"
                );

                _playGravModeJumpForceField = Plugin.BindConfig
                (
                    JUMP_CAT_NAME,
                    "Playful Gravity Jump Force",
                    2.5f,
                    "The force of which you jump on playful gravity"
                );

                _moonGravModeJumpForceField = Plugin.BindConfig
                (
                    JUMP_CAT_NAME,
                    "On The Moon Gravity Jump Force",
                    1.5f,
                    "The force of which you jump on \"On the Moon\" gravity"
                );
                
                _noneGravModeJumpForceField = Plugin.BindConfig
                (
                    JUMP_CAT_NAME,
                    "No Gravity Jump Force",
                    0f,
                    "The force of which you jump on no gravity"
                );
                #endregion
            }

            #region Stamina

            public float MaxStamina => _maxStaminaEntry.Value;
            public float StaminaGain => _staminaGainEntry.Value;
            public float StaminaLoss => _staminaLossEntry.Value;
            public float StaminaLossStartSpeed => _staminaLossStartSpeedEntry.Value;

            #endregion

            #region Weight

            public float WeightModifier => _weightModifierEntry.Value;
            public float BackpackWeightModifier => _backpackWeightModifierEntry.Value;
            public float SmallObjectWeightModifier => _smallObjWeightModifierEntry.Value;
            public float MediumObjectWeightModifier => _mediumObjWeightModifierEntry.Value;
            public float LargeObjectWeightModifier => _largeObjWeightModifierEntry.Value;
            public float MassiveObjectWeightModifier => _massiveObjWeightModifierEntry.Value;
            public float CCBWeightModifer => _ccbObjWeightModifierEntry.Value;

            #endregion

            #region Jumping

            public float JumpStaminaModifier => _jumpStaminaModifierField.Value;
            public float RealisticGravityJumpForce => _realGravModeJumpForceField.Value;
            public float PlayfulGravityJumpForce => _playGravModeJumpForceField.Value;
            public float MoonGravityJumpForce => _moonGravModeJumpForceField.Value;
            public float NoGravityJumpForce => _noneGravModeJumpForceField.Value;

            #endregion
        }

}