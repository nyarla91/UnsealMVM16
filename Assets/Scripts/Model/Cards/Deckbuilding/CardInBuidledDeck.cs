using System;
using UnityEngine;
using PointerType = Essentials.Pointers.PointerType;

namespace Model.Cards.Deckbuilding
{
    public class CardInBuidledDeck : CardInDeckbuilding
    {
        protected override bool LocalPosition => true;

        protected override void DetachFromPlayArea()
        {
            DeckbuildingBoard.BuildedDeck.RemoveCard(this);
        }

        public override void Init()
        {
            DeckbuildingBoard.BuildedDeck.AddCard(this);
            PointerTarget.OnClick += OnClick;
        }

        private void OnClick(PointerType button, Vector3 contactpoint)
        {
            if (button != PointerType.Left)
                return;
            
            if (Spell.InfiniteInDeck)
                Exile();
            else
                TransformIntoCardInAnotherArea<CardInLibary>();
        }

        private void OnDestroy()
        {
            PointerTarget.OnClick -= OnClick;
        }
    }
}