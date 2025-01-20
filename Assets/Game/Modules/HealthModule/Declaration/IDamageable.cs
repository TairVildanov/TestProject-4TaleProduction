using R3;

namespace Game.Modules.HealthModule.Declaration
{
    public interface IDamageable
    {
        ReactiveProperty<int> Health { get; }
        int MaxHealth { get; set; }
        ReactiveProperty<float> HealthPercentage { get; }
        void GetDamage(int value);
        void Heal(int value);
        public void ToggleShield(bool isActive, int defence);
    }
}