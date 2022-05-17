using Model.Combat.Actions;
using Model.Combat.Cards;
using UnityEngine;

namespace Model.Combat.GameAreas
{
    public sealed class PlayerDeck : PlayerPile<CardInDeck>
    {
        [SerializeField] private GameBoard _gameBoard;

        public void DrawCards(int ammount)
        {
            for (int i = 0; i < ammount; i++)
            {
                DrawACard();
            }
        }
        
        public void DrawACard()
        {
            _gameBoard.EffectQueue.AddEffect(new DrawCardEffect(Cards[^1], 0.1f), false);
        }
    }
}