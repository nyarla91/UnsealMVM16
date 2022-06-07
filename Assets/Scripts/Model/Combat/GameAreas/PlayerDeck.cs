using System.Linq;
using Essentials;
using Model.Cards;
using Model.Combat.Effects;
using Model.Combat.Effects.Inner;
using Model.Combat.Targeting;
using Model.Global.Save;
using Presenter.Cards;
using UnityEngine;
using View.Cards;
using Zenject;

namespace Model.Combat.GameAreas
{
    public sealed class PlayerDeck : PlayerPile<CardInDeck>
    {
        [Inject] private ManualSave _manualSave;
        
        public void DrawCards(int ammount)
        {
            for (int i = 0; i < ammount; i++)
            {
                GameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.1f, this));
            }
        }
        
        [DontCallFromSpells]
        public void DrawACard()
        {
            if (Cards.Count == 0)
            {
                GameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.1f, this), 0);
                GameBoard.EffectQueue.AddEffect(new ShuffleDeckEffect(0.1f, this), 0);
                GameBoard.PlayerDiscardPile.ShuffleIntoDeck();
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
            foreach (string card in _manualSave.Data.Deck)
            {
                CardInDeck createdCard = CreateCard(card, transform.position);
                createdCard.GetComponent<TargetToChoose>().GameBoard = GameBoard;
            }
            Shuffle();
        }
    }
}