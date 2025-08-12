using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Settings
{
    public class SettingInstaller : MonoInstaller<SettingInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<SettingsModel>().AsCached();
            Container.Bind<SettingsView>().FromComponentInHierarchy().AsCached();
            Container.Bind<SettingsController>().AsCached().NonLazy();
        }
    }
}