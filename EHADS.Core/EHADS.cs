using FistVR;
using UnityEngine;

namespace EHADS.Core
{
    public class EHADS : MonoBehaviour
    {
        private FallDamage _fallDamage;
        public static FVRPlayerBody Player => GM.CurrentPlayerBody;

        private void Awake()
        {
            _fallDamage = gameObject.AddComponent<FallDamage>();
        }
    }
}