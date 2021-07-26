using System;
using System.Linq;
using FistVR;
using HADES.Configs;
using UnityEngine;
using FVRMovementManager = FistVR.FVRMovementManager;

namespace HADES.Core
{
    public class EnhancedMovement : HADESEnhancement<EnhancedMovementConfig>
    {
        public float Stamina            { get; private set; }
        public float StaminaPercentage  { get; private set; }

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
        private float PlayerSpeed => Player.GetBodyMovementSpeed();

        private void Start()
        {
            if (!Config.Enabled) return;
            Stamina = Config.MaxStamina;
            StaminaPercentage = Config.MaxStamina / Stamina * 100;

            //Reimplementation of jump for our needs
            On.FistVR.FVRMovementManager.Jump += (_, self) =>
            {
                if (Stamina < Config.JumpStaminaModifier) return;

                Stamina -= Config.JumpStaminaModifier + Weight;
                if ((self.Mode != FVRMovementManager.MovementMode.Armswinger 
                     || self.m_armSwingerGrounded)
                     && (self.Mode != FVRMovementManager.MovementMode.SingleTwoAxis 
                     && self.Mode != FVRMovementManager.MovementMode.TwinStick 
                     || self.m_twoAxisGrounded))
                {
                    self.DelayGround(0.1f);
                    float jumpForce = GM.Options.SimulationOptions.PlayerGravityMode switch
                    {
                        SimulationOptions.GravityMode.Realistic => Config.RealisticGravityJumpForce,
                        SimulationOptions.GravityMode.Playful => Config.PlayfulGravityJumpForce,
                        SimulationOptions.GravityMode.OnTheMoon => Config.MoonGravityJumpForce,
                        SimulationOptions.GravityMode.None => Config.NoGravityJumpForce,
                        _ => 0f
                    };
                    jumpForce *= 0.65f;
                    switch (self.Mode)
                    {
                        case FVRMovementManager.MovementMode.Armswinger:
                            self.DelayGround(0.25f);
                            self.m_armSwingerVelocity.y = Mathf.Clamp(self.m_armSwingerVelocity.y, 0f,
                                self.m_armSwingerVelocity.y);
                            self.m_armSwingerVelocity.y = jumpForce;
                            self.m_armSwingerGrounded = false;
                            break;
                        case FVRMovementManager.MovementMode.SingleTwoAxis:
                            self.DelayGround(0.25f);
                            self.m_twoAxisVelocity.y =
                                Mathf.Clamp(self.m_twoAxisVelocity.y, 0f, self.m_twoAxisVelocity.y);
                            self.m_twoAxisVelocity.y = jumpForce;
                            self.m_twoAxisGrounded = false;
                            break;
                        case FVRMovementManager.MovementMode.TwinStick:
                            self.DelayGround(0.25f);
                            self.m_twoAxisVelocity.y =
                                Mathf.Clamp(self.m_twoAxisVelocity.y, 0f, self.m_twoAxisVelocity.y);
                            self.m_twoAxisVelocity.y = jumpForce;
                            self.m_twoAxisGrounded = false;
                            break;
                        
                    }
                }
            };
        }

        private void Update()
        {
            MovementManager.SlidingSpeed /= Stamina / Config.MaxStamina;
            MovementManager.DashSpeed /= Stamina / Config.MaxStamina;
        }

        private void FixedUpdate()
        {
            if (!Config.Enabled) return;

            //Stamina is drained/gained based off the base stamina loss, plus the weight, plus the player speed
            if (PlayerSpeed > Config.StaminaLossStartSpeed && Stamina > 0)
                Stamina -= Convert.ToSingle((Config.StaminaLoss + Weight + PlayerSpeed) * 0.02);
            else if (PlayerSpeed < Config.StaminaLossStartSpeed && Stamina < Config.MaxStamina)
                Stamina += Convert.ToSingle((Config.StaminaGain - Weight - PlayerSpeed) * 0.02);
        }
    }
}