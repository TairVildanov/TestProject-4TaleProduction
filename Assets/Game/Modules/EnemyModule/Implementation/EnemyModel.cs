using Game.Modules.HealthModule.Implementation;

namespace Game.Modules.EnemyModule.Implementation
{
    public class EnemyModel
    {
        public string Name { get; }
        public Health Health { get; private set; }
        public int Damage { get; private set; }
        public int Defense { get; private set; }

        public EnemyModel(string name, int health, int damage, int defense)
        {
            Name = name;
            Health = new Health(health);
            Damage = damage;
            Defense = defense;
        }
    }
}