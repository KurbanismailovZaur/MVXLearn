using MVXLearn.Controllers;
using MVXLearn.Models;
using MVXLearn.Views;
using UnityEngine;
using Zenject;

namespace MVXLearn.UI
{
    public class SettingsWindow : MonoBehaviour, IInitializable
    {
        [SerializeField] private SettingsView _view;

        private SettingsController _controller;

        [Inject] private SignalBus _signalBus;
        [Inject] private SettingsModel _model;

        public void Initialize()
        {
            _controller = new SettingsController(_model, _view, _signalBus);
        }
    }
}