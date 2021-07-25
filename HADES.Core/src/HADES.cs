using FistVR;
using UnityEngine;
using static HADES.Common.Logging;

namespace HADES.Core
{
    public class HADES : MonoBehaviour
    {
        public static FVRPlayerBody Player => GM.CurrentPlayerBody;

        public FallDamage FallDamage { get; private set; }
        public EnhancedHealth EnhancedHealth { get; private set; }
        public EnhancedMovement EnhancedMovement { get; private set; }

        private void Awake()
        {
            Print("Injected EHADS into player");
            FallDamage = gameObject.AddComponent<FallDamage>();
            EnhancedHealth = gameObject.AddComponent<EnhancedHealth>();
            EnhancedMovement = gameObject.AddComponent<EnhancedMovement>();
        }
    }
}