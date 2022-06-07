using Model.Combat.GameAreas;
using Zenject;

namespace Model.Cards.Combat
{
    public abstract class CardInCombat : Card
    {
        [field: Inject] public GameBoard GameBoard { protected get; set; }
        
        protected override void PassBoard(Card card)
        {
            CardInCombat cardInCombat = ((CardInCombat) card);
            cardInCombat.GameBoard = GameBoard;
            cardInCombat.Spell.GameBoard = GameBoard;
        }
    }
}