using Model.Combat.Effects;
using UnityEngine;
using PointerType = Essentials.Pointers.PointerType;
using Vector3 = UnityEngine.Vector3;

namespace Model.Cards
{
    public sealed class CardOnBoard : Card
    {
        
        [DontCallFromSpells]
        public void Purge()
        {
            Spell.OnPurge();
            GameBoard.PlayerBoard.OnCardPurged?.Invoke(Spell);
            MoveToDiscardPile();
        }
        
        protected override void DetachFromPlayArea()
        {
            GameBoard.PlayerBoard.RemoveCard(this);
        }

        private void Start()
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