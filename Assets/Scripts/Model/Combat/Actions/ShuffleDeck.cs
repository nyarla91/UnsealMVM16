using Model.Combat.GameAreas;

namespace Model.Combat.Actions
{
    public class ShuffleDeck : Effect
    {
        private readonly PlayerDeck _deck;

        public ShuffleDeck(PlayerDeck deck, float dealyAfter) : base(dealyAfter)
        {
            _deck = deck;
        }

        public override void Execute()
        {
            _deck.Shuffle();
        }
    }
}