using System;
using Model.Cards;
using UnityEngine;

namespace Model.Combat.GameAreas
{
    public class PlayerPile<T2> : CardArea<T2> where T2 : Card
    {
        protected override void RearrangeCards()
        {
            const float UnitsPerIndex = 0.01f;
            
            for (int i = 0; i < Size; i++)
            {
                Cards[i].TargetRotation = CardStandart.rotation;
                Cards[i].TargetPosition = CardStandart.position + new Vector3(0, UnitsPerIndex * i, 0);
            }
        }
    }
}