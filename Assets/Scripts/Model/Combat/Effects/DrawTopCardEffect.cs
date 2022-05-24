using Model.Combat.GameAreas;

namespace Model.Combat.Effects
{
    public class DrawTopCardEffect : Effect
    {
        private readonly PlayerDeck _deck;

        public DrawTopCardEffect(float dealyAfter, PlayerDeck deck) : base(dealyAfter)
        {
            _deck = deck;
        }

        public override void Execute()
        {
            _deck.DrawACard();
        }
    }
}