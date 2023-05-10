using System;
using System.Media;
using System.Windows.Forms;
using Rage;
[assembly: Rage.Attributes.Plugin("Soundboard", Description = "Increase immersion with real life sounds in game!", Author = "Roheat")]
namespace Soundboard
{
    internal class EntryPoint
    {
        internal static SoundPlayer soundplayer = new SoundPlayer();
        internal static void Main()
        {
            Game.DisplayNotification("commonmenu", "shop_tick_icon", "Soundboard","~b~By Roheat","~g~Loaded Successfully!");
            FileHelper.ReadFile();
            while (true)
            {
                GameFiber.Yield();
                foreach (Soundbite sound in FileHelper.Sounds)
                {
                    if (sound.CheckKeybind())
                    {
                        soundplayer.SoundLocation = sound.filePath;
                        soundplayer.Play();
                    }
                }
            }
        }
        internal static bool CheckModifierKey() => Settings.MenuModifierKey == Keys.None ? true : Game.IsKeyDownRightNow(Settings.MenuModifierKey);
    }
}