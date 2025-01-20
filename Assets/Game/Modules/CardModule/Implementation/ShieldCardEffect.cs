using Game.Modules.CardModule.Data;
using Game.Modules.CardModule.Declaration;
using Game.Modules.EnemyModule.Implementation;
using Game.Modules.PlayerModule.Implementation;

namespace Game.Modules.CardModule.Implementation
{
    public class ShieldCardEffect : CardEffect
    {
        private readonly CardShieldEffectConfig _config;

        public ShieldCardEffect(CardShieldEffectConfig cardEffectConfig) : base(cardEffectConfig)
        {
            _config = cardEffectConfig;
        }

        public override void Apply(PlayerViewModel player, EnemyViewModel target)
        {
            player.ToggleShield(true, _config.Shield);
        }
    }
}