using System;
using System.IO;
using MVXLearn.Configs;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Zenject;

namespace MVXLearn.Persistence
{
    public class SettingsService : IInitializable
    {
        private string _pathToFile;
        private SettingsData _settingsData;

        public bool Sound
        {
            get => _settingsData.sound;
            set
            {
                _settingsData.sound = value;
                SettingsDataChanged?.Invoke(this);
            }
        }

        public bool Vibration
        {
            get => _settingsData.vibration;
            set
            {
                _settingsData.vibration = value;
                SettingsDataChanged?.Invoke(this);
            }
        }
        
        public event Action<SettingsService> SettingsDataChanged; 
        
        public SettingsService(GameConfig gameConfig)
        {
            _pathToFile = Path.Combine(Application.persistentDataPath, gameConfig.saveFileName);
        }

        public void Initialize()
        {
            if (File.Exists(_pathToFile))
                _settingsData = JObject.Parse(File.ReadAllText(_pathToFile)).ToObject<SettingsData>();
            else
                _settingsData = new SettingsData();
        }

        public void SaveToFile()
        {
            File.WriteAllText(_pathToFile, JObject.FromObject(_settingsData).ToString());
        }
    }
}