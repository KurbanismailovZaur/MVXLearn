using UnityEngine;
using Zenject;

namespace MVXLearn.Character
{
    public class CharacterController : ITickable
    {
        [Inject] public CharacterModel Model { get; private set; }
        [Inject] public CharacterView View { get; private set; }

        public CharacterController(CharacterModel model, CharacterView view)
        {
            Model = model;
            View = view;
        }

        public void Tick()
        {
            var horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * Model.CharacterSpeed;
            var vertical = Input.GetAxis("Vertical") * Time.deltaTime * Model.CharacterSpeed;
            View.CharacterTransform.Translate(horizontal, 0f, vertical, Space.World);
            
            var ray = Model.Camera.ScreenPointToRay(Input.mousePosition);

            if (!Model.GroundCollider.Raycast(ray, out var hit, float.PositiveInfinity))
                return;

            var transform = View.CharacterTransform;
            
            var direction = hit.point - transform.position;
            direction.y = 0;
            direction.Normalize();

            var rotateSpeed = Model.CharacterRotateAngle * Time.deltaTime;
            transform.forward = Vector3.RotateTowards(transform.forward, direction, rotateSpeed, 1f);
        }
    }
}