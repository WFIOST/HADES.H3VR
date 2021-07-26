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
            Stamina = MaxStamina;
            StaminaPercentage = MaxStamina / Stamina * 100;
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