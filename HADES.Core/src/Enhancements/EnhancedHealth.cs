using System;
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
        private float RegenSpeed => HADESConfig.EnhancedHealth.RegenSpeed;

        private float _regenToGo;
        private float _currentRegenDelayLength;
        private float _healthMonitor;
        private float _initialHealth;

        private GameObject HealthBars => Player.HealthBar;
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
        }
        
        private void FixedUpdate()
        {
            //if (HealthPercentage < RegenCap) Regenerate();
            
            RegenerationHandler();
        }
        
        //this is the public entry-way to regenerate the player
        public void RegeneratePlayerHP(float amt)
        {
            _regenToGo += amt;
        }
        
        //this is the bit that actually regenerates your hp
        private void RegenerationHandler()
        {
            //if player is below RegenCap
            if (Player.GetPlayerHealth() <
                HADESConfig.EnhancedHealth.RegenCap * Player.GetMaxHealthPlayerRaw())
            {
                //if the delay time's up
                if(_currentRegenDelayLength >= HADESConfig.EnhancedHealth.RegenDelay * 50)
                {
                    //if the player's hp is lower than what it was, assume damage taken, lower hp
                    if (Player.GetPlayerHealth() < _healthMonitor)
                    {
                        _currentRegenDelayLength = 0;
                    }
                    
                    //go add player hp
                    Player.HealPercent(HADESConfig.EnhancedHealth.RegenSpeed * 0.02f);
                } else _currentRegenDelayLength++; //count down (up?) if the delay time is not finished
            }
            else
            {
                _currentRegenDelayLength = 0;
            }
            _healthMonitor = Player.GetPlayerHealth();
        }
    }
}