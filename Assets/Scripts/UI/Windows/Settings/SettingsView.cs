using System;
using Azur.WindowsSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Settings
{
    public class SettingsView : FadingWindow
    {
        [field: SerializeField] public Button CloseButton { get; private set; }
        [field: SerializeField] public Button SoundButton { get; private set; }
        [field: SerializeField] public Button VibrationButton { get; private set; }
        [field: SerializeField] public TMP_Text SoundText { get; private set; }
        [field: SerializeField] public TMP_Text VibrationText { get; private set; }
        
        public void SetButtonsState(bool sound, bool vibration)
        {
            SoundText.text = $"Sound: {GetState(sound)}";
            VibrationText.text = $"Vibration: {GetState(vibration)}";
        }

        private static string GetState(bool state) => state ? "on" : "off";
    }
}