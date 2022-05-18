using System.Linq;
using Model.Combat.Actions;
using Model.Combat.Cards;
using NyarlaEssentials;
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
                _gameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(this, 0.1f), false);
            }
        }
        
        [DontCallFromSpells]
        public void DrawACard()
        {
            if (Cards.Count == 0)
            {
                _gameBoard.PlayerDiscardPile.ShuffleIntoDeck();
                _gameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(this, 0.1f), false);
            }
            else
            {
                Cards[^1].Draw();
            }
        }

        public void Shuffle()
        {
            Cards = Cards.Shuffle().ToList();
            RearrangeCards();
        }
    }
}