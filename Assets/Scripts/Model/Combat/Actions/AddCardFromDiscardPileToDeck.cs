using Model.Combat.Cards;

namespace Model.Combat.Actions
{
    public class AddCardFromDiscardPileToDeck : Effect
    {
        private readonly CardInDiscardPile _card;

        public AddCardFromDiscardPileToDeck(CardInDiscardPile card, float dealyAfter) : base(dealyAfter)
        {
            _card = card;
        }

        public override void Execute()
        {
            _card.AddToDeck();
        }
    }
}