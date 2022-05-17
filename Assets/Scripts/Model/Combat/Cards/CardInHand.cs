using Model.Combat.Actions;
using UnityEngine;
using PointerType = NyarlaEssentials.Pointers.PointerType;

namespace Model.Combat.Cards
{
    public sealed class CardInHand : Card
    {
        [DontCallFromSpells]
        public void Discard()
        {
            Spell.OnDiscard();
            MoveToDiscardPile();
        }
        
        protected override void DetachFromPlayArea()
        {
            GameBoard.PlayerHand.RemoveCard(this);
        }

        private void Start()
        {
            GameBoard.PlayerHand.AddCard(this);
            PointerTarget.OnClick += OnClick;
        }

        private void OnClick(PointerType button, Vector3 contactpoint)
        {
            if (button == PointerType.Left)
                Play();
        }

        private void Play()
        {
            if (GameBoard.TargetChooser.ChooseActive || GameBoard.PlayerBoard.IsFull)
                return;
            
            GameBoard.PlayerHand.OnSpellPlayed?.Invoke(Spell);
            Spell.OnPlay();
            MoveToBoard();
        }

        private void OnDestroy()
        {
            PointerTarget.OnClick -= OnClick;
        }
    }
}