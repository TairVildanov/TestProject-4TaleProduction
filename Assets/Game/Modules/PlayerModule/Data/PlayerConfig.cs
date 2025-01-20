using UnityEngine;

namespace Game.Modules.PlayerModule.Data
{
    [CreateAssetMenu(fileName = "Player Config", menuName = "Player Config", order = 1)]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public int Energy { get; private set; }
        [field: SerializeField] public int MaxEnergy { get; private set; }
    }
}