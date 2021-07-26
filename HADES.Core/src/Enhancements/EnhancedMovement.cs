using System;
using System.Collections;
using System.Linq;
using FistVR;
using HADES.Utilities;
using UnityEngine;

namespace HADES.Core
{
    public class EnhancedMovement : HADESEnhancement
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
                        weight += HADESConfig.EnhancedMovement.BackpackWeightModifier;

                    weight += obj.Size switch
                    {
                        FVRPhysicalObject.FVRPhysicalObjectSize.Small => HADESConfig.EnhancedMovement
                            .SmallObjectWeightModifier,
                        FVRPhysicalObject.FVRPhysicalObjectSize.Medium => HADESConfig.EnhancedMovement
                            .MediumObjectWeightModifier,
                        FVRPhysicalObject.FVRPhysicalObjectSize.Large => HADESConfig.EnhancedMovement
                            .LargeObjectWeightModifier,
                        FVRPhysicalObject.FVRPhysicalObjectSize.Massive => HADESConfig.EnhancedMovement
                            .MassiveObjectWeightModifier,
                        FVRPhysicalObject.FVRPhysicalObjectSize.CantCarryBig => HADESConfig.EnhancedMovement
                            .CCBWeightModifer,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }

                return weight;
            }
        }

        private float MaxStamina => HADESConfig.EnhancedMovement.MaxStamina;
        private float StaminaGain => HADESConfig.EnhancedMovement.StaminaGain;
        private float StaminaLoss => HADESConfig.EnhancedMovement.StaminaLoss;
        private float PlayerSpeed => Player.GetBodyMovementSpeed();
        
        private void Start()
        {
            if (!HADESConfig.EnhancedMovement.Enabled) return;
            Stamina = MaxStamina;
            StaminaPercentage = MaxStamina / Stamina * 100;
            
            //Reimplementation of jump for our needs
            On.FistVR.FVRMovementManager.Jump += (_, self) =>
            {
                if ((self.Mode != FVRMovementManager.MovementMode.Armswinger || self.m_armSwingerGrounded) 
                    && (self.Mode != FVRMovementManager.MovementMode.SingleTwoAxis 
                        && self.Mode != FVRMovementManager.MovementMode.TwinStick || self.m_twoAxisGrounded))
                {
                    self.DelayGround(0.1f);
                    float num = GM.Options.SimulationOptions.PlayerGravityMode switch
                    {
                        SimulationOptions.GravityMode.Realistic => HADESConfig.EnhancedMovement.RealisticGravityJumpForce,
                        SimulationOptions.GravityMode.Playful => HADESConfig.EnhancedMovement.PlayfulGravityJumpForce,
                        SimulationOptions.GravityMode.OnTheMoon => HADESConfig.EnhancedMovement.MoonGravityJumpForce,
                        SimulationOptions.GravityMode.None => HADESConfig.EnhancedMovement.NoGravityJumpForce,
                        _ => 0f
                    };
                    num *= 0.65f;
                    switch (self.Mode)
                    {
                        case FVRMovementManager.MovementMode.Armswinger:
                            self.DelayGround(0.25f);
                            self.m_armSwingerVelocity.y = Mathf.Clamp(self.m_armSwingerVelocity.y, 0f, self.m_armSwingerVelocity.y);
                            self.m_armSwingerVelocity.y = num;
                            self.m_armSwingerGrounded = false;
                            break;
                        case FVRMovementManager.MovementMode.SingleTwoAxis or FVRMovementManager.MovementMode.TwinStick:
                            self.DelayGround(0.25f);
                            self.m_twoAxisVelocity.y = Mathf.Clamp(self.m_twoAxisVelocity.y, 0f, self.m_twoAxisVelocity.y);
                            self.m_twoAxisVelocity.y = num;
                            self.m_twoAxisGrounded = false;
                            break;
                    }
                }
            };
        }

        private void Update()
        {
            if (!HADESConfig.EnhancedMovement.Enabled) return;

            float speed = Player.GetBodyMovementSpeed();
            if (speed < HADESConfig.EnhancedMovement.StaminaLossStartSpeed) return;
            StartCoroutine(DrainStamina());
        }

        private IEnumerator DrainStamina()
        {
            StaminaPercentage = Stamina / MaxStamina * 100;
            
            /* Burn through the stamina, we use StaminaLoss in the loop because
            that is how many seconds are to be elapsed to completely drain the stamina.
            How much you are carrying also is a factor, so we subtract the weight from the
            number of seconds that are to be elapsed */
            for (float i = 0; 
                i < StaminaLoss - Weight;  /* This is how long (in seconds) it takes to drain all of the stamina */
                i++)
            {
                //If the player isn't going over the threshold, stop the function
                if (PlayerSpeed < HADESConfig.EnhancedMovement.StaminaLossStartSpeed || Stamina <= 0)
                    yield break;
                
                
                
                yield return Common.WAIT_A_SEX;
            }
        }
    }
}