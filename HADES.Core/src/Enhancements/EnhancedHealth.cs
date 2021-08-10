using FistVR;
using HADES.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace HADES.Core
{
    public class EnhancedHealth : HADESEnhancement<EnhancedHealthConfig>
    {
        private float _currentRegenDelayLength;

        private Text _hbText;
        private float _healthMonitor;
        private float _initialHealth;

        public float HealthPercentage { get; private set; }
        private float CurrentHealth => GM.GetPlayerHealth();
        private GameObject HealthBar => Player.HealthBar;

        private void Start()
        {
            if (!Config.Enabled) return;
            _initialHealth = GM.GetPlayerHealth();
            _hbText = HealthBar.transform.Find("f/Label_Title (1)").GetComponent<Text>();
        }

        private void Update()
        {
            if (!Config.Enabled) return;
            //i'm not sure who thought that the formula was (_initialhealth / currenthealth) * 100 lol - potatoes
            HealthPercentage = CurrentHealth / _initialHealth * 100;
            _hbText.text = $"{HealthPercentage}%";
        }

        private void FixedUpdate()
        {
            if (!Config.Enabled) return;
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
    }
}