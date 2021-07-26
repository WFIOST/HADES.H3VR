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
        protected FVRPlayerBody Player => GM.CurrentPlayerBody;
        
        /*
        * We have
        * ```cs
        * Destroy(this);
        * ```
        * so there is literally no way the feature could be loaded
        */
        
        protected void Awake()
        {
            if (!Config.Enabled) Destroy(this);
        }

        protected void Start()
        {
            if (!Config.Enabled) Destroy(this);
            Print($"Injected enhancement {Config.CategoryName}");
        }

        protected void Update()
        {
            if (!Config.Enabled) Destroy(this);
        }

        protected virtual void FixedUpdate()
        {
            if (!Config.Enabled) Destroy(this);
        }

        protected void Print(object message) => Logging.Print($"({Config.CategoryName}) - {message}");
    }
}