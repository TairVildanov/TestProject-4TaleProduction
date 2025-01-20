using System;
using System.Collections.Generic;
using System.Linq;
using Game.Modules.CardModule.Data;
using Game.Modules.CardModule.Declaration;
using Game.Modules.CardModule.Implementation;
using Game.Modules.CardModule.Presentation;
using Game.Modules.EnemyModule.Data;
using Game.Modules.EnemyModule.Implementation;
using Game.Modules.EnemyModule.Presentation;
using Game.Modules.PlayerModule.Data;
using Game.Modules.PlayerModule.Implementation;
using Game.Modules.PlayerModule.Presentation;
using UnityEngine;

namespace Game.Modules.CoreModule.Implementation
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private PlayerView _playerPrefab;
        [SerializeField] private Transform _playerSpawnPoint;

        [SerializeField] private List<EnemyConfig> _enemyConfigs;
        [SerializeField] private EnemyView _enemyPrefab;
        [SerializeField] private Transform _enemySpawnPoint;

        [SerializeField] private CardSpawner _cardSpawner;
        [SerializeField] private List<CardEffectConfig> _cardConfigs;

        [SerializeField] private DeckView _deckView;

        private PlayerViewModel _playerViewModel;
        private readonly List<EnemyViewModel> _enemyViewModels = new();

        private readonly int _drawPileCount = 20;

        private float _enemyShift;

        private void Start()
        {
            SpawnPlayer();

            foreach (var enemyConfig in _enemyConfigs)
            {
                SpawnEnemy(enemyConfig);
            }

            var deck = _cardConfigs.Select(CreateCardEffect).ToList();
            var deckManager = new DeckManager(deck, _drawPileCount);
            _deckView.Initialize(deckManager);
            var combatController =
                new CombatController(_playerViewModel, _enemyViewModels, _cardSpawner, deckManager);
            combatController.StartTurn();
        }

        private void SpawnPlayer()
        {
            var player = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity,
                _playerSpawnPoint);

            var playerModel =
                new PlayerModel(_playerConfig.Health, _playerConfig.Energy, _playerConfig.MaxEnergy);
            var playerViewModel = new PlayerViewModel(playerModel);
            _playerViewModel = playerViewModel;
            player.Initialize(playerViewModel);
        }

        private void SpawnEnemy(EnemyConfig enemyConfig)
        {
            var calculatedPosition =
                new Vector2(_enemySpawnPoint.position.x + _enemyShift, _enemySpawnPoint.position.y);
            var enemy = Instantiate(_enemyPrefab, calculatedPosition, Quaternion.identity, _enemySpawnPoint);

            var enemyModel = new EnemyModel(enemyConfig.Name, enemyConfig.Health, enemyConfig.Damage,
                enemyConfig.Defense);

            var enemyViewModel = new EnemyViewModel(enemyModel);
            _enemyViewModels.Add(enemyViewModel);

            enemy.Initialize(enemyViewModel);
            _enemyShift -= 3.8F;
        }

        private CardEffect CreateCardEffect(CardEffectConfig config)
        {
            return config switch
            {
                CardDamageEffectConfig damageConfig => new DamageCardEffect(damageConfig),
                CardHealEffectConfig healConfig => new HealCardEffect(healConfig),
                CardShieldEffectConfig shieldConfig => new ShieldCardEffect(shieldConfig),
                _ => throw new ArgumentException("Unknown card config type!")
            };
        }
    }
}