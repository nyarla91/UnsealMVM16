using Model.Combat.Effects;

namespace Model.Cards.Combat
{
    public sealed class CardInDeck : CardInCombat
    {
        private static readonly string[] DrawSounds = {"Card/Draw1", "Card/Draw2", "Card/Draw3", "Card/Draw4"};
        
        [DontCallFromSpells]
        public void Draw()
        {
            if (GameBoard.PlayerHand.IsFull)
                return;
            
            AudioSource.PlayOneShot(SoundRandomizer.LoadAudio(DrawSounds), 1);
            TransformIntoCardInAnotherArea<CardInHand>();
            Spell.OnDraw();
        }

        protected override void DetachFromPlayArea()
        {
            GameBoard.PlayerDeck.RemoveCard(this);
        }

        public override void Init()
        {
            GameBoard.PlayerDeck.AddCard(this);
        }
    }
}