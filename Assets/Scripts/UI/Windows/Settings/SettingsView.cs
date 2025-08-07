using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVXLearn.UI.Windows.Settings
{
    public class SettingsView : MonoBehaviour
    {
        public Button closeButton;
        
        public Button soundButton;
        public TMP_Text soundText;
        
        public Button vibrationButton;
        public TMP_Text vibrationText;
        private SettingsModel _model;

        public SettingsView(SettingsModel model)
        {
            _model = model;
            _model.SettingsStateChanged += OnModelSettingsStateChangedEventHandler;
        }

        public void UpdateState()
        {
            soundText.text = $"Sound: {(_model.Sound ? "on" : "off")}";
            vibrationText.text = $"Vibration: {(_model.Vibration ? "on" : "off")}";
        }

        private void OnModelSettingsStateChangedEventHandler(SettingsModel model) => UpdateState();
    }
}