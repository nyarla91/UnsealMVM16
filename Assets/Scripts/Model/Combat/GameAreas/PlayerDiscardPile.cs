using Model.Cards;
using Model.Combat.Effects;
using UnityEngine;

namespace Model.Combat.GameAreas
{
    public class PlayerDiscardPile : PlayerPile<CardInDiscardPile>
    {
        public void ShuffleIntoDeck()
        {
            foreach (var card in Cards)
            {
                GameBoard.EffectQueue.AddEffect(new AddCardFromDiscardPileToDeck(0.025f, card), 0);
            }
        }
    }
}