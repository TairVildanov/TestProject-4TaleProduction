using System;
using Game.Modules.HealthModule.Declaration;
using R3;

namespace Game.Modules.EnemyModule.Implementation
{
    public class EnemyViewModel : IDamageable, IDisposable
    {
        public ReactiveProperty<int> Health { get; }
        public int MaxHealth { get; set; }
        public ReactiveProperty<float> HealthPercentage { get; }
        public ReactiveProperty<bool> IsShieldActive { get; }

        public int Damage { get; private set; }
        public int Defense { get; private set; }
        public ReactiveProperty<int> ShieldPower { get; }


        private readonly EnemyModel _model;

        private readonly CompositeDisposable _disposable = new();

        public EnemyViewModel(EnemyModel model)
        {
            _model = model;
            Health = new ReactiveProperty<int>(model.Health.HealthAmount.Value);
            MaxHealth = model.Health.MaxHealth;
            HealthPercentage = new ReactiveProperty<float>((float)Health.Value / MaxHealth);
            Damage = _model.Damage;
            Defense = _model.Defense;
            IsShieldActive = new ReactiveProperty<bool>(_model.Health.IsShieldActive.Value);
            ShieldPower = new ReactiveProperty<int>(_model.Health.ShieldPower.Value);
            _model.Health.HealthAmount.Subscribe(UpdateHealth).AddTo(_disposable);
            Health.Subscribe(UpdateHealthPercentage).AddTo(_disposable);
            _model.Health.IsShieldActive.Subscribe(UpdateShield).AddTo(_disposable);
            _model.Health.ShieldPower.Subscribe(UpdateShieldPower).AddTo(_disposable);
        }

        private void UpdateShield(bool value)
        {
            IsShieldActive.Value = value;
        }
        private void UpdateShieldPower(int value)
        {
            ShieldPower.Value = value;
        }

        private void UpdateHealth(int value)
        {
            Health.Value = value;
        }

        private void UpdateHealthPercentage(int value)
        {
            HealthPercentage.Value = (float)value / MaxHealth;
        }

        public void GetDamage(int value)
        {
            _model.Health.Damage(value);
        }

        public void Heal(int value)
        {
            _model.Health.Heal(value);
        }

        public void ToggleShield(bool isActive, int defence)
        {
            _model.Health.ToggleShield(isActive, defence);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}