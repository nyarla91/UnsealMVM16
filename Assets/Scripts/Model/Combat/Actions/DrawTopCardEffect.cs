using Model.Combat.Cards;

namespace Model.Combat.Actions
{
    public class DrawCardEffect : Effect
    {
        private readonly CardInDeck _cardToDraw;

        public DrawCardEffect(float dealyAfter) : base(dealyAfter)
        {
            
        }

        public override void Execute()
        {
            _cardToDraw.Draw();
        }
    }
}