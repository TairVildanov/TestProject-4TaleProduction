using Game.Modules.PlayerModule.Implementation;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Modules.PlayerModule.Presentation
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private Image _healthBar;

        [SerializeField] private TextMeshProUGUI _energyText;
        [SerializeField] private Image _energyBar;

        [SerializeField] private GameObject _shield;
        [SerializeField] private TextMeshProUGUI _shieldPowerText;

        private readonly CompositeDisposable _disposable = new();

        public void Initialize(PlayerViewModel viewModel)
        {
            viewModel.Health.Subscribe(UpdateHealthText).AddTo(_disposable);
            viewModel.HealthPercentage.Subscribe(UpdateHealthBar).AddTo(_disposable);
            viewModel.Energy.Subscribe(UpdateEnergyText).AddTo(_disposable);
            viewModel.EnergyPercentage.Subscribe(UpdateEnergyBar).AddTo(_disposable);
            viewModel.IsShieldActive.Subscribe(UpdateShieldImage).AddTo(_disposable);
            viewModel.ShieldPower.Subscribe(UpdateShieldPowerText).AddTo(_disposable);
        }

        private void UpdateShieldImage(bool value)
        {
            _shield.SetActive(value);
        }

        private void UpdateShieldPowerText(int value)
        {
            _shieldPowerText.text = value.ToString();
        }

        private void UpdateHealthText(int value)
        {
            _healthText.text = $"{value}";
        }

        private void UpdateHealthBar(float value)
        {
            _healthBar.fillAmount = value;
        }

        private void UpdateEnergyText(int value)
        {
            _energyText.text = $"{value}";
        }

        private void UpdateEnergyBar(float value)
        {
            _energyBar.fillAmount = value;
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}