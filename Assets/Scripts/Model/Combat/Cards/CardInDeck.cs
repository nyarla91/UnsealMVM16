using Model.Combat.Actions;
using UnityEngine;
using PointerType = NyarlaEssentials.Pointers.PointerType;

namespace Model.Combat.Cards
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