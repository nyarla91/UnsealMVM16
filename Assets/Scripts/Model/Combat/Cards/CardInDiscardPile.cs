using Model.Combat.Actions;

namespace Model.Combat.Cards
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