using UnityEngine;

namespace Game.Modules.CardModule.Declaration
{
    public abstract class CardEffectConfig : ScriptableObject
    {
        [field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public int EnergyCost { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}