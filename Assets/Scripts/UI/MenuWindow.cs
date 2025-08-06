using MVXLearn.Controllers;
using MVXLearn.Models;
using MVXLearn.Views;
using UnityEngine;
using Zenject;

namespace MVXLearn.UI
{
    public class MenuWindow : MonoBehaviour, IInitializable
    {
        [SerializeField] private MenuView _menuView;

        private MenuController _controller;

        [Inject] private SignalBus _signalBus;

        public void Initialize()
        {
            var model = new MenuModel();
            _controller = new MenuController(model, _menuView, _signalBus);
        }
    }
}