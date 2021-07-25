using System.Collections;
using EHADS.Common;
using FistVR;
using UnityEngine;
using static EHADS.Common.Logging;

namespace EHADS.Core
{
    public class FallDamage : MonoBehaviour
    {
        private float _currentPos;

        private float _previousPos;
        private float _velocity;

        private void Update()
        {
            if (GM.IsDead() || !EHADSConfig.FallDamage.Enabled) return;

            var fallDmg = CalculateFallDamage();
            Print($"DMG: {fallDmg.Item1}, VEL: {fallDmg.Item2}");
            if (fallDmg.Item2 < EHADSConfig.FallDamage.FallHeight)
                EHADS.Player.HarmPercent(fallDmg.Item1 / 100); //Harm the player the percentage that they fell
        }

        private Tuple<float, float> CalculateFallDamage()
        {
            StartCoroutine(CheckPos());
            float damage = _velocity * EHADSConfig.FallDamage.DamageMultiplier;
            return new Tuple<float, float>(damage, _velocity);
        }

        private IEnumerator CheckPos()
        {
            //Get the player position, wait one second, then check again
            Vector3 position = EHADS.Player.transform.position;
            _previousPos = position.y;
            yield return new WaitForSeconds(1);
            _currentPos = position.y;
            //Then the "velocity" is how much has been moved between the seconds
            _velocity = _currentPos - _previousPos;
        }
    }
}