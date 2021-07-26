using FistVR;
using HADES.Configs;
using UnityEngine;

namespace HADES.Core
{
    public class EnhancedHealth : HADESEnhancement<EnhancedHealthConfig>
    {
        private float _currentRegenDelayLength;
        private float _healthMonitor;
        private float _initialHealth;
        private float _regenToGo;
        public float HealthPercentage { get; private set; }

        private float CurrentHealth => GM.GetPlayerHealth();
        private GameObject HealthBars => Player.HealthBar;

        private void Start()
        {
            if (!Config.Enabled) return;
            Print("Injected EnhancedHealth into player");
            _initialHealth = GM.GetPlayerHealth();
        }

        private void Update()
        {
            if (!Config.Enabled) return;
            //i'm not sure who thought that the formula was (_initialhealth / currenthealth) * 100 lol - potatoes
            HealthPercentage = CurrentHealth / _initialHealth * 100; //Thanks nathan!
        }

        private void FixedUpdate()
        {
            //if (HealthPercentage < RegenCap) Regenerate();

            //if player is below RegenCap
            if (Player.GetPlayerHealth() < Config.RegenCap * Player.GetMaxHealthPlayerRaw())
            {
                //if the delay time's up
                if (_currentRegenDelayLength >= Config.RegenDelay * 50)
                {
                    //if the player's hp is lower than what it was, assume damage taken, lower hp
                    if (Player.GetPlayerHealth() < _healthMonitor) _currentRegenDelayLength = 0;

                    //go add player hp
                    Player.HealPercent(Config.RegenSpeed * 0.02f);
                }
                else
                {
                    _currentRegenDelayLength++; //count down (up?) if the delay time is not finished
                }
            }
            else
            {
                _currentRegenDelayLength = 0;
            }

            _healthMonitor = Player.GetPlayerHealth();
        }

        //this is the public entry-way to regenerate the player
        public void RegeneratePlayerHP(float amt)
        {
            _regenToGo += amt;
        }
    }
}