using UnityEngine;
using static HADES.Utilities.Logging;

namespace HADES.Core
{
    public class HADES : MonoBehaviour
    {
        public FallDamage       FallDamage       { get; private set; }
        public EnhancedHealth   EnhancedHealth   { get; private set; }
        public EnhancedMovement EnhancedMovement { get; private set; }
        public Bleeding         Bleeding         { get; private set; }

        private void Awake()
        {
            FallDamage = gameObject.AddComponent<FallDamage>();
            EnhancedHealth = gameObject.AddComponent<EnhancedHealth>();
            EnhancedMovement = gameObject.AddComponent<EnhancedMovement>();
            Bleeding = gameObject.AddComponent<Bleeding>();

            Print("Injected HADES into player");
        }

        private void OnDestroy()
        {
            Destroy(FallDamage);
            Destroy(EnhancedHealth);
            Destroy(EnhancedMovement);
            Destroy(Bleeding);
        }
    }
}