using Model.Combat.Effects;
using UnityEngine;
using PointerType = Essentials.Pointers.PointerType;

namespace Model.Cards
{
    public sealed class CardInDeck : Card
    {
        
        [DontCallFromSpells]
        public void Draw()
        {
            Spell.OnDraw();
            MoveToHand();
        }

        protected override void DetachFromPlayArea()
        {
            GameBoard.PlayerDeck.RemoveCard(this);
        }

        private void Start()
        {
            GameBoard.PlayerDeck.AddCard(this);
            PointerTarget.OnClick += OnClick;
        }

        private void OnClick(PointerType button, Vector3 contactpoint)
        {
            MoveToHand();
        }

        private void OnDestroy()
        {
            PointerTarget.OnClick -= OnClick;
        }
    }
}