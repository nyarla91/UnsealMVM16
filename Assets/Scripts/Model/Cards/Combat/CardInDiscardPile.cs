using Model.Combat.Effects;

namespace Model.Cards.Combat
{
    public sealed class CardInDiscardPile : CardInCombat
    {
        private static readonly string[] ShuffleSounds = {"Card/Shuffle1", "Card/Shuffle2", "Card/Shuffle3", "Card/Shuffle4"};
        
        [DontCallFromSpells]
        public void AddToDeck()
        {
            AudioSource.PlayOneShot(SoundRandomizer.LoadAudio(ShuffleSounds), 1);
            TransformIntoCardInAnotherArea<CardInDeck>();
        }
        
        protected override void DetachFromPlayArea()
        {
            GameBoard.PlayerDiscardPile.RemoveCard(this);
        }

        public override void Init()
        {
            GameBoard.PlayerDiscardPile.AddCard(this);
        }
    }
}