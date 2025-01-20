using System;
using Game.Modules.CardModule.Declaration;
using Game.Modules.EnemyModule.Implementation;
using Game.Modules.PlayerModule.Implementation;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Modules.CardModule.Presentation
{
    public class CardView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI _cardText;
        [SerializeField] private Image _cardIcon;
        [SerializeField] private TextMeshProUGUI _energyCostText;

        public CardEffect CardEffect { get; private set; }
        private PlayerViewModel _playerViewModel;
        private EnemyViewModel _enemyViewModel;
        private Action<CardView> _onCardUsed;

        public void Initialize(CardEffect cardEffect, PlayerViewModel playerViewModel,
            EnemyViewModel enemyViewModel, Action<CardView> onCardUsed)
        {
            CardEffect = cardEffect;
            _playerViewModel = playerViewModel;
            _enemyViewModel = enemyViewModel;
            _onCardUsed = onCardUsed;

            _cardText.text = CardEffect.Text;
            _cardIcon.sprite = CardEffect.Icon;
            _energyCostText.text = CardEffect.EnergyCost.ToString();
        }

        private void OnCardClicked()
        {
            if (_playerViewModel.Energy.Value < CardEffect.EnergyCost)
            {
                Debug.Log("Недостаточно энергии для использования карты.");
                return;
            }

            CardEffect.Apply(_playerViewModel, _enemyViewModel);
            _playerViewModel.DecreaseEnergy(CardEffect.EnergyCost);

            _onCardUsed?.Invoke(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnCardClicked();
        }
    }
}