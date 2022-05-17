using Model.Combat.Actions;
using Model.Combat.TargetChoose;
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

        public PlayerHand PlayerHand => _playerHand;
        public PlayerBoard PlayerBoard => _playerBoard;
        public PlayerDeck PlayerDeck => _playerDeck;
        public PlayerDiscardPile PlayerDiscardPile => _playerDiscardPile;
        public TargetChooser TargetChooser => _targetChooser;
        public EffectQueue EffectQueue => _effectQueue;
    }
}