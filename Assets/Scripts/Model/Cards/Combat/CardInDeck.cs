using Model.Combat.Effects;
using UnityEngine;
using PointerType = Essentials.Pointers.PointerType;

namespace Model.Cards
{
    public sealed class CardInDeck : CardInCombat
    {
        
        [DontCallFromSpells]
        public void Draw()
        {
            if (GameBoard.PlayerHand.IsFull)
                return;
            
            Spell.OnDraw();
            TransformIntoCardInAnotherArea<CardInHand>();
        }

        protected override void DetachFromPlayArea()
        {
            GameBoard.PlayerDeck.RemoveCard(this);
        }

        public override void Init()
        {
            GameBoard.PlayerDeck.AddCard(this);
        }
    }
}