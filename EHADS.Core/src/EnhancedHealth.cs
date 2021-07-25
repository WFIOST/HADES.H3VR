using System;
using System.Collections;
using EHADS.Common;
using FistVR;
using UnityEngine;
using UnityEngine.UI;
using static EHADS.Common.Logging;

namespace EHADS.Core
{
    public class EnhancedHealth : MonoBehaviour
    {
        public float HealthPercentage { get; private set; }
        
        private float CurrentHealth => GM.GetPlayerHealth();
        private float RegenCap => EHADSConfig.EnhancedHealth.RegenCap;
        private float RegenDelay => EHADSConfig.EnhancedHealth.RegenDelay;
        private float RegenSpeed => EHADSConfig.EnhancedHealth.RegenSpeed;

        private GameObject HealthBars => EHADS.Player.HealthBar;
        
        private float _initialHealth;

        private void Start()
        {
            Print("Injected EnhancedHealth into player");
            _initialHealth = GM.GetPlayerHealth();
        }

        private void Update()
        {
            //i'm not sure who thought that the formulat was (_initialhealth / currenthealth) * 100 lol - potatoes
            HealthPercentage = (CurrentHealth / _initialHealth) * 100; //Thanks nathan!

            if (HealthPercentage < RegenCap)
            {
                StartCoroutine(Regenerate());
            }
        }

        private IEnumerator Regenerate()
        {
            float initHealth = CurrentHealth;
            regen:
            yield return new WaitForSeconds(RegenDelay);
            
            Logging.Debug.Print("Regenerating...");
            
            for (float i = 0; i < RegenCap; i += RegenSpeed / 10)
            {
                float curHealth = CurrentHealth;
                Logging.Debug.Print($"Current health {curHealth}\nInit health {initHealth}");
                if (curHealth < initHealth) goto regen;
                EHADS.Player.HealPercent(i);
            }
            
            Logging.Debug.Print("Done Regeneration");
        }
    }
}
