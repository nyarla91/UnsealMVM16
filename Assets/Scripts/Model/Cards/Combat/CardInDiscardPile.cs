using Model.Combat.Effects;
using UnityEngine;

namespace Model.Cards
{
    public sealed class CardInDiscardPile : CardInCombat
    {
        
        [DontCallFromSpells]
        public void AddToDeck()
        {
            TransformIntoCardInAnotherArea<CardInDeck>();
        }
        
        protected override void DetachFromPlayArea()
        {
            GameBoard.PlayerDiscardPile.RemoveCard(this);
        }

        public override void Init()
        {
            GameBoard.PlayerDiscardPile.AddCard(this);
        }
    }
}