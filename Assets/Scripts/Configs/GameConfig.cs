using UnityEngine;

namespace MVXLearn.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/Game Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public string saveFileName;
    }
}