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
        private string _saveFileName;
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
            _saveFileName = gameConfig.saveFileName;
        }

        public void Initialize()
        {
            var pathToFile = Path.Combine(Application.persistentDataPath, _saveFileName);
            
            if (File.Exists(pathToFile))
                _settingsData = JObject.Parse(File.ReadAllText(pathToFile)).ToObject<SettingsData>();
            else
                _settingsData = new SettingsData();
        }
    }
}