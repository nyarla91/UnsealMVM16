using System;
using Model.Cards.Spells;
using UnityEngine;

namespace Model.Deckbulding.Filters
{
    public class TypeFilterButton : FilterButton
    {
        [SerializeField] private SpellType _type;
        
        protected override Func<Spell, bool> _criteria => spell => spell.Type == _type;
    }
}