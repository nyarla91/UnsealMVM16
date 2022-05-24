using Model.Combat.Effects;
using UnityEngine;

namespace Model.Cards
{
    public sealed class CardInDiscardPile : Card
    {
        
        [DontCallFromSpells]
        public void AddToDeck()
        {
            MoveToDeck();
        }
        
        protected override void DetachFromPlayArea()
        {
            GameBoard.PlayerDiscardPile.RemoveCard(this);
        }

        private void Start()
        {
            GameBoard.PlayerDiscardPile.AddCard(this);
        }
    }
}