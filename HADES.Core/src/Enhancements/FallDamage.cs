using FistVR;
using UnityEngine;
using BepInEx.Configuration;

using static HADES.Core.Plugin;
using static HADES.Utilities.Logging;

namespace HADES.Core
{
    public class FallDamage : HADESEnhancement
    {
        private Vector3 _currentPos;
        private float _currentVelocity;
        private Vector3 _previousPos;
        private float _previousVelocity;
        private float _velocityDifference;
        
        private const string CATEGORY_NAME = "Fall Damage";

        public bool Enabled => _enabledEntry.Value;
        private ConfigEntry<bool> _enabledEntry;

        private readonly ConfigEntry<float> _damageMultiplierEntry;
        public float DamageMultiplier => _damageMultiplierEntry.Value;

        private readonly ConfigEntry<float> _fallHeightEntry;
        public float FallHeight => _fallHeightEntry.Value;

        public FallDamage()
        {
            _enabledEntry = Plugin.BindConfig
            (
                CATEGORY_NAME,
                "Enabled",
                true,
                "If enabled, the player will take Fall Damage based off how far they fall from (configurable)"
            );

            _fallHeightEntry = Plugin.BindConfig
            (
                CATEGORY_NAME,
                "Height",
                20f,
                "How far you need to fall to take fall damage"
            );

            _damageMultiplierEntry = Plugin.BindConfig
            (
                CATEGORY_NAME,
                "Multiplier",
                1.5f,
                "The multiplier is multiplied by your velocity (distance traveled between 2 points in 1 second) and is what damages you"
            );
        }

        private void Update()
        {
            if (!Enabled || GM.IsDead()) return;


            float damage;
            float velocity;
            CalculateFallDamage(out damage, out velocity);

            if (damage >= 1)
                Player.HarmPercent(damage / 100); //Harm the player the percentage that they fell
        }

        public void FixedUpdate()
        {
            if (!Enabled) return;
            //note that the time frame for everything here is per step
            //etc, if the velocity is 1, that means 1 meter every 50th of a second

            //set prev pos
            _previousPos = _currentPos;
            //probably not a good idea to hard-code the idea that there's only ever one player.
            _currentPos = Player.transform.position;

            //get velocity
            _previousVelocity = _currentVelocity;
            //calculate velocity
            _currentVelocity = (_previousPos - _currentPos).magnitude;

            //get velocity difference
            _velocityDifference = _currentVelocity - _previousVelocity;
        }

        private void CalculateFallDamage(out float damage, out float velocity)
        {
            float dmg = 0;
            // * 0.02 is effectively / 50 but mult because muh optimization
            float effectiveVelocity = _velocityDifference + FallHeight * 0.02f;

            //if EV is less than 0, it means that the velocity was negative enough that the VDT could not
            //bring it back up to positive. Also, the velocity being negative means its slowing down.
            //The VDT is to prevent coming from a run to a stop doesnt hurt you lol
            if (effectiveVelocity < 0) dmg = effectiveVelocity * FallHeight;
            damage = dmg;
            velocity = effectiveVelocity;
        }
    }
}