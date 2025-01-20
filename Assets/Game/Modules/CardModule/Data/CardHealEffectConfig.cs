using Game.Modules.CardModule.Declaration;
using UnityEngine;

namespace Game.Modules.CardModule.Data
{
    [CreateAssetMenu(fileName = "Card Heal Effect", menuName = "Cards Effects/Card Heal Effect")]
    public class CardHealEffectConfig : CardEffectConfig
    {
        [field: SerializeField] public int Heal { get; private set; }
    }
}