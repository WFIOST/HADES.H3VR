using BepInEx.Configuration;
using HADES.Core;

namespace HADES.Configs
{
    public class EnhancedMovementConfig : ConfigEntry
    {
        private const string CATEGORY_NAME = "Enhanced Movement";

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