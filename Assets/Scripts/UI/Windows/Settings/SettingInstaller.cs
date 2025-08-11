using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Settings
{
    public class SettingInstaller : MonoInstaller<SettingInstaller>
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _soundButton;
        [SerializeField] private Button _vibrationButton;
        [SerializeField] private TMP_Text _soundText;
        [SerializeField] private TMP_Text _vibrationText;
        
        public override void InstallBindings()
        {
            Container.Bind<SettingsModel>().AsCached();

            Container.BindInstance(_closeButton).WithId("close_button");
            Container.BindInstance(_soundButton).WithId("sound_button");
            Container.BindInstance(_vibrationButton).WithId("vibration_button");
            Container.BindInstance(_soundText).WithId("sound_text");
            Container.BindInstance(_vibrationText).WithId("vibration_text");
            Container.Bind<SettingsView>().AsCached();
            
            Container.Bind<SettingsController>().AsCached();
        }
    }
}