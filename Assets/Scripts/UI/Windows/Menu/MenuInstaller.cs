using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Menu
{
    public class MenuInstaller : MonoInstaller<MenuInstaller>
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsbutton;
        
        public override void InstallBindings()
        {
            Container.Bind<MenuModel>().AsCached();
            
            Container.BindInstance(_playButton).WithId("play_button");
            Container.BindInstance(_settingsbutton).WithId("settings_button");
            Container.Bind<MenuView>().AsCached();
            
            Container.Bind<MenuController>().AsCached();
        }
    }
}