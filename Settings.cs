using System.Windows.Forms;
using Rage;


namespace Soundboard
{
    internal class Settings
    {
        internal static Keys MenuKey = Keys.L;
        internal static Keys MenuModifierKey = Keys.LShiftKey;
        internal static InitializationFile iniFile;
        
        internal static void Initialize()
        {
            try
            {
                iniFile = new InitializationFile(@"Plugins/Soundboard.ini");
                iniFile.Create();
                MenuKey = iniFile.ReadEnum("Keybinds", "MenuKey", MenuKey);
                MenuModifierKey = iniFile.ReadEnum("Keybinds", "MenuModifierKey", MenuModifierKey);
            }
            catch(System.Exception e)
            {
                string error = e.ToString();
                Game.LogTrivial("Soundboard: ERROR IN 'Settings.cs, Initialize()': " + error);
                Game.DisplayNotification("Soundboard: Error Occured");
            }
        }
    }
}