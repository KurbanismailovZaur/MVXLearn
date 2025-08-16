using System;
using UnityEngine;
using Zenject;

namespace MVXLearn.Camera
{
    public class CameraController : ILateTickable
    {
        public CameraModel Model { get; private set; }
        public CameraView View { get; private set; }
        
        public CameraController(CameraModel model, CameraView view)
        {
            Model = model;
            View = view;
        }
        
        public void LateTick()
        {
            var pos = Model.Target.position;
            pos.y = Model.Height;
            View.CameraTransform.position = Vector3.Lerp(View.CameraTransform.position, pos, Model.Interpolation * Time.deltaTime);
        }
    }
}