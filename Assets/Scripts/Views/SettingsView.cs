using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVXLearn.Views
{
    public class SettingsView : MonoBehaviour
    {
        public Button closeButton;
        
        public Button soundButton;
        public TMP_Text soundText;
        
        public Button vibrationButton;
        public TMP_Text vibrationText;

        public void SetButtonsState(bool sound, bool vibration)
        {
            soundText.text = $"Sound: {(sound ? "on" : "off")}";
            vibrationText.text = $"Vibration: {(vibration ? "on" : "off")}";
        }
    }
}