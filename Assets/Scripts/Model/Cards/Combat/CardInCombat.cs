using Model.Combat.GameAreas;
using UnityEngine;
using Zenject;

namespace Model.Cards
{
    public abstract class CardInCombat : Card
    {
        [field: Inject] public GameBoard GameBoard { protected get; set; }
        
        protected override void PassBoard(Card card)
        {
            ((CardInCombat) card).GameBoard = GameBoard;
        }
    }
}