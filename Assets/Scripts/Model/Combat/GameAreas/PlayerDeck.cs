using System.Linq;
using Essentials;
using Model.Cards;
using Model.Combat.Effects;
using Model.Combat.Effects.Inner;
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
                _gameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.1f, this));
            }
        }
        
        [DontCallFromSpells]
        public void DrawACard()
        {
            if (Cards.Count == 0)
            {
                _gameBoard.EffectQueue.InsertEffect(new DrawTopCardEffect(0.1f, this), 0);
                _gameBoard.EffectQueue.InsertEffect(new ShuffleDeckEffect(0.1f, this), 0);
                _gameBoard.PlayerDiscardPile.ShuffleIntoDeck();
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

        private void Start()
        {
            Shuffle();
        }
    }
}