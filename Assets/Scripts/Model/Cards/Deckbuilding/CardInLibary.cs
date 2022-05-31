namespace Model.Cards.Deckbuilding
{
    public class CardInLibary : CardInDeckbuilding
    {
        protected override bool LocalPosition => true;

        protected override void DetachFromPlayArea()
        {
            DeckbuildingBoard.Libary.RemoveCard(this);
        }

        public override void Init()
        {
            DeckbuildingBoard.Libary.AddCard(this);
        }
    }
}