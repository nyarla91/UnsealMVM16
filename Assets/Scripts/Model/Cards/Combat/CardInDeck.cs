using Model.Combat.Effects;

namespace Model.Cards.Combat
{
    public sealed class CardInDeck : CardInCombat
    {
        
        [DontCallFromSpells]
        public void Draw()
        {
            if (GameBoard.PlayerHand.IsFull)
                return;
            
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