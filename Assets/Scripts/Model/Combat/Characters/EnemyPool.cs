using System;
using System.Collections.Generic;
using Essentials;
using Model.Combat.GameAreas;
using Model.Combat.Targeting;
using Model.Global;
using Model.Travel.Map;
using UnityEngine;
using Zenject;

namespace Model.Combat.Characters
{
    public class EnemyPool : ComponentInstantiator
    {
        [SerializeField] private GameBoard _gameBoard;
        
        private readonly List<Enemy> _activeEnemies = new List<Enemy>();
        private List<Enemy> _reservedEnemies;

        [Inject] private GlobalTravelState _globalTravelState;
        
        public List<Enemy> ActiveEnemies => _activeEnemies;

        public event Action OnAllEnemiesDefeated;

        private void Start()
        {
            CombatData combatData = _globalTravelState.NextCombatData;
            _reservedEnemies = combatData.Enemies;
            for (var i = 0; i < combatData.EnemiesAtOnce; i++)
            {
                ActivateNextEnemy();
            }
            RearrangEenemies();
        }

        private void ActivateNextEnemy()
        {
            if (_reservedEnemies.Count == 0)
                return;

            Enemy prefab = _reservedEnemies[0];
            InstantiateForComponent(out Enemy enemy, prefab, transform);
            enemy.GameBoard = _gameBoard; 
            enemy.GetComponent<TargetToChoose>().GameBoard = _gameBoard; 
            enemy.Init();
            _activeEnemies.Add(enemy);
            enemy.OnDeath += () => OnEnemyDeath(enemy);
            
            _reservedEnemies.RemoveAt(0);
        }

        private void OnEnemyDeath(Enemy enemy)
        {
            _activeEnemies.Remove(enemy);
            ActivateNextEnemy();
            if (_activeEnemies.Count == 0)
                OnAllEnemiesDefeated?.Invoke();
            RearrangEenemies();
        }

        private void RearrangEenemies()
        {
            float leftMostIndex = (1 - _activeEnemies.Count) * 0.5f;
            const float UnitsPerIndex = 5.5f;
            for (int i = 0; i < _activeEnemies.Count; i++)
            {
                _activeEnemies[i].TargetPosition = new Vector3((leftMostIndex + i) * UnitsPerIndex, 0, 0);
            }
        }
    }
}