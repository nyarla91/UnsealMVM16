using System;
using System.Linq;
using Essentials;
using Model.Cards;
using Model.Cards.Combat;
using Model.Cards.Spells;
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

        public event Action<Spell> OnSpellDraw;
        public event Action OnShuffle;
        
        public void DrawCards(int ammount)
        {
            for (int i = 0; i < ammount; i++)
            {
                GameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.1f, this));
            }
        }
        
        [DontCallFromSpells]
        public async void DrawACard()
        {
            if (Size == 0  && GameBoard.PlayerDiscardPile.Size > 0)
            {
                GameBoard.EffectQueue.AddEffect(new CureIntoxicationEffect(0.1f, GameBoard.Player, 1), 0);
                GameBoard.EffectQueue.AddEffect(new ShuffleDeckEffect(0.1f, this), 0);
                GameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.1f, this), 0);
                GameBoard.PlayerDiscardPile.ShuffleIntoDeck();
                await GameBoard.EffectQueue.WaitForEffects();
                OnShuffle?.Invoke();
            }
            else if (Size > 0)
            {
                CardInDeck cardToDraw = Cards[^1];
                cardToDraw.Draw();
                OnSpellDraw?.Invoke(cardToDraw.Spell);
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