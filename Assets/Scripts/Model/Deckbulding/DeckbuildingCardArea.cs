using Model.Cards.Deckbuilding;
using Model.Combat.GameAreas;
using UnityEngine;

namespace Model.Deckbulding
{
    public abstract class DeckbuildingCardArea<TCard> : CardArea<TCard> where TCard : CardInDeckbuilding
    {
        [SerializeField] protected DeckbuildingBoard _deckbuildingBoard;
        
        protected override void PassBoard(ref TCard cardInPlace)
        {
            cardInPlace.DeckbuildingBoard = _deckbuildingBoard;
        }
    }
}