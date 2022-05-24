using Model.Cards;

namespace Model.Combat.Effects
{
    public class AddCardFromDiscardPileToDeck : Effect
    {
        private readonly CardInDiscardPile _card;

        public AddCardFromDiscardPileToDeck(float dealyAfter, CardInDiscardPile card) : base(dealyAfter)
        {
            _card = card;
        }

        public override void Execute()
        {
            _card.AddToDeck();
        }
    }
}