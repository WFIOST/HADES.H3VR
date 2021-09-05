using BepInEx.Logging;
using HADES.Core;
using UnityEngine;

namespace HADES.Utilities
{
    public struct PluginInfo
    {
        public const string NAME = "HADES.Core";
        public const string GUID = "com.wfiost.hades";
        public const string VERSION = "1.0.0";
    }

    public static class Logging
    {
        public static void Print(object msg, LogLevel level = LogLevel.Info) => Plugin.ConsoleLogger.Log(level, msg);
        
        
        public static class Debug
        {
            public static void Print(object msg) => Plugin.ConsoleLogger.Log(LogLevel.Debug, msg);
        }
    }

    public class Tuple<T1, T2>
    {
        public Tuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public T1 Item1 { get; }
        public T2 Item2 { get; }
    }


    public static class Extensions
    {
        public static Vector3 Add(this Vector3 vec3, Vector3 add) =>
            new Vector3(vec3.x + add.x, vec3.y + add.y, vec3.z + add.z);
        

        public static Vector3 Add(this Vector3 vec3, float add) => 
            new Vector3(vec3.x + add, vec3.y + add, vec3.z + add);
        
    }
}