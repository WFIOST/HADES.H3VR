using System.Collections;
using BepInEx;
using BepInEx.Logging;
using FistVR;
using UnityEngine;

using static EHADS.H3VR.Common.Logging;

namespace EHADS.H3VR
{
    [BepInPlugin(Common.PluginInfo.GUID, Common.PluginInfo.NAME, Common.PluginInfo.VERSION)]
    public class EHADS : BaseUnityPlugin
    {
        public static ManualLogSource ConsoleLogger;
        
        private static FVRPlayerBody Player => GM.CurrentPlayerBody;

        public EHADS()
        {
            ConsoleLogger = Logger;
            Print($"Loading EHADS version {Common.PluginInfo.VERSION}");
        }
        private void Start()
        {
            Print($"Loaded EHADS version {Common.PluginInfo.VERSION}!");
        }
        
        private void Update()
        {
            if (!GM.IsDead())
            {
                var fallDmg = CalculateFallDamage();
                if (fallDmg.Item2 < 2f)
                {
                    Common.Logging.Debug.Print($"Taking {fallDmg.Item1} damage, player velocity {fallDmg.Item2}");
                    Player.HarmPercent(fallDmg.Item1 / 100); //Harm the player the percentage that they fell
                }
            }
        }

        #region FALL DAMAGE
        private float _previousPos;
        private float _currentPos;
        private float _velocity;

        private Common.Tuple<float, float> CalculateFallDamage()
        {
            float damage;
            
            Coroutine async = StartCoroutine(CheckPos());
            damage = _velocity * 2;

            return new Common.Tuple<float, float>(damage, _velocity);
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