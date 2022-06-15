using System;
using Model.Cards;
using Model.Cards.Combat;
using UnityEngine;

namespace Model.Combat.GameAreas
{
    public class PlayerPile<TCard> : CombatCardArea<TCard> where TCard : CardInCombat
    {
        protected override void RearrangeCards()
        {
            const float UnitsPerIndex = 0.03f;
            
            for (int i = 0; i < Size; i++)
            {
                Cards[i].TargetRotation = CardStandart.rotation;
                Cards[i].TargetPosition = CardStandart.position + new Vector3(0, UnitsPerIndex * i, 0);
            }
        }
    }
}