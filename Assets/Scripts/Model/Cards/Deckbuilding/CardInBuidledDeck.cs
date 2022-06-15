using System;
using UnityEngine;
using PointerType = Essentials.Pointers.PointerType;

namespace Model.Cards.Deckbuilding
{
    public class CardInBuidledDeck : CardInDeckbuilding
    {
        private static readonly string[] RemoveFromDeckSounds = {"Card/Shuffle1", "Card/Shuffle2", "Card/Shuffle3", "Card/Shuffle4"};
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
            {
                AudioSource.PlayOneShot(SoundRandomizer.LoadAudio(RemoveFromDeckSounds), 1);      
                TransformIntoCardInAnotherArea<CardInLibary>();
            }
        }

        private void OnDestroy()
        {
            PointerTarget.OnClick -= OnClick;
        }
    }
}