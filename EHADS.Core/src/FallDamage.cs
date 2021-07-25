using System;
using System.Collections;
using EHADS.Common;
using FistVR;
using UnityEngine;
using static EHADS.Common.Logging;

namespace EHADS.Core
{
    public class FallDamage : MonoBehaviour
    {
        private Vector3 _currentPos;
        private Vector3 _previousPos;
        private float _currentVelocity;
        private float _previousVelocity;
        private float _velocityDifference;

        private void Start()
        {
            if (!EHADSConfig.FallDamage.Enabled) Print("Injected FallDamage into player");
        }

        private void Update()
        {
            if (GM.IsDead() || !EHADSConfig.FallDamage.Enabled) return;

            var fallDmg = CalculateFallDamage();
            Print($"DMG: {fallDmg.Item1}, VEL: {fallDmg.Item2}");
            if (fallDmg.Item1 >= 1)
                EHADS.Player.HarmPercent(fallDmg.Item1 / 100); //Harm the player the percentage that they fell
        }

        private Tuple<float, float> CalculateFallDamage()
        {
            float damage = 0;
            // * 0.02 is effectively / 50 but mult because muh optimization
            float effectiveVelocity = _velocityDifference + EHADSConfig.FallDamage.FallHeight * 0.02f;

            //if EV is less than 0, it means that the velocity was negative enough that the VDT could not
            //bring it back up to positive. Also, the velocity being negative means its slowing down.
            //The VDT is to prevent coming from a run to a stop doesnt hurt you lol
            if (effectiveVelocity < 0)
            {
                damage = effectiveVelocity * EHADSConfig.FallDamage.DamageMultiplier;
            }
            return new Tuple<float, float>(damage, effectiveVelocity);
        }

        public void FixedUpdate()
        {
            CalculateVelocity();
        }
        
        //this exists to not clog up fixedupdate
        private void CalculateVelocity()
        {
            //note that the time frame for everything here is per step
            //etc, if the velocity is 1, that means 1 meter every 50th of a second
            
            //set prev pos
            _previousPos = _currentPos;
            //probably not a good idea to hard-code the idea that there's only ever one player.
            _currentPos = EHADS.Player.transform.position;
            
            //get velocity
            _previousVelocity = _currentVelocity;
            //calculate velocity
            _currentVelocity = (_previousPos - _currentPos).magnitude;
            
            //get velocity difference
            _velocityDifference = _currentVelocity - _previousVelocity;
        }
    }
}
