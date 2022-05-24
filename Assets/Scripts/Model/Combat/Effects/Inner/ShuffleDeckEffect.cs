using Model.Combat.GameAreas;

namespace Model.Combat.Effects.Inner
{
    public class ShuffleDeckEffect : Effect
    {
        private readonly PlayerDeck _deck;

        public ShuffleDeckEffect(float dealyAfter, PlayerDeck deck) : base(dealyAfter)
        {
            _deck = deck;
        }

        public override void Execute()
        {
            _deck.Shuffle();
        }
    }
}