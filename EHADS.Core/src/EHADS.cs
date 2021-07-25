using System.Collections;
using FistVR;
using UnityEngine;

using static EHADS.Common.Logging;

namespace EHADS.Core
{
    public class EHADS : MonoBehaviour
    {
        public static FVRPlayerBody Player => GM.CurrentPlayerBody;
        
        public FallDamage       fallDamage;
        public EnhancedHealth   enhancedHealth;
        private void Awake()
        {
            Print("Injected EHADS into player");
            fallDamage = gameObject.AddComponent<FallDamage>();
            enhancedHealth = gameObject.AddComponent<EnhancedHealth>();
        }
    }
}