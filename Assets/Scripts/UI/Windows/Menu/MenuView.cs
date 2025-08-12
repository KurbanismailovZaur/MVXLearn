using Azur.WindowsSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Menu
{
    public class MenuView : Window
    {
        [field: SerializeField] public Button PlayButton { get; private set; }
        [field: SerializeField] public Button SettingsButton { get; private set; }
    }
}