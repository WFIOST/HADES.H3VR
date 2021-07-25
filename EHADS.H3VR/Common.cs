using BepInEx.Logging;
using FistVR;

namespace EHADS.H3VR
{
    public static class Common
    {
        public struct PluginInfo
        {
            public const string NAME =      "EHADS.H3VR";
            public const string GUID =      "net.frityet.ehads";
            public const string VERSION =   "1.0.0";
        }

        public static class Logging
        {
            public static void Print(object msg, LogLevel level = LogLevel.Info) => EHADS.ConsoleLogger.Log(level, msg);

            public static class Debug
            {
                public static void Print(object msg) => EHADS.ConsoleLogger.Log(LogLevel.Debug, msg);
            }
        }

        public class Tuple<T1, T2>
        {
            public T1 Item1 { get; private set; }
            public T2 Item2 { get; private set; }

            public Tuple(T1 item1, T2 item2)
            {
                Item1 = item1;
                Item2 = item2;
            }
        }
    }
}