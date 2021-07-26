using FistVR;
using UnityEngine;

namespace HADES.Core
{
    public class HADESEnhancement : MonoBehaviour
    {
        protected FVRPlayerBody Player => GM.CurrentPlayerBody;
    }
}