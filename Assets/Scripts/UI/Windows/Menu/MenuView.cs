using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Menu
{
    public class MenuView
    {
        [Inject(Id = "play_button")] public Button PlayButton { get; private set; }
        [Inject(Id = "settings_button")] public Button SettingsButton { get; private set; }
    }
}