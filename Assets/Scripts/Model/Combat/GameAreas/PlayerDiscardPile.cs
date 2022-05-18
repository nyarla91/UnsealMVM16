using Model.Combat.Actions;
using Model.Combat.Cards;
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
                _gameBoard.EffectQueue.AddEffect(new AddCardFromDiscardPileToDeck(card, 0.05f), true);
            }
        }
    }
}