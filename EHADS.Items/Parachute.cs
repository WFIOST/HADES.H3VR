using FistVR;
using UnityEngine;

namespace EHADS.Items
{
    public class Parachute : PlayerBackPack
    {
        public GameObject parachute;
        public float deployTime;

        private FVRPlayerBody _player;

        public void Deploy()
        {
            if (QuickbeltSlot != null && QuickbeltSlot.IsPlayer)
            {
            }
        }
    }
}