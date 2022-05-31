using Model.Deckbulding;
using Zenject;

namespace Model.Cards.Deckbuilding
{
    public abstract class CardInDeckbuilding : Card
    {
        [field: Inject] public DeckbuildingBoard DeckbuildingBoard { protected get; set; }
        
        protected override void PassBoard(Card card)
        {
            ((CardInDeckbuilding) card).DeckbuildingBoard = DeckbuildingBoard;
        }
    }
}