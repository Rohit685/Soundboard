using System;
using Rage;
using Rage.Attributes;
using Rage.Native;
using RAGENativeUI;
using RAGENativeUI.Elements;
using RAGENativeUI.PauseMenu;
namespace Soundboard.RNUIMenu
{
    internal class Menu
    {
        internal static MenuPool pool;
        internal static UIMenu mainMenu;


        internal static void CreateMainMenu()
        {
            pool = new MenuPool();
            mainMenu = new UIMenu("Soundboard","Main Menu");
            AddSoundbites();
            
            mainMenu.AllowCameraMovement = true;
            mainMenu.MouseControlsEnabled = false;
            
            mainMenu.OnItemSelect += OnMainMenuSelect;
            pool.Add(mainMenu);
            GameFiber.StartNew(ProcessMenus);
        }

        internal static void AddSoundbites()
        {
            foreach (Soundbite bite in FileHelper.Sounds)
            {
                mainMenu.AddItem(new UIMenuItem($"{bite.menuName}",$"{bite.ModifierKey.ToString()} + {bite.Key.ToString()}"));
            }
        }

        internal static void OnMainMenuSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            Game.DisplayNotification("Menu only used for keybinds. Clicking will not play sound.");
        }
        private static void ProcessMenus()
        {

            while (true)
            {
                GameFiber.Yield();
                pool.ProcessMenus();
            }
        }
        
        private static bool firstTime = true;
        [ConsoleCommand]
        public static void OpenSoundboardMenu()
        {
            if (firstTime)
            {
                GameFiber.StartNew(ProcessMenus);
                firstTime = false;
            }
            GameFiber.WaitUntil(() => !Game.Console.IsOpen);
            GameFiber.Wait(750);
            mainMenu.Visible = true;
        } 

    }
}