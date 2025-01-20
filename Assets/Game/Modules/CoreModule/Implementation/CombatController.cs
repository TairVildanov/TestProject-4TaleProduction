using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Modules.CardModule.Declaration;
using Game.Modules.CardModule.Implementation;
using Game.Modules.CardModule.Presentation;
using Game.Modules.EnemyModule.Implementation;
using Game.Modules.PlayerModule.Implementation;
using R3;

namespace Game.Modules.CoreModule.Implementation
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class CombatController : IDisposable
    {
        private readonly PlayerViewModel _playerViewModel;
        private readonly List<EnemyViewModel> _enemyViewModels;
        private readonly CardSpawner _cardSpawner;
        private readonly DeckManager _deckManager;

        private readonly int _cardsCount = 5;

        private List<CardEffect> _spawnedCards = new();

        private bool _isPlayerTurn;
        private bool _isGameOver;

        private readonly CompositeDisposable _disposable = new();

        public CombatController(PlayerViewModel playerViewModel, List<EnemyViewModel> enemyViewModels,
            CardSpawner cardSpawner, DeckManager deckManager)
        {
            _playerViewModel = playerViewModel;
            _enemyViewModels = enemyViewModels;
            _cardSpawner = cardSpawner;
            _deckManager = deckManager;
            _isPlayerTurn = true;

            _playerViewModel.Health.Subscribe(OnPlayerHealthChanged).AddTo(_disposable);
            foreach (var enemy in _enemyViewModels)
            {
                enemy.Health.Subscribe(OnEnemyHealthChanged).AddTo(_disposable);
            }
        }

        public void StartTurn()
        {
            if (_isGameOver) return;

            if (_isPlayerTurn)
            {
                Debug.Log("Ход игрока начался");

                _playerViewModel.RestoreEnergy();
                _spawnedCards = _deckManager.DrawCards(_cardsCount);
                _cardSpawner.SpawnCards(_spawnedCards, _playerViewModel, _enemyViewModels[0], OnCardUsed);
                if (!_spawnedCards.Any(card => _playerViewModel.Energy.Value >= card.EnergyCost))
                {
                    EndPlayerTurn();
                }
            }
            else
            {
                Debug.Log("Ход врагов начался");

                foreach (var enemy in _enemyViewModels)
                {
                    PerformEnemyAction(enemy);
                }
            }
        }

        private void OnCardUsed(CardView usedCard)
        {
            if (_isGameOver) return;

            _cardSpawner.RemoveCard(usedCard);
            _deckManager.AddToDiscardPile(usedCard.CardEffect);
            _spawnedCards.Remove(usedCard.CardEffect);
            var hasPlayableCards = _spawnedCards.Any(card => _playerViewModel.Energy.Value >= card.EnergyCost);

            if (_playerViewModel.Energy.Value <= 0 || !hasPlayableCards)
            {
                EndPlayerTurn();
            }
        }


        private void EndPlayerTurn()
        {
            if (_isGameOver) return;

            Debug.Log("Ход игрока завершён");

            MoveRemainingCardsToDiscardPile();

            _isPlayerTurn = false;
            StartTurn();
        }

        private void MoveRemainingCardsToDiscardPile()
        {
            foreach (var cardEffect in _spawnedCards)
            {
                _deckManager.AddToDiscardPile(cardEffect);
            }

            _spawnedCards.Clear();
            _cardSpawner.ClearCards();
        }

        private void PerformEnemyAction(EnemyViewModel enemy)
        {
            if (_isGameOver) return;

            var random = Random.Range(0, 100);
            if (random >= 40)
            {
                _playerViewModel.GetDamage(enemy.Damage);
            }
            else
            {
                enemy.ToggleShield(true, enemy.Defense);
            }

            if (enemy == _enemyViewModels[^1])
            {
                EndEnemyTurn();
            }
        }

        private void EndEnemyTurn()
        {
            if (_isGameOver) return;

            _isPlayerTurn = true;
            StartTurn();
        }

        private void OnPlayerHealthChanged(int health)
        {
            if (health > 0) return;
            Debug.Log("Игрок проиграл!");
            EndGame();
        }

        private void OnEnemyHealthChanged(int health)
        {
            if (health > 0 || !_enemyViewModels.All(enemy => enemy.Health.Value <= 0)) return;
            Debug.Log("Игрок победил!");
            EndGame();
        }

        private void EndGame()
        {
            _isGameOver = true;
            _cardSpawner.ClearCards();

            Debug.Log("Перезапуск сцены через 3 секунды...");
            _ = RestartSceneAfterDelay();
        }

        private async UniTask RestartSceneAfterDelay()
        {
            await UniTask.Delay(3000);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}