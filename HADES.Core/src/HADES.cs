using System.Collections;
using FistVR;
using UnityEngine;

using static EHADS.Common.Logging;

namespace HADES.Core
{
    public class HADES : MonoBehaviour
    {
        public static FVRPlayerBody Player => GM.CurrentPlayerBody;

        public static FallDamage       FallDamage;
        public static EnhancedHealth   EnhancedHealth;
        public static EnhancedMovement EnhancedMovement;
        private void Awake()
        {
            Print("Injected EHADS into player");
            FallDamage = gameObject.AddComponent<FallDamage>();
            EnhancedHealth = gameObject.AddComponent<EnhancedHealth>();
            EnhancedMovement = gameObject.AddComponent<EnhancedMovement>();
        }
    }
}