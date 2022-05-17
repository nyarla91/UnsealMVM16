namespace Model.Combat.Cards
{
    public sealed class CardInDiscardPile : Card
    {
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