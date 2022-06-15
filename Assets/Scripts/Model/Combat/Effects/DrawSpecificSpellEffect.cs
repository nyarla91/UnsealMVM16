using System;
using System.Linq;
using Model.Cards.Combat;
using Model.Combat.GameAreas;
using UnityEngine;

namespace Model.Combat.Effects
{
    public class DrawSpecificSpellEffect : Effect
    {
        private readonly PlayerDeck _deck;
        private readonly Type _spellType;

        public DrawSpecificSpellEffect(float dealyAfter, PlayerDeck deck, Type spellType) : base(dealyAfter)
        {
            _deck = deck;
            _spellType = spellType;
        }

        public override void Execute()
        {
            _deck.Cards.First(card => card.GetType() == _spellType)?.Draw();
        }
    }
}