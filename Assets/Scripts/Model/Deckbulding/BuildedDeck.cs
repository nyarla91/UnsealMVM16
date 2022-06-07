using System;
using System.Collections.Generic;
using System.Linq;
using Model.Cards;
using Model.Cards.Deckbuilding;
using Model.Cards.Spells;
using Model.Combat.GameAreas;
using Model.Global.Save;
using UnityEngine;
using Zenject;

namespace Model.Deckbulding
{
    public class BuildedDeck : DeckbuildingCardArea<CardInBuidledDeck>
    {
        [Inject] private ManualSave _manualSave;
        
        protected override int MaxSize => 20;

        public void CreateInfiniteCard(Type spellType, Vector3 startingPosition) => CreateCard(spellType, startingPosition);

        public bool ContainsSpell(Type spellType) => Cards.Any(card => card.Spell.GetType() == spellType);

        public void SaveDeck()
        {
            _manualSave.Data.Deck = new List<string>();
            foreach (Type spellType in Cards.Select(card => card.Spell.GetType()))
            {
                _manualSave.Data.Deck.Add($"{spellType.Namespace}.{spellType.Name}");
            }
            _manualSave.Save();
        }

        protected override void RearrangeCards()
        {
            const float UnitsPerColumn = 2.25f;
            const float UnitsPerRow = 0.9f;
            const int Columns = 2;
            int column = -1;
            int row = 0;
            foreach (var card in Cards)
            {
                column++;
                if (column == Columns)
                {
                    column = 0;
                    row++;
                }
                card.TargetPosition = CardStandart.localPosition + new Vector3(column * UnitsPerColumn, 0, -row * UnitsPerRow);
                card.TargetRotation = CardStandart.rotation;
            }
        }

        private void Start()
        {
            foreach (string card in _manualSave.Data.Deck)
            {
                CreateCard(card, transform.position);
            }
        }
    }
}