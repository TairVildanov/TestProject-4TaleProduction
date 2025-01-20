using Game.Modules.HealthModule.Implementation;
using R3;

namespace Game.Modules.PlayerModule.Implementation
{
    public class PlayerModel
    {
        public Health Health { get; private set; }
        public ReactiveProperty<int> Energy { get; set; }
        public int MaxEnergy { get; }

        public PlayerModel(int health, int energy, int maxEnergy)
        {
            Health = new Health(health);
            Energy = new ReactiveProperty<int>(energy);
            MaxEnergy = maxEnergy;
        }
    }
}