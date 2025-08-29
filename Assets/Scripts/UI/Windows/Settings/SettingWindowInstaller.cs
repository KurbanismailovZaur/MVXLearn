using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Settings
{
    public class SettingWindowInstaller : MonoInstaller<SettingWindowInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<SettingsWindowModel>().AsCached().NonLazy();
            Container.Bind<SettingsWindowView>().FromComponentInHierarchy().AsCached();
            Container.Bind<SettingsWindowController>().AsCached();
        }
    }
}