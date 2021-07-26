using System.Collections;
using FistVR;
using HADES.Utilities;
using UnityEngine;
using static HADES.Utilities.Logging;

namespace HADES.Core
{
    public class EnhancedHealth : HADESEnhancement
    {
        public float HealthPercentage { get; private set; }

        private float CurrentHealth => GM.GetPlayerHealth();
        private float RegenCap => HADESConfig.EnhancedHealth.RegenCap;
        private float RegenDelay => HADESConfig.EnhancedHealth.RegenDelay;
        private float RegenSpeed => HADESConfig.EnhancedHealth.RegenSpeed;

        private GameObject HealthBars => Player.HealthBar;
        
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
            float initHealth = CurrentHealth; //Get the health at the execution of the function
            
            regen:
            yield return new WaitForSeconds(RegenDelay); //Wait the delay out

            Logging.Debug.Print("Regenerating...");
            
            //Loop through until the regen cap is reached, i is iterated by the regeneration speed
            for (float i = 0; i < RegenCap; i += RegenSpeed / 10) 
            {
                //The health before healing
                float curHealth = CurrentHealth;
                Logging.Debug.Print($"Current health {curHealth}\nInit health {initHealth}");
                //If the player was damaged (The current health before healing is less than the health before starting regeneration),
                //then restart the loop with the cooldown
                if (curHealth < initHealth) goto regen;
                //TODO: make the player heal in n intervals until the regen cap is reached
                Player.HealPercent(i);
            }

            Logging.Debug.Print("Done Regeneration");
        }
    }
}