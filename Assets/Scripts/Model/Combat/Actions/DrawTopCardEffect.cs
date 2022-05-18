﻿using Model.Combat.GameAreas;

namespace Model.Combat.Actions
{
    public class DrawTopCardEffect : Effect
    {
        private readonly PlayerDeck _deck;

        public DrawTopCardEffect(PlayerDeck deck, float dealyAfter) : base(dealyAfter)
        {
            _deck = deck;
        }

        public override void Execute()
        {
            _deck.DrawACard();
        }
    }
}