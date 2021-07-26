using System;
using System.Linq;
using FistVR;
using HADES.Configs;
using UnityEngine;
using FVRMovementManager = On.FistVR.FVRMovementManager;

namespace HADES.Core
{
    public class EnhancedMovement : HADESEnhancement<EnhancedMovementConfig>
    {
        public float Stamina { get; private set; }
        public float StaminaPercentage { get; private set; }

        public float Weight
        {
            get
            {
                var qbSlots = Player.QuickbeltSlots;

                var weight = 0.0f;

                foreach (FVRQuickBeltSlot slot in qbSlots.Where(slot => slot.CurObject != null))
                {
                    FVRPhysicalObject obj = slot.CurObject;

                    if (slot.Type == FVRQuickBeltSlot.QuickbeltSlotType.Backpack)
                        weight += Config.BackpackWeightModifier;

                    weight += obj.Size switch
                    {
                        FVRPhysicalObject.FVRPhysicalObjectSize.Small => Config
                            .SmallObjectWeightModifier,
                        FVRPhysicalObject.FVRPhysicalObjectSize.Medium => Config
                            .MediumObjectWeightModifier,
                        FVRPhysicalObject.FVRPhysicalObjectSize.Large => Config
                            .LargeObjectWeightModifier,
                        FVRPhysicalObject.FVRPhysicalObjectSize.Massive => Config
                            .MassiveObjectWeightModifier,
                        FVRPhysicalObject.FVRPhysicalObjectSize.CantCarryBig => Config
                            .CCBWeightModifer,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }

                return weight;
            }
        }

        private float MaxStamina => Config.MaxStamina;
        private float StaminaGain => Config.StaminaGain;
        private float StaminaLoss => Config.StaminaLoss;
        private float PlayerSpeed => Player.GetBodyMovementSpeed();

        private void Start()
        {
            base.Start();
            Stamina = MaxStamina;
            StaminaPercentage = MaxStamina / Stamina * 100;

            //Reimplementation of jump for our needs
            FVRMovementManager.Jump += (_, self) =>
            {
                if ((self.Mode != FistVR.FVRMovementManager.MovementMode.Armswinger || self.m_armSwingerGrounded)
                    && (self.Mode != FistVR.FVRMovementManager.MovementMode.SingleTwoAxis
                        && self.Mode != FistVR.FVRMovementManager.MovementMode.TwinStick || self.m_twoAxisGrounded))
                {
                    self.DelayGround(0.1f);
                    float num = GM.Options.SimulationOptions.PlayerGravityMode switch
                    {
                        SimulationOptions.GravityMode.Realistic => Config.RealisticGravityJumpForce,
                        SimulationOptions.GravityMode.Playful => Config.PlayfulGravityJumpForce,
                        SimulationOptions.GravityMode.OnTheMoon => Config.MoonGravityJumpForce,
                        SimulationOptions.GravityMode.None => Config.NoGravityJumpForce,
                        _ => 0f
                    };
                    num *= 0.65f;
                    switch (self.Mode)
                    {
                        case FistVR.FVRMovementManager.MovementMode.Armswinger:
                            self.DelayGround(0.25f);
                            self.m_armSwingerVelocity.y = Mathf.Clamp(self.m_armSwingerVelocity.y, 0f,
                                self.m_armSwingerVelocity.y);
                            self.m_armSwingerVelocity.y = num;
                            self.m_armSwingerGrounded = false;
                            break;
                        case FistVR.FVRMovementManager.MovementMode.SingleTwoAxis or FistVR.FVRMovementManager
                            .MovementMode.TwinStick:
                            self.DelayGround(0.25f);
                            self.m_twoAxisVelocity.y =
                                Mathf.Clamp(self.m_twoAxisVelocity.y, 0f, self.m_twoAxisVelocity.y);
                            self.m_twoAxisVelocity.y = num;
                            self.m_twoAxisGrounded = false;
                            break;
                    }
                }
            };
        }


        private void FixedUpdate()
        {
            base.FixedUpdate();

            if (PlayerSpeed < Config.StaminaLossStartSpeed)
            {
            }
        }
    }
}