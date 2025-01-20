using System;
using System.Collections.Generic;
using Game.Modules.CardModule.Declaration;
using Game.Modules.CardModule.Presentation;
using Game.Modules.EnemyModule.Implementation;
using Game.Modules.PlayerModule.Implementation;
using UnityEngine;

namespace Game.Modules.CardModule.Implementation
{
    public class CardSpawner : MonoBehaviour
    {
        [SerializeField] private CardView _cardPrefab;
        [SerializeField] private Transform _cardParent;

        private readonly List<CardView> _spawnedCards = new();

        public void SpawnCards(List<CardEffect> cardEffects, PlayerViewModel playerViewModel,
            EnemyViewModel enemyViewModel, Action<CardView> onCardUsed)
        {
            ClearCards();

            foreach (var effect in cardEffects)
            {
                var cardView = Instantiate(_cardPrefab, _cardParent);
                cardView.Initialize(effect, playerViewModel, enemyViewModel, onCardUsed);
                _spawnedCards.Add(cardView);
            }
        }

        public void ClearCards()
        {
            foreach (var card in _spawnedCards)
            {
                Destroy(card.gameObject);
            }

            _spawnedCards.Clear();
        }

        public void RemoveCard(CardView cardView)
        {
            _spawnedCards.Remove(cardView);
            Destroy(cardView.gameObject);
        }
    }
}