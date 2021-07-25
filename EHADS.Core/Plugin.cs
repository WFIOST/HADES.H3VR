using System.Collections;
using BepInEx;
using BepInEx.Logging;
using EHADS.Common;
using FistVR;
using UnityEngine;
using static EHADS.Common.Logging;
using PluginInfo = EHADS.Common.PluginInfo;

namespace EHADS.Core
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.NAME, PluginInfo.VERSION)]
    public class EHADS : BaseUnityPlugin
    {
        public static ManualLogSource ConsoleLogger;

        public EHADS()
        {
            ConsoleLogger = Logger;
            Print($"Loading EHADS version {PluginInfo.VERSION}");
        }

        private static FVRPlayerBody Player => GM.CurrentPlayerBody;

        private void Start()
        {
            Print($"Loaded EHADS version {PluginInfo.VERSION}!");
        }

        private void Update()
        {
            if (!GM.IsDead())
            {
                var fallDmg = CalculateFallDamage();
                Print($"DMG: {fallDmg.Item1}, VEL: {fallDmg.Item2}");
                if (fallDmg.Item2 < 20f)
                    Player.HarmPercent(-(fallDmg.Item1 / 100)); //Harm the player the percentage that they fell
            }
        }

        #region FALL DAMAGE

        private float _previousPos;
        private float _currentPos;
        private float _velocity;

        private Tuple<float, float> CalculateFallDamage()
        {
            StartCoroutine(CheckPos());
            float damage = _velocity * 2;
            return new Tuple<float, float>(damage, _velocity);
        }

        private IEnumerator CheckPos()
        {
            //Get the player position, wait one second, then check again
            Vector3 position = Player.transform.position;
            _previousPos = position.y;
            yield return new WaitForSeconds(1);
            _currentPos = position.y;
            //Then the "velocity" is how much has been moved between the seconds
            _velocity = _previousPos - _currentPos;
        }

        #endregion
    }
}