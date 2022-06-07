using System;
using Model.Cards;
using Model.Cards.Spells;
using Model.Combat.GameAreas;
using UnityEngine;

namespace Model.Combat.Effects
{
    public class AddSpellToAreaEffect<TCard> : Effect where TCard : Card
    {
        private readonly CardArea<TCard> _area;
        private readonly Type _spell;

        public AddSpellToAreaEffect(float dealyAfter, CardArea<TCard> area, Type spell) : base(dealyAfter)
        {
            _area = area;
            _spell = spell;
        }

        public override void Execute()
        {
            _area.CreateCard(_spell, new Vector3(0, 10, 0));
        }
    }
}