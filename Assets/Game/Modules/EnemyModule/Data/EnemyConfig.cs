using UnityEngine;

namespace Game.Modules.EnemyModule.Data
{
    [CreateAssetMenu(fileName = "Enemy Config", menuName = "Enemy Config", order = 1)]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public int Defense { get; private set; }
    }
}