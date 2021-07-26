using FistVR;
using HADES.Configs;
using HADES.Utilities;
using UnityEngine;

namespace HADES.Core
{
    public class HADESEnhancement<TConfigEntry> : MonoBehaviour where TConfigEntry : ConfigEntry, new()
    {
        protected readonly TConfigEntry Config = new TConfigEntry();
        protected FVRPlayerBody Player => GM.CurrentPlayerBody;
        protected FVRMovementManager MovementManager => GM.CurrentMovementManager;

        protected void Print(object message)
        {
            Logging.Print($"({Config.CategoryName}) - {message}");
        }
    }
}