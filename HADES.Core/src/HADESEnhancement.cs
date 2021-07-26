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

        private void Awake()
        {
            if (!Config.Enabled) return;
        }

        public virtual void Start()
        {
            if (!Config.Enabled) return;
            Print($"Injected enhancement {Config.CategoryName}");
        }

        private void Update()
        {
            if (!Config.Enabled) return;
        }

        private void FixedUpdate()
        {
            if (!Config.Enabled) return;
        }

        protected void Print(object message) => Logging.Print($"({Config.CategoryName}) - {message}");
    }
}