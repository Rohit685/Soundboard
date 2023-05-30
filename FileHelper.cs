using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Rage;

namespace Soundboard
{
    internal static class FileHelper
    {
     
        internal static string ConfigFilePath = @"Plugins\SoundboardConfig.txt";
        internal static List<Soundbite> Sounds = new List<Soundbite>();

        internal static void ReadFile()
        {
            List<string> strings = new List<string>();

            using (FileStream fileStream = new FileStream(ConfigFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                // Read the file content
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    // Loop through the lines and split each line into an array
                    while (!reader.EndOfStream)
                    {
                        strings.Add(reader.ReadLine());
                    }
                }
            }

            foreach (string line in strings)
            {
                Soundbite newSound;
                if (string.IsNullOrWhiteSpace(line))
                {
                    Logger.Warning("ReadFile() in FileHelper.cs", "line was empty. Skipping line");
                    continue;
                }
                string commentChecker = line.Substring(0, 2);
                if (commentChecker.Equals("//"))
                {
                    Logger.Warning("ReadFile() in FileHelper.cs", "line started with //. Skipping line");
                    continue;
                }
                string menuName = line.Split('=')[0].Trim();
                string[] splitLine = line.Split('=');
                string soundProperties = splitLine[1];
                string[] soundPropertiesSplit = soundProperties.Split(',');
                string fileName = soundPropertiesSplit[0].Trim();
                string fileNameExtension = fileName.Split('.')[1];
                if (!fileNameExtension.Equals("wav"))
                {
                    Game.DisplayNotification($"Filename Invalid: {fileName}. Only .wav files are accepted");
                    Logger.Warning("ReadFile() in FileHelper.cs", "File was not a .wav file.");
                    continue;
                }
                
                Keys ModifierKey;
                bool parseSuccessful = Enum.TryParse(soundPropertiesSplit[1].Trim(), false, out ModifierKey);
                if (!parseSuccessful)
                {
                    ModifierKey = Keys.None;
                }
                
                
                Keys Key;
                parseSuccessful = Enum.TryParse(soundPropertiesSplit[2].Trim(), false, out Key);
                if (!parseSuccessful)
                {
                    Key = Keys.None;
                }
                
                newSound = new Soundbite(menuName, fileName, ModifierKey, Key);
                Sounds.Add(newSound);
                Logger.Normal($"ReadFile() in FileHelper.cs",$"Successfully added sound: {menuName}");
            }
        }
        

    }
}