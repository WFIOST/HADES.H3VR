using System;
using System.Linq;
using HADES.Utilities;
using On.FistVR;
using UnityEngine;
using FVRPhysicalObject = FistVR.FVRPhysicalObject;
using FVRQuickBeltSlot = FistVR.FVRQuickBeltSlot;
using GM = FistVR.GM;
using SimulationOptions = FistVR.SimulationOptions;
using BepInEx.Configuration;

using static HADES.Core.Plugin;
using static HADES.Utilities.Logging;

namespace HADES.Core
{
    public class EnhancedMovement : HADESEnhancement
    {
        public float Stamina           { get; private set; }
        public float StaminaPercentage { get; private set; }

        public float Weight
        {
            get
            {
                var qbSlots = Player.QuickbeltSlots;

                var weight = 0.0f;

                //If your QB slot has an object in it, add the associated weight of the size of the object to the weight 
                foreach (FVRQuickBeltSlot slot in qbSlots.Where(slot => !slot.CurObject.IsNull()))
                {
                    FVRPhysicalObject obj = slot.CurObject;

                    if (slot.Type == FVRQuickBeltSlot.QuickbeltSlotType.Backpack)
                        weight += BackpackWeightModifier;

                    weight += obj.Size switch
                    {
                        FVRPhysicalObject.FVRPhysicalObjectSize.Small        => SmallObjectWeightModifier,
                        FVRPhysicalObject.FVRPhysicalObjectSize.Medium       => MediumObjectWeightModifier,
                        FVRPhysicalObject.FVRPhysicalObjectSize.Large        => LargeObjectWeightModifier,
                        FVRPhysicalObject.FVRPhysicalObjectSize.Massive      => MassiveObjectWeightModifier,
                        FVRPhysicalObject.FVRPhysicalObjectSize.CantCarryBig => CCBWeightModifer,
                        _                                                    => throw new ArgumentOutOfRangeException()
                    };
                }

                return weight;
            }
        }

        private float PlayerSpeed
        {
            get => Player.GetBodyMovementSpeed();
            set
            {
                MovementManager.SlidingSpeed = value;
                MovementManager.DashSpeed = value;
            }
        }

        public bool Enabled => _enabledEntry.Value;
        private ConfigEntry<bool> _enabledEntry;

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

        public float MaxStamina            => _maxStaminaEntry.Value;
        public float StaminaGain           => _staminaGainEntry.Value;
        public float StaminaLoss           => _staminaLossEntry.Value;
        public float StaminaLossStartSpeed => _staminaLossStartSpeedEntry.Value;

        #endregion

        #region Weight

        public float WeightModifier              => _weightModifierEntry.Value;
        public float BackpackWeightModifier      => _backpackWeightModifierEntry.Value;
        public float SmallObjectWeightModifier   => _smallObjWeightModifierEntry.Value;
        public float MediumObjectWeightModifier  => _mediumObjWeightModifierEntry.Value;
        public float LargeObjectWeightModifier   => _largeObjWeightModifierEntry.Value;
        public float MassiveObjectWeightModifier => _massiveObjWeightModifierEntry.Value;
        public float CCBWeightModifer            => _ccbObjWeightModifierEntry.Value;

        #endregion

        #region Jumping

        public float JumpStaminaModifier       => _jumpStaminaModifierField.Value;
        public float RealisticGravityJumpForce => _realGravModeJumpForceField.Value;
        public float PlayfulGravityJumpForce   => _playGravModeJumpForceField.Value;
        public float MoonGravityJumpForce      => _moonGravModeJumpForceField.Value;
        public float NoGravityJumpForce        => _noneGravModeJumpForceField.Value;

        #endregion

        private const string CATEGORY_NAME = "EnhancedMovement";

        public EnhancedMovement()
        {
           _enabledEntry = Plugin.BindConfig
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
                "MaxStamina",
                100f,
                "Max stamina for the player, more stamina means you are able to move more"
            );

            _staminaGainEntry = Plugin.BindConfig
            (
                CATEGORY_NAME,
                "StaminaGain",
                15f,
                "The speed of which stamina regenerates"
            );

            _staminaLossEntry = Plugin.BindConfig
            (
                CATEGORY_NAME,
                "StaminaLoss",
                30f,
                "The speed of which stamina drains"
            );

            _staminaLossStartSpeedEntry = Plugin.BindConfig
            (
                CATEGORY_NAME,
                "StaminaLossSpeed",
                10f,
                "The speed that must be reached for stamina to drain"
            );

            #endregion


            #region Weight

            const string WEIGHT_CAT_NAME = CATEGORY_NAME + " - Weight Configuration";

            _weightModifierEntry = Plugin.BindConfig
            (
                WEIGHT_CAT_NAME,
                "WeightModifer",
                1f,
                "How much what you are carrying modifies the stamina loss"
            );

            _backpackWeightModifierEntry = Plugin.BindConfig
            (
                WEIGHT_CAT_NAME,
                "BackpackWeightModifier",
                10f,
                "How much weight wearing a backpack will add"
            );

            _smallObjWeightModifierEntry = Plugin.BindConfig
            (
                WEIGHT_CAT_NAME,
                "SmallObjectWeightModifier",
                1f,
                "How much weight a small object will add if it is in a Quickbelt slot"
            );

            _mediumObjWeightModifierEntry = Plugin.BindConfig
            (
                WEIGHT_CAT_NAME,
                "MediumObjectWeightModifier",
                2.5f,
                "How much weight a medium object will add if it is in a Quickbelt slot"
            );

            _largeObjWeightModifierEntry = Plugin.BindConfig
            (
                WEIGHT_CAT_NAME,
                "LargeObjectWeightModifier",
                5f,
                "How much weight a large object will add if it is in a Quickbelt slot"
            );

            _massiveObjWeightModifierEntry = Plugin.BindConfig
            (
                WEIGHT_CAT_NAME,
                "MassiveObjectWeightModifier",
                10f,
                "How much weight a massive object will add if it is in a Quickbelt slot"
            );

            _ccbObjWeightModifierEntry = Plugin.BindConfig
            (
                WEIGHT_CAT_NAME,
                "Can'tCarryBigObjectWeightModifier",
                15f,
                "How much weight a Can't Carry Big object will add if it is in a Quickbelt slot"
            );

            #endregion


            #region Jumping

            const string JUMP_CAT_NAME = CATEGORY_NAME + "_JumpConfiguration";

            _jumpStaminaModifierField = Plugin.BindConfig
            (
                JUMP_CAT_NAME,
                "JumpingStaminaModifier",
                5f,
                "How much stamina gets used when jumping"
            );

            _realGravModeJumpForceField = Plugin.BindConfig
            (
                JUMP_CAT_NAME,
                "RealisticGravityJumpForce",
                3.1f,
                "The force of which you jump on realistic gravity"
            );

            _playGravModeJumpForceField = Plugin.BindConfig
            (
                JUMP_CAT_NAME,
                "PlayfulGravityJumpForce",
                2.5f,
                "The force of which you jump on playful gravity"
            );

            _moonGravModeJumpForceField = Plugin.BindConfig
            (
                JUMP_CAT_NAME,
                "OnTheMoonGravityJumpForce",
                1.5f,
                "The force of which you jump on \"On the Moon\" gravity"
            );

            _noneGravModeJumpForceField = Plugin.BindConfig
            (
                JUMP_CAT_NAME,
                "NoGravityJumpForce",
                0f,
                "The force of which you jump on no gravity"
            );

            #endregion
        }

        private void Start()
        {
            if (!Enabled) return;
            Stamina = MaxStamina;
            StaminaPercentage = MaxStamina / Stamina * 100;

            FVRMovementManager.Jump += JumpPlus;
        }

        private void Update()
        {

            //Decrease the players speed based upon how much stamina there is compared to the max amount of stamina
            PlayerSpeed *= Convert.ToSingle(Stamina / MaxStamina);
        }

        private void FixedUpdate()
        {
            if (!Enabled) return;

            //Stamina is drained/gained based off the base stamina loss, plus the weight, plus the player speed
            if (PlayerSpeed > StaminaLossStartSpeed && Stamina > 0)
                Stamina -= Convert.ToSingle((StaminaLoss + Weight + PlayerSpeed) * 0.02);
            else if (PlayerSpeed < StaminaLossStartSpeed && Stamina < MaxStamina)
                Stamina += Convert.ToSingle((StaminaGain - Weight - PlayerSpeed) * 0.02);
        }

        private void JumpPlus(FVRMovementManager.orig_Jump _, FistVR.FVRMovementManager self)
        {
            if (Stamina < JumpStaminaModifier) return;

            Stamina -= JumpStaminaModifier + Weight;

            if ((self.Mode != FistVR.FVRMovementManager.MovementMode.Armswinger || self.m_armSwingerGrounded)
                && (self.Mode != FistVR.FVRMovementManager.MovementMode.SingleTwoAxis
                    && self.Mode != FistVR.FVRMovementManager.MovementMode.TwinStick || self.m_twoAxisGrounded))
            {
                self.DelayGround(0.1f);
                float jumpForce = GM.Options.SimulationOptions.PlayerGravityMode switch
                {
                    SimulationOptions.GravityMode.Realistic => RealisticGravityJumpForce,
                    SimulationOptions.GravityMode.Playful   => PlayfulGravityJumpForce,
                    SimulationOptions.GravityMode.OnTheMoon => MoonGravityJumpForce,
                    SimulationOptions.GravityMode.None      => NoGravityJumpForce,
                    _                                       => 0f
                };
                jumpForce *= 0.65f;
                switch (self.Mode)
                {
                    case FistVR.FVRMovementManager.MovementMode.Armswinger:
                        self.DelayGround(0.25f);
                        self.m_armSwingerVelocity.y = Mathf.Clamp(self.m_armSwingerVelocity.y, 0f,
                                                                  self.m_armSwingerVelocity.y);
                        self.m_armSwingerVelocity.y = jumpForce;
                        self.m_armSwingerGrounded = false;
                        break;
                    case FistVR.FVRMovementManager.MovementMode.SingleTwoAxis:
                        self.DelayGround(0.25f);
                        self.m_twoAxisVelocity.y =
                            Mathf.Clamp(self.m_twoAxisVelocity.y, 0f, self.m_twoAxisVelocity.y);
                        self.m_twoAxisVelocity.y = jumpForce;
                        self.m_twoAxisGrounded = false;
                        break;
                    case FistVR.FVRMovementManager.MovementMode.TwinStick:
                        self.DelayGround(0.25f);
                        self.m_twoAxisVelocity.y =
                            Mathf.Clamp(self.m_twoAxisVelocity.y, 0f, self.m_twoAxisVelocity.y);
                        self.m_twoAxisVelocity.y = jumpForce;
                        self.m_twoAxisGrounded = false;
                        break;
                }
            }
        }
    }
}