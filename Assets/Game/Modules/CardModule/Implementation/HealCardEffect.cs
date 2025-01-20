using Game.Modules.CardModule.Data;
using Game.Modules.CardModule.Declaration;
using Game.Modules.EnemyModule.Implementation;
using Game.Modules.PlayerModule.Implementation;

namespace Game.Modules.CardModule.Implementation
{
    public class HealCardEffect : CardEffect
    {
        private readonly CardHealEffectConfig _config;

        public HealCardEffect(CardHealEffectConfig cardEffectConfig) : base(cardEffectConfig)
        {
            _config = cardEffectConfig;
        }

        public override void Apply(PlayerViewModel player, EnemyViewModel target)
        {
            player.Heal(_config.Heal);
        }
    }
}