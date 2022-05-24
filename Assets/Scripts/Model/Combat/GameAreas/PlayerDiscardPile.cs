using Model.Cards;
using Model.Combat.Effects;
using UnityEngine;

namespace Model.Combat.GameAreas
{
    public class PlayerDiscardPile : PlayerPile<CardInDiscardPile>
    {
        [SerializeField] private GameBoard _gameBoard;
        
        public void ShuffleIntoDeck()
        {
            foreach (var card in Cards)
            {
                _gameBoard.EffectQueue.InsertEffect(new AddCardFromDiscardPileToDeck(0.025f, card), 0);
            }
        }
    }
}