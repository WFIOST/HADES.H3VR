using FistVR;
using HADES.Config;
using UnityEngine;

namespace HADES.Core
{
    public class HADESEnhancement<TConfigEntry> : MonoBehaviour where TConfigEntry : ConfigEntry, new()
    {
        protected TConfigEntry Config = new TConfigEntry();
        protected FVRPlayerBody Player => GM.CurrentPlayerBody;
    }
}