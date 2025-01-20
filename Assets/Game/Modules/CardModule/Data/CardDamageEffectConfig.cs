using Game.Modules.CardModule.Declaration;
using UnityEngine;

namespace Game.Modules.CardModule.Data
{
    [CreateAssetMenu(fileName = "Card Damage Effect", menuName = "Cards Effects/Card Damage Effect")]
    public class CardDamageEffectConfig : CardEffectConfig
    {
        [field: SerializeField] public int Damage { get; private set; }
    }
}