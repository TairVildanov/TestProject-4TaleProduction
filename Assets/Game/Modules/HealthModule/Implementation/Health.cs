using R3;
using UnityEngine;

namespace Game.Modules.HealthModule.Implementation
{
    public class Health
    {
        public ReactiveProperty<int> HealthAmount { get; }
        public int MaxHealth { get; }
        public ReactiveProperty<bool> IsShieldActive { get; } = new(false);
        public ReactiveProperty<int> ShieldPower { get; } = new(0);

        public Health(int healthAmount)
        {
            MaxHealth = healthAmount;
            HealthAmount = new ReactiveProperty<int>(healthAmount);
        }

        public void ToggleShield(bool isActive, int shieldPower)
        {
            if (shieldPower > 0)
            {
                ShieldPower.Value += shieldPower;
            }
            IsShieldActive.Value = isActive && ShieldPower.Value > 0;
        }

        public void Heal(int amount)
        {
            HealthAmount.Value = Mathf.Min(HealthAmount.Value + amount, MaxHealth);
        }

        public void Damage(int amount)
        {
            var damageToApply = amount;

            if (IsShieldActive.Value)
            {
                damageToApply = Mathf.Max(amount - ShieldPower.Value, 0);
                ShieldPower.Value -= damageToApply;
                if (ShieldPower.Value <= 0)
                {
                    ToggleShield(false, 0);
                }
            }

            HealthAmount.Value = Mathf.Max(HealthAmount.Value - damageToApply, 0);
        }
    }
}