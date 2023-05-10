using System;
using System.Windows.Forms;
using Rage;
namespace Soundboard
{
    internal class Soundbite
    {
        internal string menuName {get; set; }
        internal string fileName {get; set; }
        
        internal string filePath { get; set; }
        internal Keys ModifierKey { get; set; }
        internal Keys Key { get; set; }
        internal static string soundLocationFolder = @"Plugins\Soundboard\";
        
        internal Soundbite(string menuName,string fileName, Keys ModifierKey, Keys Key)
        {
            this.menuName = menuName;
            this.fileName = fileName;
            this.ModifierKey = ModifierKey;
            this.Key = Key;
            filePath = soundLocationFolder + fileName;
        }
        
        internal bool CheckKeybind()
        {
            if (ModifierKey == Keys.None && Key == Keys.None)
            {
                return false;
            }
            else
            {
                return (ModifierKey == Keys.None ? true : Game.IsKeyDownRightNow(ModifierKey)) && Game.IsKeyDown(Key);
            }
        }

        override public string ToString()
        {
            return $"{menuName}={fileName},{ModifierKey.ToString()},{Key.ToString()}";
        }


    }
}