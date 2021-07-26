using System;
using System.Linq;
using FistVR;
using UnityEngine;

namespace HADES.Core
{
    public class EnhancedMovement : MonoBehaviour
    {
        private HADES _hadesSystem;
        public float Stamina { get; private set; }

        public float Weight
        {
            get
            {
                var qbSlots = _hadesSystem.Player.QuickbeltSlots;
                
                var weight = 0.0f;
                
                foreach (FVRQuickBeltSlot slot in qbSlots.Where(slot => slot.CurObject != null))
                {
                    FVRPhysicalObject obj = slot.CurObject;

                    if (slot.Type == FVRQuickBeltSlot.QuickbeltSlotType.Backpack) weight += BackpackWeightModifer;

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

        private float WeightModifier => HADESConfig.EnhancedMovement.WeightModifier;
        private float BackpackWeightModifer => HADESConfig.EnhancedMovement.BackpackWeightModifier;

        private void Start()
        {
            _hadesSystem = GetComponent<HADES>();
            Stamina = MaxStamina;
        }

        private void Update()
        {
            if (!HADESConfig.EnhancedMovement.Enabled) return;

            float speed = _hadesSystem.Player.GetBodyMovementSpeed();
            if (speed < 10f) return;
            GM.CurrentMovementManager.SlidingSpeed -= Stamina / 100;
        }
    }
}