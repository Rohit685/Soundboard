using System;
using Rage;

namespace Soundboard
{
    internal static class Logger
    {
        internal static string defaultInfo = "Soundboard[{0}][{1}]: {2}";
        
        internal static void Normal(string logLocation, string msg)
        {
            Game.LogTrivial(String.Format(defaultInfo,"NORMAL",logLocation,msg));
        }

        internal static void Warning(string logLocation,string msg)
        {
            Game.LogTrivial(String.Format(defaultInfo,"WARNING",logLocation,msg));
        }

        internal static void Error(string logLocation, string msg)
        {
            Game.LogTrivial(String.Format(defaultInfo,"ERROR",logLocation,msg));
        }
    }
}