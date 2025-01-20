using System;
using Game.Modules.HealthModule.Declaration;
using R3;

namespace Game.Modules.PlayerModule.Implementation
{
    public class PlayerViewModel : IDamageable, IDisposable
    {
        public ReactiveProperty<int> Health { get; }
        public int MaxHealth { get; set; }
        public ReactiveProperty<float> HealthPercentage { get; }
        public ReactiveProperty<int> Energy { get; }
        private int MaxEnergy { get; }
        public ReactiveProperty<float> EnergyPercentage { get; }
        public ReactiveProperty<bool> IsShieldActive { get; }
        public ReactiveProperty<int> ShieldPower { get; }

        private readonly PlayerModel _model;
        private readonly CompositeDisposable _disposable = new();

        public PlayerViewModel(PlayerModel model)
        {
            _model = model;
            Health = new ReactiveProperty<int>(model.Health.HealthAmount.Value);
            MaxHealth = model.Health.MaxHealth;
            HealthPercentage = new ReactiveProperty<float>((float)Health.Value / MaxHealth);
            Energy = new ReactiveProperty<int>(model.Energy.Value);
            MaxEnergy = model.MaxEnergy;
            EnergyPercentage = new ReactiveProperty<float>((float)Energy.Value / MaxEnergy);
            IsShieldActive = new ReactiveProperty<bool>(_model.Health.IsShieldActive.Value);
            ShieldPower = new ReactiveProperty<int>(_model.Health.ShieldPower.Value);
            _model.Health.HealthAmount.Subscribe(UpdateHealth).AddTo(_disposable);
            _model.Energy.Subscribe(UpdateEnergy).AddTo(_disposable);
            Health.Subscribe(UpdateHealthPercentage).AddTo(_disposable);
            _model.Health.IsShieldActive.Subscribe(UpdateShield).AddTo(_disposable);
            _model.Health.ShieldPower.Subscribe(UpdateShieldPower).AddTo(_disposable);
        }

        private void UpdateShield(bool isActive)
        {
            IsShieldActive.Value = isActive;
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

        private void UpdateEnergy(int value)
        {
            Energy.Value = value;
        }

        public void DecreaseEnergy(int value)
        {
            AddEnergy(-value);
        }

        public void IncreaseEnergy(int value)
        {
            AddEnergy(value);
        }

        public void RestoreEnergy()
        {
            AddEnergy(MaxEnergy);
        }

        private void AddEnergy(int value)
        {
            _model.Energy.Value += value;
            if (_model.Energy.Value > MaxEnergy)
            {
                _model.Energy.Value = MaxEnergy;
            }
            else if (_model.Energy.Value < 0)
            {
                _model.Energy.Value = 0;
            }

            EnergyPercentage.Value = (float)_model.Energy.Value / MaxEnergy;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}