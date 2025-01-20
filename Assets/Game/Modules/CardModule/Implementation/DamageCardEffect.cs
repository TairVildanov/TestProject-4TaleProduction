using Game.Modules.CardModule.Data;
using Game.Modules.CardModule.Declaration;
using Game.Modules.EnemyModule.Implementation;
using Game.Modules.PlayerModule.Implementation;

namespace Game.Modules.CardModule.Implementation
{
    public class DamageCardEffect : CardEffect
    {
        private readonly CardDamageEffectConfig _config;
        public DamageCardEffect(CardDamageEffectConfig cardEffectConfig) : base(cardEffectConfig)
        {
            _config = cardEffectConfig;
        }

        public override void Apply(PlayerViewModel player, EnemyViewModel target)
        {
            target.GetDamage(_config.Damage);
        }
    }
}