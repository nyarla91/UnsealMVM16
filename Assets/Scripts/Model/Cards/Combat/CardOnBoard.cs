using Model.Combat.Effects;
using UnityEngine;
using PointerType = Essentials.Pointers.PointerType;
using Vector3 = UnityEngine.Vector3;

namespace Model.Cards
{
    public sealed class CardOnBoard : CardInCombat
    {
        
        [DontCallFromSpells]
        public void Purge()
        {
            Spell.OnPurge();
            GameBoard.PlayerBoard.OnCardPurged?.Invoke(Spell);
            TransformIntoCardInAnotherArea<CardInDiscardPile>();
        }
        
        protected override void DetachFromPlayArea()
        {
            GameBoard.PlayerBoard.RemoveCard(this);
        }

        public override void Init()
        {
            GameBoard.PlayerBoard.AddCard(this);
            PointerTarget.OnClick += OnClick;
        }

        private void OnClick(PointerType button, Vector3 contactpoint)
        {
            
        }

        private void OnDestroy()
        {
            PointerTarget.OnClick -= OnClick;
        }
    }
}