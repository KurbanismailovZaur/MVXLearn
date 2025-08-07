using System;

namespace MVXLearn.UI.Windows.Settings
{
    public class SettingsModel
    {
        private MVXLearn.Settings _settings;

        public bool Sound
        {
            get => _settings.Sound;
            set
            {
                _settings.Sound = value;
                SettingsStateChanged?.Invoke(this);
            }
        }

        public bool Vibration
        {
            get => _settings.Vibration;
            set
            {
                _settings.Vibration = value;
                SettingsStateChanged?.Invoke(this);
            }
        }
        
        public event Action<SettingsModel> SettingsStateChanged; 
        
        public SettingsModel(MVXLearn.Settings settings) => _settings = settings;
    }
}