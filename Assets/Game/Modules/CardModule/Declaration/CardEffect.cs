using Game.Modules.EnemyModule.Implementation;
using Game.Modules.PlayerModule.Implementation;
using UnityEngine;

namespace Game.Modules.CardModule.Declaration
{
    public abstract class CardEffect
    {
        private CardEffectConfig Config { get; }

        protected CardEffect(CardEffectConfig config)
        {
            Config = config;
        }

        public string Text => Config.Text;
        public Sprite Icon => Config.Icon;
        public int EnergyCost => Config.EnergyCost;

        public abstract void Apply(PlayerViewModel player, EnemyViewModel target);
    }

}