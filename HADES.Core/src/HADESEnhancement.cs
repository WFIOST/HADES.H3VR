using System;
using FistVR;
using HADES.Configs;
using HADES.Utilities;
using UnityEngine;

namespace HADES.Core
{
    public class HADESEnhancement<TConfigEntry> : MonoBehaviour where TConfigEntry : ConfigEntry, new()
    {
        protected readonly TConfigEntry Config = new TConfigEntry();
        protected FVRPlayerBody      Player          => GM.CurrentPlayerBody;
        protected FVRMovementManager MovementManager => GM.CurrentMovementManager;

        protected void Print(object message)
        {
            Logging.Debug.Print($"({Config.CategoryName}) - {message}");
        }
        
        private void Update()
        {
            if (Config == null)
            {
                Print("Config null!");
            }
        }
    }
}