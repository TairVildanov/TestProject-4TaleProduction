using Game.Modules.CardModule.Declaration;
using UnityEngine;

namespace Game.Modules.CardModule.Data
{
    [CreateAssetMenu(fileName = "Card Shield Effect", menuName = "Cards Effects/Card Shield Effect")]
    public class CardShieldEffectConfig : CardEffectConfig
    {
        [field: SerializeField] public int Shield { get; private set; }
    }
}