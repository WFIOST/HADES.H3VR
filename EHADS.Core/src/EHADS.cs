using FistVR;
using UnityEngine;

namespace EHADS.Core
{
    public class EHADS : MonoBehaviour
    {
        public static FVRPlayerBody Player => GM.CurrentPlayerBody;
        
        public FallDamage fallDamage;

        private void Awake()
        {
            fallDamage = gameObject.AddComponent<FallDamage>();
        }
    }
}