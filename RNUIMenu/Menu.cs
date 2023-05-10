using System;
using Rage;
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
                mainMenu.AddItem(new UIMenuItem($"{bite.menuName}",$"{bite.ModifierKey.ToString()},{bite.Key.ToString()}"));
            }
        }

        internal static void OnMainMenuSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            Soundbite bite = FileHelper.Sounds[index];
            EntryPoint.soundplayer.SoundLocation = bite.filePath;
            EntryPoint.soundplayer.Play();
        }
        private static void ProcessMenus()
        {

            while (true)
            {
                GameFiber.Yield();

                pool.ProcessMenus();

                if (Game.IsKeyDown(Settings.MenuKey) && EntryPoint.CheckModifierKey() && !UIMenu.IsAnyMenuVisible && !TabView.IsAnyPauseMenuVisible)
                {
                    mainMenu.Visible = true;
                }
            }
        }

    }
}