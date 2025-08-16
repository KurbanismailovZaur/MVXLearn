using UnityEngine;
using Zenject;

namespace MVXLearn.Camera
{
    public class CameraModel
    {
        [Inject(Id = "height")]
        public float Height { get; private set; }
        
        [Inject(Id = "interpolation")]
        public float Interpolation { get; private set; }
        
        [Inject(Id = "target")]
        public Transform Target { get; private set; }
    }
}