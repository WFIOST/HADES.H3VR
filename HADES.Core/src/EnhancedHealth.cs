using System.Collections;
using FistVR;
using HADES.Utilities;
using UnityEngine;
using static HADES.Utilities.Logging;

namespace HADES.Core
{
    public class EnhancedHealth : MonoBehaviour
    {
        public float HealthPercentage { get; private set; }

        private float CurrentHealth => GM.GetPlayerHealth();
        private float RegenCap => HADESConfig.EnhancedHealth.RegenCap;
        private float RegenDelay => HADESConfig.EnhancedHealth.RegenDelay;
        private float RegenSpeed => HADESConfig.EnhancedHealth.RegenSpeed;

        private GameObject HealthBars => Common.Player.HealthBar;
        
        private float _initialHealth;
        
        private void Start()
        {
            if (!HADESConfig.EnhancedHealth.Enabled) return;
            Print("Injected EnhancedHealth into player");
            _initialHealth = GM.GetPlayerHealth();
        }

        private void Update()
        {
            if (!HADESConfig.EnhancedHealth.Enabled) return;
            //i'm not sure who thought that the formula was (_initialhealth / currenthealth) * 100 lol - potatoes
            HealthPercentage = CurrentHealth / _initialHealth * 100; //Thanks nathan!

            if (HealthPercentage < RegenCap) StartCoroutine(Regenerate());
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
                Common.Player.HealPercent(i);
            }

            Logging.Debug.Print("Done Regeneration");
        }
    }
}