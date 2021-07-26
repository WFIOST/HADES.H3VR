using System;
using System.Collections;
using FistVR;
using HADES.Common;
using UnityEngine;
using static HADES.Common.Logging;

namespace HADES.Core
{
    public class EnhancedHealth : MonoBehaviour
    {
        private HADES _hadesSystem;
        private float _initialHealth;
        public float HealthPercentage { get; private set; }

        private float CurrentHealth => GM.GetPlayerHealth();
        private float RegenSpeed => HADESConfig.EnhancedHealth.RegenSpeed;

        private float RegenToGo;
        private float currentRegenDelayLength;
        private float healthMonitor;

        private GameObject HealthBars => _hadesSystem.Player.HealthBar;

        private void Start()
        {
            _hadesSystem = GetComponent<HADES>();
            Print("Injected EnhancedHealth into player");
            _initialHealth = GM.GetPlayerHealth();
        }

        private void Update()
        {
            //i'm not sure who thought that the formula was (_initialhealth / currenthealth) * 100 lol - potatoes
            HealthPercentage = CurrentHealth / _initialHealth * 100; //Thanks nathan!
        }
        
        private void FixedUpdate()
        {
            //if (HealthPercentage < RegenCap) Regenerate();
            
            RegenerationHandler();
        }
        
        //this is the public entry-way to regenerate the player
        public void RegeneratePlayerHP(float amt)
        {
            RegenToGo += amt;
        }
        
        //this is the bit that actually regenerates your hp
        private void RegenerationHandler()
        {
            /*PSEUDOCODE
            if the player's hp goes lower than was last frame, it is cancelled and restarted
             */
            
            //if player is below RegenCap
            if (_hadesSystem.Player.GetPlayerHealth() <
                HADESConfig.EnhancedHealth.RegenCap * _hadesSystem.Player.GetMaxHealthPlayerRaw())
            {
                //if the delay time's up
                if(currentRegenDelayLength >= HADESConfig.EnhancedHealth.RegenDelay * 50)
                {
                    //if the player's hp is lower than what it was, assume damage taken, lower hp
                    if (_hadesSystem.Player.GetPlayerHealth() < healthMonitor)
                    {
                        currentRegenDelayLength = 0;
                    }
                    
                    //go add player hp
                    _hadesSystem.Player.HealPercent(HADESConfig.EnhancedHealth.RegenSpeed * 0.02f);
                } else currentRegenDelayLength++; //count down (up?) if the delay time is not finished
            }
            else
            {
                currentRegenDelayLength = 0;
            }
            healthMonitor = _hadesSystem.Player.GetPlayerHealth();
        }
    }
}