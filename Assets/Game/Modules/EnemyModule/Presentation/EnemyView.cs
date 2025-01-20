using Game.Modules.EnemyModule.Implementation;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Modules.EnemyModule.Presentation
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private Image _healthBar;
        [SerializeField] private GameObject _shield;
        [SerializeField] private TextMeshProUGUI _shieldPowerText;

        private readonly CompositeDisposable _disposable = new();

        public void Initialize(EnemyViewModel viewModel)
        {
            viewModel.Health.Subscribe(UpdateHealth).AddTo(_disposable);
            viewModel.HealthPercentage.Subscribe(UpdateHealthBar).AddTo(_disposable);
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

        private void UpdateHealth(int value)
        {
            _healthText.text = $"{value}";
        }

        private void UpdateHealthBar(float value)
        {
            _healthBar.fillAmount = value;
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}