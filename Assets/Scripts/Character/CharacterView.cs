using UnityEngine;

namespace MVXLearn.Character
{
    public class CharacterView : MonoBehaviour
    {
        [field: SerializeField] public Transform CharacterTransform { get; private set; }
    }
}