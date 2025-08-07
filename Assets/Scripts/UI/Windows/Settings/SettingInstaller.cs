using UnityEngine;
using Zenject;

namespace MVXLearn.UI.Windows.Settings
{
    public class SettingInstaller : MonoInstaller<SettingInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<SettingsModel>().AsSingle();
            Container.Bind<SettingsView>().FromComponentInHierarchy().AsCached();
            Container.Bind<SettingsController>().AsCached();
        }
    }
}