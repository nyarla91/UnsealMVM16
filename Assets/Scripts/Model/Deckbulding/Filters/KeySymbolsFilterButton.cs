using System;
using Model.Cards.Spells;
using UnityEngine;

namespace Model.Deckbulding.Filters
{
    public class KeySymbolsFilterButton : FilterButton
    {
        [SerializeField] private string _keySymbol;
        
        protected override Func<Spell, bool> _criteria => spell => spell.Description.Contains(_keySymbol);
    }
}