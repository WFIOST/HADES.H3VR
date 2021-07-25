using System;
using FistVR;
using UnityEngine;

namespace EHADS.Core
{
    public class EHADS : MonoBehaviour
    {
        public static FVRPlayerBody Player => GM.CurrentPlayerBody;
        private FallDamage _fallDamage;
        private void Awake()
        {
            _fallDamage = gameObject.AddComponent<FallDamage>();
        }
    }
}