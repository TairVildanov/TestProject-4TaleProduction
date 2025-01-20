using Game.Modules.CardModule.Declaration;
using Game.Modules.EnemyModule.Implementation;
using Game.Modules.PlayerModule.Implementation;

namespace Game.Modules.CardModule.Implementation
{
    public class CardModel
    {
        public string Name { get; }
        public int EnergyCost { get; }
        private readonly CardEffect _effect;

        public CardModel(string name, int energyCost, CardEffect effect)
        {
            Name = name;
            EnergyCost = energyCost;
            _effect = effect;
        }

        public void Execute(PlayerViewModel player, EnemyViewModel target) => _effect.Apply(player, target);
    }
}