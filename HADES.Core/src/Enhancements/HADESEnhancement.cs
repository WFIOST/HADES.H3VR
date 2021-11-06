using UnityEngine;
using FistVR;

using static HADES.Core.Plugin;
using static HADES.Utilities.Logging;

namespace HADES.Core
{
    public class HADESEnhancement : MonoBehaviour
    {
        public FVRPlayerBody Player => GM.CurrentPlayerBody;
        public FVRMovementManager MovementManager => GM.CurrentMovementManager;
    }
}