using UnityEngine;
using PointerType = Essentials.Pointers.PointerType;

namespace Model.Cards.Deckbuilding
{
    public class CardInLibary : CardInDeckbuilding
    {
        private static readonly string[] AddToDeckSouns = {"Card/Draw1", "Card/Draw2", "Card/Draw3", "Card/Draw4"};
        protected override bool LocalPosition => true;

        protected override void DetachFromPlayArea()
        {
            DeckbuildingBoard.Libary.RemoveCard(this);
        }

        public override void Init()
        {
            DeckbuildingBoard.Libary.AddCard(this);
            PointerTarget.OnClick += OnClick;
        }

        private void OnClick(PointerType button, Vector3 contactpoint)
        {
            if (button != PointerType.Left || DeckbuildingBoard.BuildedDeck.IsFull)
                return;
            
            if (Spell.InfiniteInDeck)
                DeckbuildingBoard.BuildedDeck.CreateInfiniteCard(Spell.GetType(), transform.position);
            else
            {
                AudioSource.PlayOneShot(SoundRandomizer.LoadAudio(AddToDeckSouns), 1);   
                TransformIntoCardInAnotherArea<CardInBuidledDeck>();
            }
        }

        private void OnDestroy()
        {
            PointerTarget.OnClick -= OnClick;
        }
    }
}