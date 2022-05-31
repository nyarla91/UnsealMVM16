using Model.Cards;
using Model.Cards.Deckbuilding;
using Model.Combat.GameAreas;
using UnityEngine;

namespace Model.Deckbulding
{
    public class BuidledDeck : CardArea<CardInBuidledDeck>
    {
        protected override int MaxSize => 20;
        
        protected override void RearrangeCards()
        {
            
        }
    }
}