using System;
using System.Collections.Generic;
using Model.Combat.Cards;
using Model.Combat.Cards.Spells;
using UnityEngine;

namespace Model.Combat.GameAreas
{
    public sealed class PlayerHand : CardArea<CardInHand>
    {
        public override int MaxSize => 12;

        public Action<Spell> OnSpellPlayed;
        
        protected override void RearrangeCards()
        {
            if (Size == 0)
                return;

            float leftMostIndex = (1 - Size) * 0.5f;
            const float UnitsPerIndex = 1;
            for (int i = 0; i < Size; i++)
            {
                Vector3 localPosition = new Vector3((leftMostIndex + i) * UnitsPerIndex, i * 0.02f, 0);
                Cards[i].TargetPosition = CardStandart.TransformPoint(localPosition);
                Cards[i].TargetRotation = CardStandart.rotation;
            }
        }
    }
}