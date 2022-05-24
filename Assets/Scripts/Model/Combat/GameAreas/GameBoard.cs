using System.Collections.Generic;
using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Combat.Targeting;
using UnityEngine;

namespace Model.Combat.GameAreas
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private PlayerHand _playerHand;
        [SerializeField] private PlayerBoard _playerBoard;
        [SerializeField] private PlayerDeck _playerDeck;
        [SerializeField] private PlayerDiscardPile _playerDiscardPile;
        [SerializeField] private TargetChooser _targetChooser;
        [SerializeField] private EffectQueue _effectQueue;
        [SerializeField] private Turn _turn;
        [SerializeField] private Player _player;

        private readonly List<Enemy> _enemies = new List<Enemy>();

        public PlayerHand PlayerHand => _playerHand;
        public PlayerBoard PlayerBoard => _playerBoard;
        public PlayerDeck PlayerDeck => _playerDeck;
        public PlayerDiscardPile PlayerDiscardPile => _playerDiscardPile;
        public TargetChooser TargetChooser => _targetChooser;
        public EffectQueue EffectQueue => _effectQueue;
        public Turn Turn => _turn;
        public Player Player => _player;
        public List<Enemy> Enemies => _enemies;

        public void AddEnemy(Enemy enemy)
        {
            _enemies.Add(enemy);
        }

        public void RemoveEnemy(Enemy enemy)
        {
            _enemies.Remove(enemy);
        }
    }
}