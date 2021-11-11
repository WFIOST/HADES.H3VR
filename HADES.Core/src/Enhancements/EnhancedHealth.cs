using FistVR;
using UnityEngine;
using UnityEngine.UI;
using BepInEx.Configuration;

using static HADES.Core.Plugin;
using static HADES.Utilities.Logging;

namespace HADES.Core
{
    public class EnhancedHealth : HADESEnhancement
    {
        private float _currentRegenDelayLength;

        private float _healthMonitor;
        private float _initialHealth;

        public  float      HealthPercentage { get; private set; }
        private float      CurrentHealth    => GM.GetPlayerHealth();
        private GameObject HealthBar        => Player.HealthBar;


        public bool Enabled => _enabledEntry.Value;
        private ConfigEntry<bool> _enabledEntry;

        public float RegenCap => _regenCapEntry.Value;
        private ConfigEntry<float> _regenCapEntry;

        public float RegenDelay => _regenDelayEntry.Value;
        private ConfigEntry<float> _regenDelayEntry;

        public float RegenSpeed => _regenSpeedEntry.Value;
        private ConfigEntry<float> _regenSpeedEntry;

        private const string CATEGORY_NAME = "EnhancedHealth";

        public EnhancedHealth()
        {
            _enabledEntry = BindConfig<bool>
            (
                CATEGORY_NAME, 
                "Enabled", 
                true, 
                "Is Enhanced Health enabled"
            );

            _regenCapEntry = BindConfig<float>
            (
                CATEGORY_NAME,
                "RegenerationCap", 
                0.10f, 
                "Limit to how much you may regenerate (Note: this is a percentage so 1 is 100%!)"
            );

            _regenDelayEntry = Plugin.BindConfig
            (
                CATEGORY_NAME,
                "RegenerationDelay",
                10f,
                "Number of seconds before the regeneration will start"
            );

            _regenSpeedEntry = Plugin.BindConfig
            (
                CATEGORY_NAME,
                "RegenerationSpeed",
                0.5f,
                "How long does it take to regenerate to the regeneration cap"
            );
        }

        private void Start()
        {
            if (!Enabled) return;
            _initialHealth = GM.GetPlayerHealth();
        }

        private void Update()
        {
            if (!Enabled) return;
            //i'm not sure who thought that the formula was (_initialhealth / currenthealth) * 100 lol - potatoes
            HealthPercentage = CurrentHealth / _initialHealth * 100;
        }

        private void FixedUpdate()
        {
            if (!Enabled) return;

            /* REGENERATION */

            //if player is below RegenCap
            if (CurrentHealth < RegenCap * Player.GetMaxHealthPlayerRaw())
            {
                //if the delay time's up
                if (_currentRegenDelayLength >= RegenDelay * 50)
                {
                    //if the player's hp is lower than what it was, assume damage taken, lower hp
                    if (CurrentHealth < _healthMonitor) _currentRegenDelayLength = 0;
                    //go add player hp
                    Player.HealPercent(RegenSpeed * 0.02f);
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

            _healthMonitor = CurrentHealth;
        }
    }
}