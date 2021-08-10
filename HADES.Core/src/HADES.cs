using UnityEngine;
using static HADES.Utilities.Logging;

namespace HADES.Core
{
    public class HADES : MonoBehaviour
    {
        public FallDamage       FallDamage          { get; private set; }
        public EnhancedHealth   EnhancedHealth      { get; private set; }
        public EnhancedMovement EnhancedMovement    { get; private set; }

        private void Awake()
        {
            FallDamage = gameObject.AddComponent<FallDamage>();
            EnhancedHealth = gameObject.AddComponent<EnhancedHealth>();
            EnhancedMovement = gameObject.AddComponent<EnhancedMovement>();
            Print("Injected HADES into player");
        }
    }
}