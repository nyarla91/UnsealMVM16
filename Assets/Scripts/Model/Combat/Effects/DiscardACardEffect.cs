using Model.Cards;

namespace Model.Combat.Effects
{
    public class DiscardACardEffect : Effect
    {
        private readonly CardInHand _card;

        public DiscardACardEffect(float dealyAfter, CardInHand card) : base(dealyAfter)
        {
            _card = card;
        }

        public override void Execute()
        {
            _card.Discard();
        }
    }
}