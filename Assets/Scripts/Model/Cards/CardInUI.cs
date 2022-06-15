namespace Model.Cards
{
    public class CardInUI : Card
    {
        public override bool InteractableOnPause => true;
        protected override void PassBoard(Card card) { }

        protected override void DetachFromPlayArea() { }

        public override void Init() { }
    }
}